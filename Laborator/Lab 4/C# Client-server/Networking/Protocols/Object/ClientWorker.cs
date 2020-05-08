using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Model;
using Services;
using Networking.Utils;
using Networking.Dto;
using log4net;
using System.Collections.Generic;

namespace Networking.Protocols.Object
{
    public class ClientWorker : IObserver
    {
        private static ILog logger = LogManager.GetLogger(typeof(ClientWorker));

        private IService server;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;

        public ClientWorker(IService server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {

                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
                logger.Debug("ClientWorker object created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void Run()
        {
            logger.Debug("Running client worker...");
            while (connected)
            {
                try
                {
                    object request = formatter.Deserialize(stream);
                    logger.Debug("Receipt request " + request + " from client" + connection);
                    object response = HandleRequest((Request)request);
                    if (response != null)
                    {
                        SendResponse((Response)response);
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.Message + '\n' + e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    logger.Error(e.Message + '\n' + e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        private Response HandleRequest(Request request)
        {
            Response response = null;

            if (request is LoginRequest)
            {
                LoginRequest loginRequest = (LoginRequest)request;
                AdminDto dto = loginRequest.User;
                logger.Debug("Processing login request by user " + dto.ID);
                Admin user = DtoUtils.GetFromDto(dto);
                try
                {
                    lock (server)
                    {
                        server.Login(user.ID, user.Password, this);
                    }
                    return new OkResponse();
                }
                catch (Error e)
                {
                    logger.Debug("User " + dto.ID + " already logged in. Disconnecting...");
                    connected = false;
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is LogoutRequest)
            {
                LogoutRequest logoutRequest = (LogoutRequest)request;
                AdminDto dto = logoutRequest.User;
                logger.Debug("Processing logout request by user " + dto.ID);
                Admin user = DtoUtils.GetFromDto(dto);
                try
                {
                    lock (server)
                    {
                        server.Logout(user.ID, this);
                    }
                    connected = false;
                    return new OkResponse();

                }
                catch (Error e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is SaveChildRequest)
            {
                SaveChildRequest saveChildRequest = (SaveChildRequest)request;
                ChildDto dto = saveChildRequest.Child;
                logger.Debug("Processing save request for child ID " + dto.ID);
                Child c = DtoUtils.GetFromDto(dto);
                try
                {
                    Child result;
                    lock (server)
                    {
                        result = server.SaveChild(c);
                    }
                    if (result != null)
                    {
                        return new SavedChildResponse(DtoUtils.GetDto(result));
                    }
                    else
                    {
                        return new ErrorResponse("Child " + c.ID + " could not be saved");
                    }

                }
                catch (Error e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is FilterChildrenRequest)
            {
                FilterChildrenRequest filterChildrenRequest = (FilterChildrenRequest)request;
                string data = filterChildrenRequest.Data;
                IList<int> parameters = Parser.ToList(data);
                logger.Debug("Processing filter children request for event ID " + parameters[0]);
                try
                {
                    Child[] result;
                    lock (server)
                    {
                        result = server.GetFilteredChildren(parameters[0], parameters[1], parameters[2]).ToArray();
                    }
                    return new ListOfChildrenResponse(DtoUtils.GetDto(result));
                }
                catch (Error e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is GetEventsRequest)
            {
                GetEventsRequest getEventsRequest = (GetEventsRequest)request;
                logger.Debug("Processing get events request");
                try
                {
                    Event[] events;
                    lock (server)
                    {
                        events = server.GetAllEvents().ToArray();
                    }
                    return new ListOfEventsResponse(DtoUtils.GetDto(events));
                }
                catch (Error e)
                {
                    return new ErrorResponse(e.Message);
                }

            }

            if (request is CountChildrenRequest)
            {
                CountChildrenRequest countChildrenRequest = (CountChildrenRequest)request;
                logger.Debug("Processing count children request for event ID " + countChildrenRequest.EventID);
                try
                {
                    int count;
                    lock (server)
                    {
                        count = server.CountChildren(countChildrenRequest.EventID);
                    }
                    return new CounterOfChildrenResponse(count);
                }
                catch (Error e)
                {
                    return new ErrorResponse(e.Message);
                }
            }
            return response;
        }

        private void SendResponse(Response response)
        {
            logger.Debug("Sending response " + response);
            formatter.Serialize(stream, response);
            stream.Flush();
        }

        public void ChildSaved(Child c)
        {
            logger.Debug("Notification - new child added: " + c.ID);
            try
            {
                SendResponse(new NewChildResponse(DtoUtils.GetDto(c)));
            }
            catch (Exception e)
            {
                throw new Error(e.Message);
            }
        }
    }

}