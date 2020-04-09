package repository.db;

import com.sun.tools.javac.util.Pair;
import domain.AthleteProbe;
import domain.Probe;
import domain.ProbeType;
import domain.validators.AthleteProbeValidator;
import domain.validators.ProbeValidator;
import org.junit.jupiter.api.*;
import utils.config.Iterables;

import java.time.LocalDateTime;

@TestInstance(TestInstance.Lifecycle.PER_CLASS)
@TestMethodOrder(MethodOrderer.Alphanumeric.class)
class AthleteProbeDatabaseRepositoryTest {

    private AthleteProbeDatabaseRepository repository;
    private AthleteProbe toAddBack;

    @BeforeEach
    void setUp() throws Exception {
        repository = new AthleteProbeDatabaseRepository(new AthleteProbeValidator());
        toAddBack = new AthleteProbe("Atodiresei-Chirileanu George-Denis","DornaSuperSprint");
    }

    @Test
    void findOne() {
    }

    @Test
    @Order(1)
    void findAll() {
        Iterable<AthleteProbe> entities = repository.findAll();
        assert(Iterables.toList(entities).size() == 2);
    }

    @Test
    @Order(2)
    void save() {
        assert(repository.save(new AthleteProbe("Atodiresei-Chirileanu George-Denis","DornaSuperSprint")) != null);
        assert(repository.findOne(Pair.of("Atodiresei-Chirileanu George-Denis","DornaSuperSprint")) != null);
    }

    @Test
    @Order(3)
    void delete() {
        assert(repository.delete(Pair.of("Atodiresei-Chirileanu George-Denis","DornaSuperSprint")) != null);
        assert(repository.findOne(Pair.of("Atodiresei-Chirileanu George-Denis","DornaSuperSprint")) == null);
        assert(repository.save(new AthleteProbe("Atodiresei-Chirileanu George-Denis","DornaSuperSprint")) == null);
    }

    @Test
    void update() {
    }
}