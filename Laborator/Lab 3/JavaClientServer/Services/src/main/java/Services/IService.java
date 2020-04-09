package Services;

import Domain.Child;
import Domain.Event;

import java.util.List;

public interface IService {
    void login(String userName, String Password,IObserver client) throws Error;
    void logout(String userName,IObserver client) throws Error;
    List<Child> getFilteredChildren(int idEvent,int ageMin,int ageMax) throws Error;
    List<Event> getAllEvents() throws Error;
    Child saveChild(Child c) throws Error;
    int countChildren(int idEvent) throws Error;
}
