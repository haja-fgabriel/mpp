package rpcprotocol;

import Services.IService;
import AbsConcurrentServer;

import java.net.Socket;


public class RpcConcurrentServer extends AbsConcurrentServer {
    private IService server;

    public RpcConcurrentServer(int port, IService server) {
        super(port);
        this.server = server;
        System.out.println("RpcConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {
        ClientRpcReflexionWorker worker = new ClientRpcReflexionWorker(server, client);

        Thread tw = new Thread(worker);
        return tw;
    }

    @Override
    public void stop() {
        System.out.println("Stopping services ...");
    }
}
