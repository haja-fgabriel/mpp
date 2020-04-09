package utils.config;

import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.net.URISyntaxException;
import java.nio.file.Paths;
import java.util.Properties;

public class DatabaseConfig {
    private static String CONFIG_LOCATION;
    public static Properties getProperties() {
        if (CONFIG_LOCATION == null) {
            try {
                CONFIG_LOCATION = Paths.get(DatabaseConfig.class.getClassLoader()
                        .getResource("database.properties").toURI()).toAbsolutePath().toString();
            } catch (URISyntaxException e) {
                e.printStackTrace();
            }
        }

        Properties properties = new Properties();
        try {
            properties.load(new FileReader(CONFIG_LOCATION));
            return properties;
        } catch (FileNotFoundException e) {
            throw new RuntimeException("Properties file not found at location " + CONFIG_LOCATION);
        } catch (IOException e) {
            throw new RuntimeException("Properties file could not be loaded at location " + CONFIG_LOCATION);
        }
    }
}
