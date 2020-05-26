using Client.Forms;
using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Networking.Protocols.Object;
using Services;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Client
{
    static class Program
    {
        private static BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
        private static BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();

        private static IService server;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        private static void ConnectToRemotingServer()
        {
            IDictionary properties = new Hashtable();
            properties["port"] = 0;

            TcpChannel channel = new TcpChannel(properties, clientProvider, serverProvider);
            ChannelServices.RegisterChannel(channel, false);
            server = (IService)Activator.GetObject(typeof(IService), "tcp://localhost:55555/Athletes");

        }

        private static void ConnectToObjectServer()
        {
            server = new ServerProxy("127.0.0.1", 55555);
        }

        [STAThread]
        static void Main()
        {
            // TODO replace with remoting server

            ConnectToRemotingServer();

            ClientController controller = new ClientController(server);

                
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm(controller));
        }
    }
}
