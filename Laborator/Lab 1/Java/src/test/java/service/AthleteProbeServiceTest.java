package service;

import domain.AthleteProbe;
import domain.validators.AthleteProbeValidator;
import domain.validators.AthleteValidator;
import domain.validators.ProbeValidator;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.MethodOrderer;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.TestMethodOrder;
import repository.db.AthleteDatabaseRepository;
import repository.db.AthleteProbeDatabaseRepository;
import repository.db.ProbeDatabaseRepository;
import utils.config.Iterables;

import java.util.List;

@TestMethodOrder(MethodOrderer.Alphanumeric.class)
class AthleteProbeServiceTest {

    private AthleteProbeDatabaseRepository athleteProbeRepository;
    private ProbeDatabaseRepository probeRepository;
    private AthleteDatabaseRepository athleteRepository;

    private AthleteProbeService athleteProbeService;

    @BeforeEach
    void setUp() {
        athleteProbeRepository = new AthleteProbeDatabaseRepository(new AthleteProbeValidator());
        probeRepository = new ProbeDatabaseRepository(new ProbeValidator());
        athleteRepository = new AthleteDatabaseRepository(new AthleteValidator());
        athleteProbeService = new AthleteProbeService(athleteRepository, athleteProbeRepository, probeRepository);
    }

    @Test
    void getAllEntities() {
        List<AthleteProbe> entities = (List<AthleteProbe>) Iterables.toList(athleteProbeService.getAllAthletesProbes());
        assert(entities.size() == 2);
    }

    @Test
    void addAthlete() {
    }

    @Test
    void registerAthleteToProbe() {
    }

    @Test
    void searchAthlete() {
    }

    @Test
    void addProbe() {
    }

    @Test
    void testGetAllEntities() {
    }

    @Test
    void testAddAthlete() {
    }

    @Test
    void testRegisterAthleteToProbe() {
    }

    @Test
    void searchAthletes() {
    }

    @Test
    void testAddProbe() {
    }
}