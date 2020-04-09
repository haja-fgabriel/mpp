package repository.db;


import domain.User;
import domain.validators.Validator;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class UserDatabaseRepository extends DatabaseRepository<String, User> {
    public UserDatabaseRepository(Validator<String, User> validator) {
        super(validator);
    }

    @Override
    public User findOne(String s) {
        if (s == null)
            throw new IllegalArgumentException("argument is null");
        try {
            PreparedStatement statement = connection.prepareStatement("select * from RegisteredUsers where Name=?");
            statement.setString(1, s);
            ResultSet result = statement.executeQuery();
            if (result.next()) {
                return new User(result.getString(1), result.getString(2));
            } else {
                LOGGER.info("Value not found");
                return null;
            }
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        return null;
    }

    @Override
    public Iterable<User> findAll() {
        List<User> probes = new ArrayList<>();
        Statement statement = null;
        try {
            statement = connection.createStatement();
            ResultSet result = statement.executeQuery("select * from RegisteredUsers");
            while (result.next()) {
                probes.add(new User(result.getString(1), result.getString(2)));
            }
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        return probes;
    }

    @Override
    public User save(User entity) {
        if (entity == null)
            throw new IllegalArgumentException("entity is null");
        try {
            PreparedStatement statement = connection.prepareStatement("insert into RegisteredUsers(Name, Password) values(?,?)");
            statement.setString(1, entity.getName());
            statement.setString(2, entity.getPassword());
            if (statement.executeUpdate() > 0)
                return null;
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        User probe = findOne(entity.getName());
        if (probe == null)
            throw new RuntimeException("the evil magic, again...");
        return probe;
    }

    @Override
    public User delete(String s) {
        if (s == null)
            throw new IllegalArgumentException("argument is null");
        User probe = findOne(s);
        try {
            PreparedStatement statement = connection.prepareStatement("delete from RegisteredUsers where Name=?");
            statement.setString(1, s);
            if (statement.executeUpdate() > 0) {
                LOGGER.info("Update successful");
                return probe;
            }
            else
                return null;
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        return null;
    }

    @Override
    public User update(User entity) {
        if (entity == null)
            throw new IllegalArgumentException("entity is null");
        try {
            PreparedStatement statement = connection.prepareStatement("update RegisteredUsers set Password=? where Name=?");
            statement.setString(2, entity.getPassword());
            statement.setString(1, entity.getName());
            if (statement.executeUpdate() > 0) {
                LOGGER.info("Update successful.");
                return null;
            }
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        User probe = findOne(entity.getName());
        if (probe != null) {
            LOGGER.warn("The evil magic, again... God knows what's happening");
            throw new RuntimeException("the evil magic, again...");
        }
        return probe;
    }

}
