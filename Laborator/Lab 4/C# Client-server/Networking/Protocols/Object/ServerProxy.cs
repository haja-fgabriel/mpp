using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Services;
using Model;
using Networking.Dto;
using log4net;
using Networking.Utils;

namespace Networking.Protocols.Object
{
    public class ServerProxy : IService
    {
        private static ILog logger = LogManager.GetLogger(typeof(ClientWorker));

        private string host;
        private int port;

        private IObserver client;

        private NetworkStream stream;

        private IFormatter formatter;
        private TcpClient connection;

        private Queue<Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;

        public ServerProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<Response>();
            logger.Debug("Created new ServerProxy instance");
        }

        // TODO finish code for the rest of the methods implemented in service

        public int CountChildren(int idEvent)
        {
            SendRequest(new CountChildrenRequest(idEvent));
            Response response = ReadResponse();

            if (response is ErrorResponse)
            {
                ErrorResponse e = (ErrorResponse)response;
                throw new Error(e.Message);
            }

            return ((CounterOfChildrenResponse)response).Count;
        }

        public List<Event> GetAllEvents()
        {
            SendRequest(new GetEventsRequest());
            Response r = ReadResponse();
            if (r is ErrorResponse)
            {
                ErrorResponse e = (ErrorResponse)r;
                throw new Error(e.Message);
            }
            Event[] events = DtoUtils.GetFromDto(((ListOfEventsResponse)r).Events);

            List<Event> result = new List<Event>();
            for (int i = 0; i < events.Length; i++)
            {
                result.Add(events[i]);
            }

            return result;
        }

        public List<Child> GetFilteredChildren(int idEvent, int ageMin, int ageMax)
        {
            SendRequest(new FilterChildrenRequest(Parser.ToString(idEvent, ageMin, ageMax)));
            Response response = ReadResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse e = (ErrorResponse)response;
                throw new Error(e.Message);
            }

            Child[] listOfChildren = DtoUtils.GetFromDto(((ListOfChildrenResponse)response).Children);
            List<Child> result = new List<Child>();
            for (int i = 0; i < listOfChildren.Length; i++)
            {
                result.Add(listOfChildren[i]);
            }
            return result;
        }

        public void Login(string username, string password, IObserver client)
        {
            InitializeConnection();
            AdminDto udto = new AdminDto(username, password);

            SendRequest(new LoginRequest(udto));
            Response response = ReadResponse();

            if (response is OkResponse)
            {
                this.client = client;
                return;
            }
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                CloseConnection();
                throw new Error(err.Message);
            }
        }

        public void Logout(string username, IObserver client)
        {
            AdminDto udto = new AdminDto(username);
            SendRequest(new LogoutRequest(udto));
            Response response = ReadResponse();

            CloseConnection();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new Error(err.Message);
            }
        }

        public Child SaveChild(Child c)
        {
            ChildDto childDto = DtoUtils.GetDto(c);
            SendRequest(new SaveChildRequest(childDto));
            Response response = ReadResponse();

            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                throw new Error(err.Message);
            }

            SavedChildResponse savedChild = (SavedChildResponse)response;
            return DtoUtils.GetFromDto(savedChild.Child);
        }

        private void CloseConnection()
        {
            finished = true;
            try
            {
                stream.Close();
                //output.close();
                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private void SendRequest(Request request)
        {
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                throw new Error("Error sending object " + e);
            }

        }

        private Response ReadResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        private void InitializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void StartReader()
        {
            Thread tw = new Thread(Run);
            tw.Start();
        }


        private void HandleUpdate(UpdateResponse update)
        {
            if (update is NewChildResponse)
            {
                NewChildResponse response = (NewChildResponse)update;
                // TODO observer code for this
                client.ChildSaved(DtoUtils.GetFromDto(response.Child));
            }
        }

        public virtual void Run()
        {
            while (!finished)
            {
                try
                {
                    object response = formatter.Deserialize(stream);
                    Console.WriteLine("response received " + response);
                    if (response is UpdateResponse)
                    {
                        HandleUpdate((UpdateResponse)response);
                    }
                    else
                    {
                        lock (responses)
                        {
                            responses.Enqueue((Response)response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }

            }
        }
    }
}

