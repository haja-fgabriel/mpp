using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Repository;
using Persistence.Repository.Database;
using Persistence.Repository.EntityFramework;
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
    class Program
    {
        private static ILog logger = LogManager.GetLogger(typeof(Program));
        private static IRepositoryAdmin<string, Admin> admins;
        private static IRepositoryChild<int, Child> children;
        private static IRepositoryEvent<int, Event> events;

        private static ConcurrentService service;
        private static BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
        private static BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();

        private static void PrepareEntityFrameworkRepositories()
        {
            admins = new EntityFrameworkRepositoryAdmin();
            children = new EntityFrameworkRepositoryChild();
            events = new EntityFrameworkRepositoryEvent();
        }

        private static void PrepareRepositories()
        {
            admins = new DbRepositoryAdmin(ConfigurationManager.AppSettings);
            children = new DbRepositoryChild(ConfigurationManager.AppSettings);
            events = new DbRepositoryEvent(ConfigurationManager.AppSettings);
        }

        private static void RunObjectServer()
        {
            SerializedServer server = new SerializedServer("127.0.0.1", 55555, service);
            server.Start();
        }

        private static void RunRemotingServer()
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

        private static void RunProtobufServer()
        {
            ProtobufSerializedServer server = new ProtobufSerializedServer("127.0.0.1", 55555, service);
            server.Start();
        }

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            //BasicConfigurator.Configure();

            PrepareEntityFrameworkRepositories();

            service = new ConcurrentService(admins, children, events);

            //RunObjectServer();
            //RunRemotingServer();
            RunProtobufServer();

            //logger.Info("Server started");
        }
    }
}
