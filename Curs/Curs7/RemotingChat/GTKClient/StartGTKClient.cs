using System;
using System.Collections;

using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using ChatServices;
using Hashtable=System.Collections.Hashtable;

using Gtk;

using GTKClient;

namespace ConsoleClient
{
    internal class StartGTKClient
    {
        public static void Main(string[] args)
        {
            BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
            IDictionary props = new Hashtable();

            props["port"] = 0;
            TcpChannel channel = new TcpChannel(props, clientProv, serverProv);
            ChannelServices.RegisterChannel(channel, false);
            IChatServices services =
                (IChatServices)Activator.GetObject(typeof(IChatServices), "tcp://localhost:55555/Chat");
            
            Console.WriteLine("am obtinut referinta!!!");

          
            
            
            Application.Init ();
            GTKClientCtrl ctrl=new GTKClientCtrl(services);
            Window w=new LoginWindow(ctrl);
            // Window w = new ChatWindow();
            w.ShowAll();
            Application.Run ();

            //Console.ReadKey();

        }
    }
}