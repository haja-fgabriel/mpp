package protobuf;

import Domain.Admin;
import Domain.Child;
import Domain.Event;
import Services.Error;
import Services.IObserver;
import Services.IService;
import dto.AdminDto;
import dto.ChildDto;
import dto.DtoUtils;
import rpcprotocol.*;
import utils.Parser;

import java.io.*;
import java.net.Socket;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

public class ProtobufProxy implements IService {


    private String host;
    private int port;

    private IObserver client;

    private InputStream input;
    private OutputStream output;
    private Socket connection;

    private BlockingQueue<AthletesResponseOuterClass.AthletesResponse> qResponses;
    private volatile boolean finished;

    public ProtobufProxy(String host, int port) throws Error {
        this.host = host;
        this.port = port;
        qResponses = new LinkedBlockingQueue<>();
    }

    @Override
    public void login(String userName, String Password, IObserver client) throws Error {
        initializeConnection();

        sendRequest(ProtobufUtils.createLoginRequest(new Admin(userName, "", Password)));
        AthletesResponseOuterClass.AthletesResponse response = readResponse();
        if (response.getType() == AthletesResponseOuterClass.AthletesResponse.ResponseType.Ok) {
            this.client = client;
            return;
        }
        if (response.getType() == AthletesResponseOuterClass.AthletesResponse.ResponseType.Error) {
            String message = response.getError();
            closeConnection();
            throw new Error(message);
        }
    }

    @Override
    public void logout(String userName, IObserver client) throws Error {
        sendRequest(ProtobufUtils.createLogoutRequest(new Admin(userName, "", "")));
        AthletesResponseOuterClass.AthletesResponse response = readResponse();
        closeConnection();
        if (response.getType() == AthletesResponseOuterClass.AthletesResponse.ResponseType.Error) {
            throw new Error(response.getError());
        }

    }

    @Override
    public List<Child> getFilteredChildren(int idEvent, int ageMin, int ageMax) throws Error {
        sendRequest(ProtobufUtils.createGetFilteredChildrenRequest(Parser.ToString(idEvent, ageMin, ageMax)));
        AthletesResponseOuterClass.AthletesResponse response = readResponse();
        if (response.getType() == AthletesResponseOuterClass.AthletesResponse.ResponseType.Error) {
            throw new Error(response.getError());
        }
        return ProtobufUtils.getListOfChildren(response);
    }

    @Override
    public List<Event> getAllEvents() throws Error {
        sendRequest(ProtobufUtils.createGetAllEventsRequest());
        AthletesResponseOuterClass.AthletesResponse response = readResponse();
        if (response.getType() == AthletesResponseOuterClass.AthletesResponse.ResponseType.Error) {
            throw new Error(response.getError());
        }
        return ProtobufUtils.getListOfEvents(response);
    }

    @Override
    public Child saveChild(Child c) throws Error {
        sendRequest(ProtobufUtils.createSaveChildRequest(c));
        AthletesResponseOuterClass.AthletesResponse response = readResponse();
        if (response.getType() == AthletesResponseOuterClass.AthletesResponse.ResponseType.Error) {
            throw new Error(response.getError());
        }
        return ProtobufUtils.getChild(response);
    }

    @Override
    public int countChildren(int idEvent) throws Error {
        sendRequest(ProtobufUtils.createCountOfChildrenRequest(idEvent));
        AthletesResponseOuterClass.AthletesResponse response = readResponse();
        if (response.getType() == AthletesResponseOuterClass.AthletesResponse.ResponseType.Error) {
            throw new Error(response.getError());
        }
        return response.getChildCount();
    }

    private void handleUpdate(AthletesResponseOuterClass.AthletesResponse response) {
        Child c = ProtobufUtils.getChild(response);
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
                    //Object response = input.readObject();
                    AthletesResponseOuterClass.AthletesResponse response = AthletesResponseOuterClass.AthletesResponse.parseDelimitedFrom(input);
                    System.out.println("response received " + response);
                    //Response response1 = (Response) response;

                    if (response.getType() == AthletesResponseOuterClass.AthletesResponse.ResponseType.NewChild) {
                        handleUpdate(response);
                    } else {

                        try {
                            qResponses.put(response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException e) {
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

    private void sendRequest(AthletesRequestOuterClass.AthletesRequest request) throws Error {
        try {
            request.writeDelimitedTo(output);
            output.flush();
        } catch (IOException e) {
            throw new Error("Error sending object " + e);
        }

    }

    private AthletesResponseOuterClass.AthletesResponse readResponse() throws Error {
        AthletesResponseOuterClass.AthletesResponse response = null;
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
            output = connection.getOutputStream();
            //output.flush();
            input = connection.getInputStream();
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
