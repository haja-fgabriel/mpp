package protobuf;

import Domain.Child;
import Domain.Event;
import Services.Error;
import Services.IObserver;
import Services.IService;

import java.util.List;

public class ProtobufServerProxy implements IService {

    

    @Override
    public void login(String userName, String Password, IObserver client) throws Error {

    }

    @Override
    public void logout(String userName, IObserver client) throws Error {

    }

    @Override
    public List<Child> getFilteredChildren(int idEvent, int ageMin, int ageMax) throws Error {
        return null;
    }

    @Override
    public List<Event> getAllEvents() throws Error {
        return null;
    }

    @Override
    public Child saveChild(Child c) throws Error {
        return null;
    }

    @Override
    public int countChildren(int idEvent) throws Error {
        return 0;
    }
}
