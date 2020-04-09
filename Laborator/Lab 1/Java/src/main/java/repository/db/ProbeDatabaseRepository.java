package repository.db;

import domain.AgeCategory;
import domain.Probe;
import domain.ProbeType;
import domain.validators.Validator;

import java.sql.*;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.OffsetDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.TimeZone;

public class ProbeDatabaseRepository extends DatabaseRepository<String, Probe> {
    public ProbeDatabaseRepository(Validator<String, Probe> validator) {
        super(validator);
    }

    @Override
    public Probe findOne(String s) {
        if (s == null)
            throw new IllegalArgumentException("argument is null");
        try {
            PreparedStatement statement = connection.prepareStatement("select * from probes where Name=?");
            statement.setString(1, s);
            ResultSet result = statement.executeQuery();
            if (result.next()) {
                Timestamp timestamp = result.getTimestamp(3);
                if (timestamp != null) {
                    ProbeType type = ProbeType.valueOf(result.getString(2));
                    AgeCategory ageCategory = AgeCategory.valueOf(result.getString(4));
                    LocalDateTime date = LocalDateTime.ofInstant(Instant.ofEpochMilli(timestamp.getTime()),
                            TimeZone.getDefault().toZoneId());
                    return new Probe(s, type, date, ageCategory);
                } else {
                    LOGGER.info("Invalid timestamp");
                    return null;
                }
            } else {
                LOGGER.warn("Value not found");
                return null;
            }
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        return null;
    }

    @Override
    public Iterable<Probe> findAll() {
        List<Probe> probes = new ArrayList<>();
        Statement statement = null;
        try {
            statement = connection.createStatement();
            ResultSet result = statement.executeQuery("select * from probes");
            while (result.next()) {
                ProbeType type = ProbeType.valueOf(result.getString(2));
                AgeCategory ageCategory = AgeCategory.valueOf(result.getString(4));
                Timestamp timestamp = result.getTimestamp(3);
                if (timestamp != null) {
                    LocalDateTime date = LocalDateTime.ofInstant(Instant.ofEpochMilli(timestamp.getTime()),
                            TimeZone.getDefault().toZoneId());
                    probes.add(new Probe(result.getString(1), type, date, ageCategory));
                }
            }
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        return probes;
    }

    @Override
    public Probe save(Probe entity) {
        if (entity == null)
            throw new IllegalArgumentException("entity is null");
        try {
            PreparedStatement statement = connection.prepareStatement("insert into probes(Name, Type, Date, AgeCategory) values(?,?,?,?)");
            statement.setString(1, entity.getName());
            statement.setString(2, entity.getType().toString());
            statement.setTimestamp(3, Timestamp.from(entity.getDate()
                    .toInstant(OffsetDateTime.now().getOffset())));
            statement.setString(4, entity.getAgeCategory().toString());
            if (statement.executeUpdate() > 0)
                return null;
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        Probe probe = findOne(entity.getName());
        if (probe == null)
            throw new RuntimeException("the evil magic, again...");
        return probe;
    }

    @Override
    public Probe delete(String s) {
        if (s == null)
            throw new IllegalArgumentException("argument is null");
        Probe probe = findOne(s);
        try {
            PreparedStatement statement = connection.prepareStatement("delete from probes where Name=?");
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
    public Probe update(Probe entity) {
        if (entity == null)
            throw new IllegalArgumentException("entity is null");
        try {
            PreparedStatement statement = connection.prepareStatement("update probes set Type=?, Date=?, AgeCategory=? where Name=?");
            statement.setString(4, entity.getName());
            statement.setString(3, entity.getAgeCategory().toString());
            statement.setString(1, entity.getType().toString());
            statement.setTimestamp(2, Timestamp.from(entity.getDate()
                    .toInstant(OffsetDateTime.now().getOffset())));
            if (statement.executeUpdate() > 0) {
                LOGGER.info("Update successful.");
                return null;
            }
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        Probe probe = findOne(entity.getName());
        if (probe != null) {
            LOGGER.warn("The evil magic, again... God knows what's happening");
            throw new RuntimeException("the evil magic, again...");
        }
        return probe;
    }

}
