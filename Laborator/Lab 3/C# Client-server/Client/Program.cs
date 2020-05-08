using Client.Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Networking.Protocols.Object;
using Services;

namespace Client
{
    static class Program
    { 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            IService server = new ServerProxy("127.0.0.1", 55555);
            // TODO replace with remoting server

            ClientController controller = new ClientController(server);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm(controller));
        }
    }
}
