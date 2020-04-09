package service;

import domain.User;
import repository.db.UserDatabaseRepository;

import java.util.ArrayList;
import java.util.List;

public class UserService {
    private UserDatabaseRepository repository;
    private boolean isLoggedIn;
    private List<User> connectedUsers;

    public UserService(UserDatabaseRepository repository) {
        this.repository = repository;
        connectedUsers = new ArrayList<>();
    }

    public void register(String username, String password) {
        try {
            if (repository.save(new User(username, password)) == null) {
                // TODO code for successful registration;
            }

        } catch (Exception e) {
            ;
        }
    }

    public void login(String username, String password) {
        try {
            User user = repository.findOne(username);
            if (user != null && user.getPassword().equals(password)) {
                connectedUsers.add(user);
                // TODO code for successful login
            }
        } catch (Exception e) {
            ;
        }
    }

    public void logout() {
        ;
    }
}
