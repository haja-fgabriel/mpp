package repository.db;

import com.sun.tools.javac.util.Pair;
import domain.AthleteProbe;
import domain.validators.Validator;

import java.sql.*;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.OffsetDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.TimeZone;

public class AthleteProbeDatabaseRepository extends DatabaseRepository<Pair<String, String>, AthleteProbe> {
    public AthleteProbeDatabaseRepository(Validator<Pair<String, String>, AthleteProbe> validator) {
        super(validator);
    }

    @Override
    public AthleteProbe findOne(Pair<String, String> s) {
        if (s == null)
            throw new IllegalArgumentException("argument is null");
        try {
            PreparedStatement statement = connection.prepareStatement("select * from AthletesProbes where Athlete=? and Probe=?");
            statement.setString(1, s.fst);
            statement.setString(2, s.snd);

            ResultSet result = statement.executeQuery();
            if (result.next()) {
                return new AthleteProbe(result.getString(1), result.getString(2));
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
    public Iterable<AthleteProbe> findAll() {
        List<AthleteProbe> probes = new ArrayList<>();
        Statement statement = null;
        try {
            statement = connection.createStatement();
            ResultSet result = statement.executeQuery("select * from AthletesProbes");
            while (result.next()) {
                probes.add(new AthleteProbe(result.getString(1), result.getString(2)));
            }
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        return probes;
    }

    @Override
    public AthleteProbe save(AthleteProbe entity) {
        if (entity == null)
            throw new IllegalArgumentException("entity is null");
        try {
            PreparedStatement statement = connection.prepareStatement("insert into AthletesProbes(Athlete, Probe) values(?,?)");
            statement.setString(1, entity.getAthlete());
            statement.setString(2, entity.getProbe());
            if (statement.executeUpdate() > 0)
                return null;
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        AthleteProbe probe = findOne(entity.getId());
        if (probe == null)
            throw new RuntimeException("the evil magic, again...");
        return probe;
    }

    @Override
    public AthleteProbe delete(Pair<String, String> s) {
        if (s == null)
            throw new IllegalArgumentException("argument is null");
        AthleteProbe probe = findOne(s);
        try {
            PreparedStatement statement = connection.prepareStatement("delete from AthletesProbes where Athlete=? and Probe=?");
            statement.setString(1, s.fst);
            statement.setString(2, s.snd);
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
    public AthleteProbe update(AthleteProbe entity) {
        if (entity == null)
            throw new IllegalArgumentException("entity is null");
        try {
            PreparedStatement statement = connection.prepareStatement("update AthletesProbes where Athlete=? and Probe=?");
            statement.setString(1, entity.getAthlete());
            statement.setString(2, entity.getProbe());
            if (statement.executeUpdate() > 0) {
                LOGGER.info("Update successful.");
                return null;
            }
        } catch (SQLException e) {
            LOGGER.warn(e.getMessage());
        }
        AthleteProbe probe = findOne(entity.getId());
        if (probe != null) {
            LOGGER.warn("The evil magic, again... God knows what's happening");
            throw new RuntimeException("the evil magic, again...");
        }
        return probe;
    }

}
