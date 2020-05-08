package Repository;

import Domain.Child;
import Utils.DbUtils;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.LinkedList;
import java.util.List;
import java.util.Properties;

public class DBRepositoryChild implements IRepositoryChild<Integer, Child> {
    private DbUtils dbUtils;

    public DBRepositoryChild(Properties props) {
        this.dbUtils = new DbUtils(props);
    }

    private static final Logger logger = LogManager.getLogger();

    @Override
    public Child save(Child entity) {
        logger.traceEntry("saving child :)) {} ", entity);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("insert into People values (?,?,?,?,?)")) {
            preStmt.setInt(1, entity.getId());
            preStmt.setString(2, entity.getName());
            preStmt.setInt(3, entity.getAge());
            preStmt.setInt(4, entity.getIdEvent1());
            preStmt.setInt(5, entity.getIdEvent2());
            int result = preStmt.executeUpdate();
            logger.traceExit("child saved {}", entity);
            return entity;
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
        logger.traceExit("no save");
        return null;

    }

    @Override
    public List<Child> filter(int idEv, int min, int max) {
        logger.traceEntry("finding all children that go to event {} and have ages between {} and {}", idEv, min, max);
        List<Child> l = new LinkedList<>();
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("select * from People where (Probe1=? or Probe2=?) and age >= ? and age <= ?")) {
            preStmt.setInt(1, idEv);
            preStmt.setInt(2, idEv);
            preStmt.setInt(3, min);
            preStmt.setInt(4, max);
            try (ResultSet result = preStmt.executeQuery()) {
                while (result.next()) {
                    Integer id = result.getInt("Id");
                    Integer age = result.getInt("Age");
                    String name = result.getString("Name");
                    Integer ev1 = result.getInt("Probe1");
                    Integer ev2 = result.getInt("Probe2");
                    Child e = new Child(id, name, age, ev1, ev2);
                    l.add(e);
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
        if (l.isEmpty())
            logger.traceExit("No children that match description");
        else
            logger.traceExit("Children are {}", l);
        return l;

    }

    @Override
    public int countByEvent(int idEv) {
        logger.traceEntry("counting all children that go to event {}", idEv);
        int l = 0;
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("select count(*) from People where Probe1=? or Probe2=?")) {
            preStmt.setInt(1, idEv);
            preStmt.setInt(2, idEv);
            try (ResultSet result = preStmt.executeQuery()) {
                if (result.next()) {
                    l = result.getInt(1);
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
        logger.traceExit("Number of elements is:{}", l);
        return l;
    }
}
