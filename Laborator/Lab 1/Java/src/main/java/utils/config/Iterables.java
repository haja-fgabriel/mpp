package utils.config;

import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

public class Iterables {

    public static List<?> toList(Iterable<?> entities) {
        return StreamSupport.stream(entities.spliterator(), false)
                .collect(Collectors.toList());
    }
}
