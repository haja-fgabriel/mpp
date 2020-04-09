package repository.db;

import domain.Athlete;
import domain.validators.AthleteValidator;

import java.sql.*;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.OffsetDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.TimeZone;

public class AthleteDatabaseRepository extends DatabaseRepository<String, Athlete> {

    public AthleteDatabaseRepository(AthleteValidator validator) {
        super(validator);
    }

    @Override
    public Athlete findOne(String s) {
        if (s == null)
            throw new IllegalArgumentException("argument is null");
        try {
            PreparedStatement statement = connection.prepareStatement("select * from people where name=?");
            statement.setString(1, s);
            ResultSet result = statement.executeQuery();
            if (result.next()) {
                Timestamp timestamp = result.getTimestamp(2);
                if (timestamp != null) {
                    LocalDateTime birthDate = LocalDateTime.ofInstant(Instant.ofEpochMilli(timestamp.getTime()),
                            TimeZone.getDefault().toZoneId());
                    return new Athlete(result.getString(1), birthDate);
                } else {
                    LOGGER.info("Invalid timestamp");
                    return null;
                }
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
    public Iterable<Athlete> findAll() {
        List<Athlete> athletes = new ArrayList<>();
        Statement statement = null;
        try {
            statement = connection.createStatement();
            ResultSet result = statement.executeQuery("select * from people");
            while (result.next()) {
                Timestamp timestamp = result.getTimestamp(2);
                if (timestamp != null) {
                    LocalDateTime birthDate = LocalDateTime.ofInstant(Instant.ofEpochMilli(timestamp.getTime()),
                            TimeZone.getDefault().toZoneId());
                    athletes.add(new Athlete(result.getString(1), birthDate));
                }
            }
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        return athletes;
    }

    @Override
    public Athlete save(Athlete entity) {
        if (entity == null)
            throw new IllegalArgumentException("entity is null");
        try {
            PreparedStatement statement = connection.prepareStatement("insert into people(Name, BirthDate) values(?,?)");
            statement.setString(1, entity.getName());
            statement.setTimestamp(2, Timestamp.from(entity.getBirthDate()
                    .toInstant(OffsetDateTime.now().getOffset())));
            if (statement.executeUpdate() > 0)
                return null;
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        Athlete athlete = findOne(entity.getName());
        if (athlete == null)
            throw new RuntimeException("the evil magic, again...");
        return athlete;
    }

    @Override
    public Athlete delete(String s) {
        if (s == null)
            throw new IllegalArgumentException("argument is null");
        Athlete athlete = findOne(s);
        try {
            PreparedStatement statement = connection.prepareStatement("delete from people where Name=?");
            statement.setString(1, s);
            if (statement.executeUpdate() > 0) {
                LOGGER.info("Update successful");
                return athlete;
            }
            else
                return null;
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        return null;
    }

    @Override
    public Athlete update(Athlete entity) {
        if (entity == null)
            throw new IllegalArgumentException("entity is null");
        try {
            PreparedStatement statement = connection.prepareStatement("update people set BirthDate=? where Name=?");
            statement.setString(2, entity.getName());
            statement.setTimestamp(1, Timestamp.from(entity.getBirthDate()
                    .toInstant(OffsetDateTime.now().getOffset())));
            if (statement.executeUpdate() > 0) {
                LOGGER.info("Update successful.");
                return null;
            }
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        Athlete athlete = findOne(entity.getName());
        if (athlete != null) {
            LOGGER.warn("The evil magic, again... God knows what's happening");
            throw new RuntimeException("the evil magic, again...");
        }
        return athlete;
    }
}
