package protobuf;

import AbsConcurrentServer;

import java.net.Socket;

public class ProtobufConcurrentServer extends AbsConcurrentServer {

    @Override
    protected Thread createWorker(Socket client) {
        return null;
    }
}
