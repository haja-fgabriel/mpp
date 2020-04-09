package utils.config;

import java.util.Properties;

public class DatabaseProperties {
    private static Properties DATA_PROPERTIES = DatabaseConfig.getProperties();
    public static Properties getProperties() {
        return DATA_PROPERTIES;
    }
}
