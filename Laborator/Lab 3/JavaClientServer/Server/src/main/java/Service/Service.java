package Service;

import Domain.Admin;
import Domain.Child;
import Domain.Event;
import Repository.IRepositoryAdmin;
import Repository.IRepositoryChild;
import Repository.IRepositoryEvent;
import Services.Error;
import Services.IObserver;
import Services.IService;

import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class Service implements IService {
    private IRepositoryAdmin<String, Admin> repositoryAdmin;
    private IRepositoryChild<Integer, Child> repositoryChild;
    private IRepositoryEvent<Integer, Event> repositoryEvent;
    private Map<String, IObserver> loggedAdmins;

    public Service(
            IRepositoryAdmin<String, Admin> repositoryAdmin,
            IRepositoryChild<Integer, Child> repositoryChild,
            IRepositoryEvent<Integer, Event> repositoryEvent) {
        this.repositoryAdmin = repositoryAdmin;
        this.repositoryChild = repositoryChild;
        this.repositoryEvent = repositoryEvent;
        loggedAdmins = new ConcurrentHashMap<>();
    }

    @Override
    public synchronized void login(String userName, String Password, IObserver client) throws Error {
        Admin a = repositoryAdmin.find(userName);
        if (a != null) {
            if (a.getPassword().equals(Password)) {
                if (loggedAdmins.get(userName) != null)
                    throw new Error("User Already Logged in!");
                else {
                    loggedAdmins.put(userName, client);

                }
            }
        } else
            throw new Error("Bad user or password");
    }

    @Override
    public void logout(String userName, IObserver client) throws Error {
        Admin a = repositoryAdmin.find(userName);
        if (a != null)
            if (loggedAdmins.get(userName) != null) {
                loggedAdmins.remove(userName, client);
            }
    }

    @Override
    public synchronized List<Child> getFilteredChildren(int idEvent, int ageMin, int ageMax) {
        return repositoryChild.filter(idEvent, ageMin, ageMax);
    }

    @Override
    public synchronized List<Event> getAllEvents() {
        List<Event> events = repositoryEvent.findAll();
        events.forEach(x -> {
            if (x.getId() != 0)
                x.setNo(repositoryChild.countByEvent(x.getId()));
        });
        return events;
    }

    @Override
    public synchronized Child saveChild(Child c) {
        Child toReturn = repositoryChild.save(c);
        notifyAdd(c);
        return toReturn;

    }

    private void notifyAdd(Child c) {
        ExecutorService executorService = Executors.newFixedThreadPool(5);
        System.out.println("updating for logged users....");
        for (IObserver admLog : loggedAdmins.values()) {
            executorService.execute(() -> {
                try {
                    admLog.childSaved(c);
                } catch (Error error) {
                    System.out.println(error);
                }
            });
        }
        executorService.shutdown();
    }

    @Override
    public synchronized int countChildren(int idEvent) {
        return repositoryChild.countByEvent(idEvent);
    }
}
