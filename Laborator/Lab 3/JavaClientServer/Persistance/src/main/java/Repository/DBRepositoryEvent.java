package Repository;

import Domain.Event;
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

public class DBRepositoryEvent implements IRepositoryEvent<Integer, Event> {
    private DbUtils dbUtils;
    private static final Logger logger= LogManager.getLogger();

    public DBRepositoryEvent(Properties props) {
        logger.info("Initializing repo props : {}",props);
        this.dbUtils = new DbUtils(props);
    }


    @Override
    public List<Event> findAll() {
        logger.traceEntry("finding all events");
        List<Event> l=new LinkedList<>();
        Connection con= dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("select * from Probes")){
            try(ResultSet result=preStmt.executeQuery()) {

                while (result.next()) {
                    int id = result.getInt("Id");
                    int min=result.getInt("AgeMin");
                    int max=result.getInt("AgeMax");
                    int distance=result.getInt("Distance");
                    Event e=new Event(id,min,max,distance);
                    l.add(e);
                }
            }
        }catch (SQLException ex){
            logger.error(ex);
            System.out.println("Error DB "+ex);
        }
        finally {

            try {
                con.close();
                logger.info("closed");
            } catch (SQLException ex) {
                logger.error(ex);
            }
        }
        if(l.isEmpty())
            logger.traceExit("No event");
        else
            logger.traceExit("Events are {}",l);
        return l;

    }

    @Override
    public Event find(Integer integer) {
        logger.traceEntry("finding event with id {} ",integer);
        Connection con=dbUtils.getConnection();
        Event e=null;
        try(PreparedStatement preStmt=con.prepareStatement("select * from Probes where Id=?")){
            preStmt.setInt(1,integer);
            try(ResultSet result=preStmt.executeQuery()) {

                if (result.next()) {
                    int id = result.getInt("Id");
                    int min=result.getInt("AgeMin");
                    int max=result.getInt("AgeMax");
                    int distance=result.getInt("Distance");
                    e=new Event(id,min,max,distance);
                    logger.traceExit("S-a gasit evntul {}",e);
                }
            }
        }catch (SQLException ex){
            logger.error(ex);
            System.out.println("Error DB "+ex);
        }
        finally {

            try {
                con.close();
                logger.info("closed");
            } catch (SQLException ex) {
                logger.error(ex);
            }
        }
        if(e.equals(null))
             logger.traceExit("No event found with id {}", integer);
        return e;
    }


}
