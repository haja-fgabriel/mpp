import Domain.Admin;
import Domain.Child;
import Domain.Event;
import Repository.*;
import Service.Service;
import Services.IService;

import rpcprotocol.RpcConcurrentServer;
import utils.ServerException;

import java.io.IOException;
import java.util.Properties;


public class StartServer {
    private static int defaultPort = 55555;
    private static Properties serverProps = new Properties();

    public static void main(String[] args) {
        if (loadProperties())
            return;

        IRepositoryEvent<Integer, Event> event = new DBRepositoryEvent(serverProps);
        IRepositoryChild<Integer, Child> child = new DBRepositoryChild(serverProps);
        IRepositoryAdmin<String, Admin> admin = new DBRepositoryAdmin(serverProps);
        IService serverImpl = new Service(admin, child, event);
        int serverPort = defaultPort;

        try {
            serverPort = Integer.parseInt(serverProps.getProperty("server.port"));
        } catch (NumberFormatException nef) {
            System.err.println("Wrong  Port Number" + nef.getMessage());
            System.err.println("Using default port " + defaultPort);
        }

        System.out.println("Starting server on port: " + serverPort);

        AbstractServer server = new RpcConcurrentServer(serverPort, serverImpl);
        try {
            server.start();
        } catch (ServerException e) {
            System.err.println("Error starting the server" + e.getMessage());
        } finally {
            try {
                server.stop();
            } catch (ServerException e) {
                System.err.println("Error stopping server " + e.getMessage());
            }
        }
    }

    private static boolean loadProperties() {
        try {
            serverProps.load(StartServer.class.getResourceAsStream("/server.properties"));
            System.out.println("Server properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find server.properties " + e);
            return true;
        }
        return false;
    }
}
