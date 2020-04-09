package repository.db;

import domain.User;
import domain.validators.UserValidator;
import org.junit.jupiter.api.*;
import utils.config.Iterables;

import java.time.LocalDateTime;

@TestMethodOrder(MethodOrderer.Alphanumeric.class)
class UserDatabaseRepositoryTest {

    private UserDatabaseRepository repository;
    private User toAddBack;

    @BeforeEach
    void setUp() throws Exception {
        repository = new UserDatabaseRepository(new UserValidator());
        toAddBack = new User("root", "alpine");
    }

    @Test
    void findOne() {
    }

    @Test
    @Order(1)
    void findAll() {
        Iterable<User> entities = repository.findAll();
        assert(Iterables.toList(entities).size() == 2);
    }

    @Test
    @Order(2)
    void save() {
        assert(repository.save(new User("root", "alpine")) != null);
    }

    @Test
    @Order(3)
    void delete() {
        assert(repository.delete("root") != null);
        assert(repository.findOne("root") == null);
        assert(repository.save(new User("root", "alpine")) == null);
    }

    @Test
    void update() {
    }
}