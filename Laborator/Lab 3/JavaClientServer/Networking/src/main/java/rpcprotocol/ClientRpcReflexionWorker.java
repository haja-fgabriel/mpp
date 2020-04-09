package rpcprotocol;

import Domain.Admin;
import Domain.Child;
import Domain.Event;
import Services.Error;
import Services.IObserver;
import Services.IService;
import dto.AdminDto;
import dto.ChildDto;
import dto.DtoUtils;
import dto.EventDto;
import utils.Parser;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.net.Socket;
import java.util.List;

public class ClientRpcReflexionWorker implements Runnable, IObserver {
    private IService server;
    private Socket connection;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private volatile boolean connected;

    private static Response okResponse = new Response.Builder().type(ResponseType.OK).build();

    public ClientRpcReflexionWorker(IService server, Socket connection) {
        this.server = server;
        this.connection = connection;
        try {
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input = new ObjectInputStream(connection.getInputStream());
            connected = true;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }


    @Override
    public void childSaved(Child c) throws Error {
        ChildDto childDto = DtoUtils.getDTO(c);
        System.out.println("Notifing user...");
        Response response = new Response.Builder().type(ResponseType.NEW_CHILD).data(childDto).build();
        try {
            sendResponse(response);
        } catch (IOException e) {
            throw new Error("Sending error: " + e);
        }
    }

    @Override
    public void run() {
        while (connected) {
            try {
                Object request = input.readObject();
                Response response = handleRequest((Request) request);
                if (response != null) {
                    sendResponse(response);
                }
            } catch (IOException | ClassNotFoundException e) {
                e.printStackTrace();
            }
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            System.out.println("Error " + e);
        }
    }

    private void sendResponse(Response response) throws IOException {
        System.out.println("sending response " + response);
        synchronized (output) {
            output.writeObject(response);
            output.flush();
        }
    }

    private Response handleRequest(Request request) {
        Response response = null;
        String handlerName = "handle" + (request).type();
        System.out.println("HandlerName " + handlerName);
        try {
            Method method = this.getClass().getDeclaredMethod(handlerName, Request.class);
            response = (Response) method.invoke(this, request);
            System.out.println("Method " + handlerName + " invoked");
        } catch (NoSuchMethodException | InvocationTargetException | IllegalAccessException e) {
            e.printStackTrace();
        }
        return response;
    }

    private Response handleLOGIN(Request request) {
        System.out.println("Login request ..." + request.type());
        AdminDto adminDto = (AdminDto) request.data();
        Admin admin = DtoUtils.getFromDTO(adminDto);
        try {
            server.login(admin.getId(), admin.getPassword(), this);
            return okResponse;
        } catch (Error e) {
            connected = false;
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleLOGOUT(Request request) {
        System.out.println("Logout request...");
        AdminDto adminDto = (AdminDto) request.data();
        Admin user = DtoUtils.getFromDTO(adminDto);
        try {
            server.logout(user.getId(), this);
            connected = false;
            return okResponse;

        } catch (Error e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleSAVE_CHILD(Request request) {
        System.out.println("SavingChildRequest...");

        try {
            ChildDto childDto = (ChildDto) request.data();
            Child c = DtoUtils.getFromDTO(childDto);
            if (server.saveChild(c) != null) {
                System.out.println("Sending response....");
                return new Response.Builder().type(ResponseType.SAVED_CHILD).data(childDto).build();
            } else
                return new Response.Builder().type(ResponseType.ERROR).data("Not saved...").build();
        } catch (Error e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }

    }

    private Response handleGET_EVENTS(Request request) {
        System.out.println("FindingAllEvents...");
        try {
            Event[] events = new Event[server.getAllEvents().size()];
            server.getAllEvents().toArray(events);
            EventDto[] eventDtos = DtoUtils.getDTO(events);
            return new Response.Builder().type(ResponseType.LIST_OF_EVENTS).data(eventDtos).build();
        } catch (Error e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleCOUNT_CHILDREN(Request request) {
        System.out.println("CountingAllChildrenThatGo...");
        int idEv = (Integer) request.data();
        try {
            return new Response.Builder().type(ResponseType.COUNTER_OF_CHILDREN).data(server.countChildren(idEv)).build();
        } catch (Error e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleFILTER_CHILDREN(Request request) {
        System.out.println("FilteringChildren....");
        String req = (String) request.data();
        List<Integer> params = Parser.ToIntList(req);
        try {
            List<Child> children = server.getFilteredChildren(params.get(0), params.get(1), params.get(2));
            Child[] children1 = new Child[children.size()];
            children.toArray(children1);
            ChildDto[] childDtos = DtoUtils.getDTO(children1);
            return new Response.Builder().type(ResponseType.LIST_OF_CHILDREN).data(childDtos).build();
        } catch (Error e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }

    }
}
