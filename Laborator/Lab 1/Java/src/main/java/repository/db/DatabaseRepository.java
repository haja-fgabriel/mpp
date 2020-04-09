package repository.db;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import utils.config.DatabaseProperties;
import domain.Entity;
import domain.validators.Validator;
import repository.InMemoryRepository;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public abstract class DatabaseRepository<ID, E extends Entity<ID>> extends InMemoryRepository<ID, E> {

    private static String getProperty(String property) {
        return DatabaseProperties.getProperties().getProperty(property);
    }

    protected static final Logger LOGGER = LogManager.getLogger(DatabaseRepository.class);

    protected String driver =  getProperty("jdbc.mysql.driver");
    protected String databasePath = getProperty("jdbc.mysql.url");
    protected String user = getProperty("jdbc.mysql.user");
    protected String password = getProperty("jdbc.mysql.password");
    protected Connection connection;

    protected void createConnection() {
        try {
            Class.forName(driver);
            connection = DriverManager.getConnection(databasePath, user, password);
            LOGGER.info("Connected to database.");
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public DatabaseRepository(Validator<ID, E> validator) {
        super(validator);
        createConnection();
    }

    @Override
    public abstract E findOne(ID id);

    @Override
    public abstract Iterable<E> findAll();

    @Override
    public abstract E save(E entity);

    @Override
    public abstract E delete(ID id);

    @Override
    public abstract E update(E entity);
}
