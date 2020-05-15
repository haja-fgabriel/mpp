using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Repository;
using Persistence.Repository.Database;
using Model;
using System.Configuration;
using Server.Service;
using Services;
using log4net;
using log4net.Config;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Server
{
    class TestObserver : IObserver
    {
        public void ChildSaved(Child c)
        {
            Console.WriteLine("[DEBUG] Observer notification working\n");
        }
    }

    class OtherTests
    {
        public static void Run(IService service)
        {
            IObserver obs = new TestObserver();
            service.Login("root", "alpine", obs);
            try
            {
                service.Login("root", "alpine", new TestObserver());
            }
            catch (Error e)
            {
                _ = (e.Message);
            }

            service.Logout("root", obs);
            service.Logout("root", obs);
            service.Logout("evilRoot", obs);

            service.CountChildren(1);
        }
    }

    class Program
    {
        private static ILog logger = LogManager.GetLogger(typeof(Program));
        private static IRepositoryAdmin<string, Admin> admins = new DbRepositoryAdmin(ConfigurationManager.AppSettings);
        private static IRepositoryChild<int, Child> children = new DbRepositoryChild(ConfigurationManager.AppSettings);
        private static IRepositoryEvent<int, Event> events = new DbRepositoryEvent(ConfigurationManager.AppSettings);
        private static ConcurrentService service = new ConcurrentService(admins, children, events);
        private static BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
        private static BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();

        static void RunObjectServer()
        {
            SerializedServer server = new SerializedServer("127.0.0.1", 55555, service);
            server.Start();
        }

        static void RunRemotingServer()
        {
            serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            IDictionary properties = new Hashtable();

            properties["port"] = 55555;

            TcpChannel channel = new TcpChannel(properties, clientProvider, serverProvider);
            ChannelServices.RegisterChannel(channel, false);
            RemotingServices.Marshal(service, "Athletes");

            logger.Info("Server started");
            Console.WriteLine("Server started. Press <ENTER> to exit.");
            Console.ReadLine();
        }

        static void RunProtobufServer()
        {
            ProtobufSerializedServer server = new ProtobufSerializedServer("127.0.0.1", 55555, service);
            server.Start();
        }

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            //BasicConfigurator.Configure();

            //RunObjectServer();
            //RunRemotingServer();
            RunProtobufServer();

            //logger.Info("Server started");
        }
    }
}
