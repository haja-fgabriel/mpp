using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Networking
{
    public abstract class AbstractServer
    {
        private static ILog logger = LogManager.GetLogger(typeof(AbstractServer));

        private TcpListener server;
        private String host;
        private int port;

        public AbstractServer(String host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public void Start()
        {
            logger.Debug("Starting server...");
            IPAddress adr = IPAddress.Parse(host);
            IPEndPoint ep = new IPEndPoint(adr, port);

            server = new TcpListener(ep);
            server.Start();
            logger.Info("Server started.");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                logger.Info("Accepted new client");
                ProcessRequest(client);
            }
        }

        public abstract void ProcessRequest(TcpClient client);
    }
}
