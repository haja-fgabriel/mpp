using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;
using ChatServices;
using Hashtable=System.Collections.Hashtable;

namespace ChatClient
{
    static class StartChatClient
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
            IDictionary props = new Hashtable();

            props["port"] = 0;
            TcpChannel channel = new TcpChannel(props, clientProv, serverProv);
            ChannelServices.RegisterChannel(channel, false);
            IChatServices services =
                 (IChatServices)Activator.GetObject(typeof(IChatServices), "tcp://localhost:55555/Chat");
          
            ChatClientCtrl ctrl=new ChatClientCtrl(services);
            LoginWindow win=new LoginWindow(ctrl);
            Application.Run(win);
        }
    }
}
