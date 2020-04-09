package Utils;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Properties;

public class DbUtils {
    private Properties jdbcProps;
    private static final Logger logger = LogManager.getLogger();

    public DbUtils(Properties prop) {
        jdbcProps = prop;
    }

    private Connection instance = null;

    private Connection getNewConnection() {

        logger.traceEntry("Making a new connection...");
        String driver = jdbcProps.getProperty("events.db.driver");
        String url = jdbcProps.getProperty("events.db.url");
        String username = jdbcProps.getProperty("events.db.user");
        String password = jdbcProps.getProperty("events.db.password");

        logger.info("trying to connect to database ... {}", url);
        Connection con = null;

        try {
            Class.forName(driver);
            logger.info("Loaded driver ...{}", driver);
            con = DriverManager.getConnection(url, username, password);
        } catch (ClassNotFoundException e) {
            logger.error(e);
            System.out.println("Error loading driver " + e);
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error getting connection " + e);
        }
        return con;
    }

    public Connection getConnection() {
        logger.traceEntry("Getting Connection...");
        try {
            if (instance == null || instance.isClosed())
                instance = getNewConnection();

        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit("Found connection :{}", instance);
        return instance;
    }

}
