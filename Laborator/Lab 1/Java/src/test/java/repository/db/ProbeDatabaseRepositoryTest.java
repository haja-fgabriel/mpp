package repository.db;

import domain.AgeCategory;
import domain.Probe;
import domain.ProbeType;
import domain.validators.ProbeValidator;
import org.junit.jupiter.api.*;
import utils.config.Iterables;

import java.sql.SQLException;
import java.time.LocalDateTime;

@TestInstance(TestInstance.Lifecycle.PER_CLASS)
@TestMethodOrder(MethodOrderer.Alphanumeric.class)
class ProbeDatabaseRepositoryTest {

    private ProbeDatabaseRepository repository;
    private Probe toAddBack;

    @BeforeEach
    void setUp() throws Exception {
        repository = new ProbeDatabaseRepository(new ProbeValidator());
        toAddBack = new Probe("MaratonGrecia", ProbeType.M1500, LocalDateTime.of(75,03,22,12,01, 45), AgeCategory.AGE_12_TO_15);
    }

    @Test
    @Order(4)
    void findOne() {
        assert(repository.findOne("MaratonGrecia").getAgeCategory().equals(AgeCategory.AGE_12_TO_15));
    }

    @Test
    @Order(1)
    void findAll() {
        Iterable<Probe> entities = repository.findAll();
        assert(Iterables.toList(entities).size() == 3);
    }

    @Test
    @Order(2)
    void save() {
        assert(repository.save(new Probe("MaratonGrecia", ProbeType.M1500, LocalDateTime.of(75,03,22,12,01, 45), AgeCategory.AGE_12_TO_15)) != null);
    }

    @Test
    @Order(3)
    void delete() {
        assert (repository.delete("DornaSuperSprint") == null);
        assert(repository.delete("MaratonGrecia") != null);
        assert(repository.findOne("MaratonGrecia") == null);
        assert(repository.save(new Probe("MaratonGrecia", ProbeType.M1500, LocalDateTime.of(75,03,22,12,01, 45), AgeCategory.AGE_12_TO_15)) == null);
    }

    @Test
    void update() {
    }
}