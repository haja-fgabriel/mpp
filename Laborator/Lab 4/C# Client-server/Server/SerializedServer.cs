using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Networking;
using Networking.Protocols.Object;
using Networking.Protocols.Protobuf;
using Services;

namespace Server
{
    public class SerializedServer : ConcurrentServer
    {
        private IService service;
        private ClientWorker worker;

        public SerializedServer(string host, int port, IService service)
            : base(host, port)
        {
            this.service = service;
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            worker = new ClientWorker(service, client);
            return new Thread(new ThreadStart(worker.Run));
        }
    }

    public class ProtobufSerializedServer : ConcurrentServer
    {
        private IService service;
        private ProtobufClientWorker worker;

        public ProtobufSerializedServer(string host, int port, IService service)
            : base(host, port)
        {
            this.service = service;
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            worker = new ProtobufClientWorker(service, client);
            return new Thread(new ThreadStart(worker.Run));
        }
    }
}
