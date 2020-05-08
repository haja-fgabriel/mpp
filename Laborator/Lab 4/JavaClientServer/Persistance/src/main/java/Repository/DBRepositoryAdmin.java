package Repository;

import Domain.Admin;
import Utils.DbUtils;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Properties;

public class DBRepositoryAdmin implements IRepositoryAdmin<String, Admin> {
    private DbUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();

    public DBRepositoryAdmin(Properties props) {
        this.dbUtils = new DbUtils(props);
    }

    private void testSelect() {
        logger.traceEntry("Testing select clause...");
        Connection con = dbUtils.getConnection();
        try (PreparedStatement statement = con.prepareStatement("select * from registeredusers")) {
            try (ResultSet result = statement.executeQuery()) {
                if (result.next())
                    do {
                        logger.trace("found username " + result.getString("Name"));
                    } while (result.next());
            }
        } catch (SQLException ex) {
            logger.error(ex);
            System.out.println("Error DB " + ex);
        } finally {
            try {
                con.close();
                logger.info("closed");
            } catch (SQLException ex) {
                logger.error(ex);
            }
        }

    }

    @Override
    public Admin find(String username) {
        logger.traceEntry("finding admin with username {} ", username);
        //testSelect();
        Connection con = dbUtils.getConnection();
        Admin e = null;
        try (PreparedStatement preStmt = con.prepareStatement("select * from registeredusers where Username=?")) {
            preStmt.setString(1, username);
            try (ResultSet result = preStmt.executeQuery()) {
                if (result.next()) {
                    String Username = result.getString("Username");
                    String name = result.getString("Name");
                    String password = result.getString("Password");
                    e = new Admin(Username, name, password);
                    logger.traceExit("Adminul este {}", e);
                }
            }
        } catch (SQLException ex) {
            logger.error(ex);
            System.out.println("Error DB " + ex);
        } finally {
            try {
                con.close();
                logger.info("closed");
            } catch (SQLException ex) {
                logger.error(ex);
            }
        }

        if (e == null)
            logger.traceExit("No admin found with username {}", username);
        return e;
    }
}
