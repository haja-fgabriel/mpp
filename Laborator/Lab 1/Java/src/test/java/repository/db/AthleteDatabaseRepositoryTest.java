package repository.db;

import domain.Athlete;
import domain.validators.AthleteValidator;
import org.junit.jupiter.api.*;
import utils.config.DatabaseProperties;
import utils.config.Iterables;

import java.time.LocalDateTime;

@TestInstance(TestInstance.Lifecycle.PER_CLASS)
@TestMethodOrder(MethodOrderer.Alphanumeric.class)
public class AthleteDatabaseRepositoryTest {

    private AthleteDatabaseRepository repository;
    private Athlete toAddBack;

    @BeforeEach
    public void setUp() throws Exception {
        repository = new AthleteDatabaseRepository(new AthleteValidator());
        toAddBack = new Athlete("Bodea Andrei", LocalDateTime.of(1999,02,15,00,04));
    }

    @Test
    public void findOne() {

    }

    @Test
    @Order(1)
    public void findAll() {
        Iterable<Athlete> entities = repository.findAll();
        assert(Iterables.toList(entities).size() == 11);
    }

    @Test
    @Order(2)
    public void save() {
        assert(repository.save(new Athlete("Bodea Andrei", LocalDateTime.of(1998,02,15,00,06))) != null);
    }

    @Test
    @Order(3)
    public void delete() {
        assert(repository.delete("Bodea Andrei") != null);
        assert(repository.findOne("Bodea Andrei") == null);
        assert(repository.save(new Athlete("Bodea Andrei", LocalDateTime.of(1999,02,15,00,06))) == null);
    }

    @Test
    @Order(4)
    public void update() {
        assert(repository.update(new Athlete("Bodea Andrei", LocalDateTime.of(1999, 02, 15, 23,59))) == null);
    }

    @AfterAll
    public void tearDown() {
    }

}