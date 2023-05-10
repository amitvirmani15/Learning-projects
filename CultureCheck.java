package quartzdemo;
​
import com.gem.automation.util.database.DatabaseUtil;
import org.apache.log4j.LogManager;
import org.apache.log4j.Logger;
​
import java.sql.*;
​
public class CultureCheck {
    static Logger logger = LogManager.getLogger(CultureCheck.class);
    public static void main(String[] args) {
        String connectionUrl = getConnectionURL("172.16.86.215", "2151", "gemqadb");
//        String connectionUrl = DatabaseUtil.getConnectionURL("localhost", "1521", "orcl.vmware.com");
        Connection connection = DatabaseUtil.getConnection(connectionUrl, "GemOwner6", "password");
        Statement statement = null;
​
        try {
            statement = connection.createStatement();
            String sql = "Select * from Culture";
            ResultSet resultSet = statement.executeQuery(sql);
​
            while (resultSet.next()) {
                System.out.println(resultSet.getString(4) + "\t" + resultSet.getString(5));
            }
​
            statement.close();
            connection.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
​
    public static Connection getConnection(final String connectionUrl, String userName, String password) {
        Connection connection = null;
        try {
            Class.forName("oracle.jdbc.driver.OracleDriver");
            connection = DriverManager.getConnection(connectionUrl, userName, password);
            if (connection != null) {
                logger.debug("Connected to GEM database");
                return connection;
            }
        } catch (SQLException e) {
            logger.error("SQL Exception Occured while connecting to GEM DB", e);
​
        } catch (ClassNotFoundException e) {
            logger.error("SQL Exception Occured while loading the driver for GEM DB", e);
        }
        return null;
    }
​
​
    public static String getConnectionURL(String dbhost, String port, String serviceName) {
        return "jdbc:oracle:thin:" + "@" + dbhost + ":" + port + "/" + serviceName;
    }
}