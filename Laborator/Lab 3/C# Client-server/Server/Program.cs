using System;
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
        private static IService service = new ConcurrentService(admins, children, events);

        static void RunObjectServer()
        {
            SerializedServer server = new SerializedServer("127.0.0.1", 55555, service);
            server.Start();
        }

        static void RunRemotingServer()
        {
            ;
        }

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            //BasicConfigurator.Configure();

            RunObjectServer();


            //logger.Info("Server started");
        }
    }
}
