using log4net;
using Networking.Utils;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Networking.Protocols.Protobuf
{
    using AthletesRequestType = AthletesRequest.Types.RequestType;

    public class ProtobufClientWorker : IObserver
    {
        private static ILog logger = LogManager.GetLogger(typeof(ProtobufClientWorker));

        private IService server;
        private TcpClient connection;

        private NetworkStream stream;
        private volatile bool connected;

        public ProtobufClientWorker(IService server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {

                stream = connection.GetStream();
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
                    AthletesRequest request = AthletesRequest.Parser.ParseDelimitedFrom(stream);
                    logger.Debug("Receipt request " + request + " from client" + connection);
                    AthletesResponse response = HandleRequest(request);
                    if (response != null)
                    {
                        SendResponse(response);
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

        private AthletesResponse HandleRequest(AthletesRequest request)
        {
            AthletesResponse response = null;
            AthletesRequestType type = request.Type;

            switch (type)
            {
                case AthletesRequestType.Login:
                    {

                        logger.Debug("Processing login request by user " + request.User.Id);
                        Model.Admin user = ProtobufUtils.GetAdmin(request);
                        try
                        {
                            lock (server)
                            {
                                server.Login(user.ID, user.Password, this);
                            }
                            return ProtobufUtils.CreateOkResponse();
                        }
                        catch (Error e)
                        {
                            logger.Debug("User " + request.User.Id + " already logged in. Disconnecting...");
                            connected = false;
                            return ProtobufUtils.CreateErrorResponse(e.Message);
                        }
                    }

                case AthletesRequestType.Logout:
                    {
                        logger.Debug("Processing logout request by user " + request.User.Id);
                        Model.Admin user = ProtobufUtils.GetAdmin(request);
                        try
                        {
                            lock (server)
                            {
                                server.Logout(user.ID, this);
                            }
                            connected = false;
                            return ProtobufUtils.CreateOkResponse();

                        }
                        catch (Error e)
                        {
                            return ProtobufUtils.CreateErrorResponse(e.Message);
                        }
                    }

                case AthletesRequestType.GetEvents:
                    {
                        logger.Debug("Processing get events request");
                        try
                        {
                            Model.Event[] events;
                            lock (server)
                            {
                                events = server.GetAllEvents().ToArray();
                            }
                            return ProtobufUtils.CreateListOfEventsResponse(events);
                        }
                        catch (Error e)
                        {
                            return ProtobufUtils.CreateErrorResponse(e.Message);
                        }
                    }

                case AthletesRequestType.CountChildren:
                    {
                        logger.Debug("Processing count children request for event ID " + request.EventID);
                        try
                        {
                            int count;
                            lock (server)
                            {
                                count = server.CountChildren(request.EventID);
                            }
                            return ProtobufUtils.CreateCounterOfChildrenResponse(count);
                        }
                        catch (Error e)
                        {
                            return ProtobufUtils.CreateErrorResponse(e.Message);
                        }
                    }

                case AthletesRequestType.FilterChildren:
                    {
                        //FilterChildrenRequest filterChildrenRequest = (FilterChildrenRequest)request;
                        //string data = filterChildrenRequest.Data;

                        IList<int> parameters = Parser.ToList(request.Data);
                        logger.Debug("Processing filter children request for event ID " + parameters[0]);
                        try
                        {
                            Model.Child[] result;
                            lock (server)
                            {
                                result = server.GetFilteredChildren(parameters[0], parameters[1], parameters[2]).ToArray();
                            }
                            return ProtobufUtils.CreateListOfChildrenResponse(result);
                        }
                        catch (Error e)
                        {
                            return ProtobufUtils.CreateErrorResponse(e.Message);
                        }
                    }

                case AthletesRequestType.SaveChild:
                    {
                        logger.Debug("Processing save request for child ID " + request.Child.Id);
                        try
                        {
                            Model.Child result;
                            lock (server)
                            {
                                result = server.SaveChild(ProtobufUtils.GetChild(request));
                            }
                            if (result != null)
                            {
                                return ProtobufUtils.CreateSavedChildResponse(result);
                            }
                            else
                            {
                                return ProtobufUtils.CreateErrorResponse("Child " + request.Child.Id + " could not be saved");
                            }

                        }
                        catch (Error e)
                        {
                            return ProtobufUtils.CreateErrorResponse(e.Message);
                        }
                    }
            }
            return response;
        }

        private void SendResponse(AthletesResponse response)
        {
            logger.Debug("Sending response " + response);
            lock (response)
            {
                Google.Protobuf.MessageExtensions.WriteDelimitedTo(response, stream);
                stream.Flush();
            }
        }

        public void ChildSaved(Model.Child c)
        {
            logger.Debug("Notification - new child added: " + c.ID);
            try
            {
                SendResponse(ProtobufUtils.CreateNewChildResponse(c));
            }
            catch (Exception e)
            {
                throw new Error(e.Message);
            }
        }
    }
}
