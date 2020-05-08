package rpcprotocol;

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
import java.net.Socket;
import java.util.Arrays;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class ServerProxy implements IService {

    private String host;
    private int port;

    private IObserver client;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private Socket connection;

    private BlockingQueue<Response> qResponses;
    private volatile boolean finished;

    public ServerProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qResponses = new LinkedBlockingQueue<Response>();
    }

    @Override
    public void login(String userName, String Password, IObserver client) throws Error {
        initializeConnection();

        AdminDto adminDto = new AdminDto(userName, Password);
        Request req = new Request.Builder().type(RequestType.LOGIN).data(adminDto).build();
        sendRequest(req);
        Response response = readResponse();
        if (response.type() == ResponseType.OK) {
            this.client = client;
            return;
        }
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            closeConnection();
            throw new Error(err);
        }
    }

    @Override
    public void logout(String userName, IObserver client) throws Error {
        AdminDto adminDto = new AdminDto(userName);
        Request req = new Request.Builder().type(RequestType.LOGOUT).data(adminDto).build();
        sendRequest(req);
        Response response = readResponse();
        closeConnection();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new Error(err);
        }
    }

    @Override
    public List<Child> getFilteredChildren(int idEvent, int ageMin, int ageMax) throws Error {
        String str = Parser.ToString(idEvent, ageMin, ageMax);
        Request request = new Request.Builder().type(RequestType.FILTER_CHILDREN).data(str).build();
        sendRequest(request);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new Error(err);
        }
        ChildDto[] childDtos = (ChildDto[]) response.data();
        Child[] children = DtoUtils.getFromDTO(childDtos);
        List<Child> toReturn = Arrays.asList(children);
        return toReturn;
    }

    @Override
    public List<Event> getAllEvents() throws Error {
        Request request = new Request.Builder().type(RequestType.GET_EVENTS).build();
        sendRequest(request);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new Error(err);
        }
        EventDto[] eventDtos = (EventDto[]) response.data();
        Event[] events = DtoUtils.getFromDTO(eventDtos);
        List<Event> toReturn = Arrays.asList(events);
        return toReturn;
    }

    @Override
    public Child saveChild(Child c) throws Error {
        System.out.println("Welcome from proxy");
        ChildDto childDto = DtoUtils.getDTO(c);
        Request request = new Request.Builder().type(RequestType.SAVE_CHILD).data(childDto).build();
        System.out.println("sending request " + request);
        sendRequest(request);
        Response response = readResponse();
        System.out.println("got response " + response);
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new Error(err);
        }
        Child child = DtoUtils.getFromDTO((ChildDto) response.data());
        return child;
    }

    @Override
    public int countChildren(int idEvent) throws Error {
        Request request = new Request.Builder().type(RequestType.COUNT_CHILDREN).data(idEvent).build();
        sendRequest(request);
        Response response = readResponse();
        if (response.type() == ResponseType.ERROR) {
            String err = response.data().toString();
            throw new Error(err);
        }
        return (Integer) response.data();
    }

    private void handleUpdate(Response response) {
        ChildDto childDto = (ChildDto) response.data();
        Child c = DtoUtils.getFromDTO(childDto);
        System.out.println("Child was saved " + c);
        try {
            System.out.println("doing update...");
            client.childSaved(c);
        } catch (Error e) {
            e.printStackTrace();
        }
    }


    private class ReaderThread implements Runnable {
        public void run() {
            while (!finished) {
                try {
                    Object response = input.readObject();
                    System.out.println("response received " + response);
                    Response response1 = (Response) response;
                    if (response1.type() == ResponseType.NEW_CHILD) {
                        handleUpdate(response1);
                    } else {

                        try {
                            qResponses.put((Response) response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException | ClassNotFoundException e) {
                    System.out.println("Reading error " + e);
                }
            }
        }
    }

    private void closeConnection() {
        finished = true;
        try {
            input.close();
            output.close();
            connection.close();
            client = null;
        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    private void sendRequest(Request request) throws Error {
        try {
            output.writeObject(request);
            output.flush();
        } catch (IOException e) {
            throw new Error("Error sending object " + e);
        }

    }

    private Response readResponse() throws Error {
        Response response = null;
        try {

            response = qResponses.take();

        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }

    private void initializeConnection() throws Error {
        try {
            connection = new Socket(host, port);
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input = new ObjectInputStream(connection.getInputStream());
            finished = false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void startReader() {
        Thread tw = new Thread(new ReaderThread());
        tw.start();
    }
}

