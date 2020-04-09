package service;

import domain.*;
import repository.InMemoryRepository;
import repository.db.AthleteDatabaseRepository;
import repository.db.AthleteProbeDatabaseRepository;
import repository.db.ProbeDatabaseRepository;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.logging.Logger;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

public class AthleteProbeService {

    private static Logger LOGGER = Logger.getLogger(AthleteProbeService.class.getName());

    private AthleteDatabaseRepository athleteRepository;
    private AthleteProbeDatabaseRepository athleteProbeRepository;
    private ProbeDatabaseRepository probeRepository;

    public AthleteProbeService(AthleteDatabaseRepository athleteRepository, AthleteProbeDatabaseRepository athleteProbeRepository, ProbeDatabaseRepository probeRepository) {
        this.athleteRepository = athleteRepository;
        this.athleteProbeRepository = athleteProbeRepository;
        this.probeRepository = probeRepository;
    }

    public Iterable<AthleteProbe> getAllAthletesProbes() {
        return athleteProbeRepository.findAll();
    }

    public Athlete addAthlete(String name, LocalDateTime birthDate) {
        // TODO complete code
        try{
            if (athleteRepository.save(new Athlete(name, birthDate)) == null) {
                LOGGER.info("athlete added successfully");
            }
        } catch (Exception e) {
            LOGGER.warning(e.getMessage());
        }
        return null;
    }

    public AthleteProbe registerAthleteToProbe(String athlete, String probe) {
        // TODO complete code
        try {
            if (athleteProbeRepository.save(new AthleteProbe(athlete, probe)) == null) {
                LOGGER.info("athlete successfully registered to probe");
                // TODO code for successfully registering new Athlete entity
            }
        } catch (Exception e) {
            LOGGER.warning(e.getMessage());
        }
        return null;
    }

    public Iterable<Athlete> searchAthletes(String probe, AgeCategory category) {
        Map<String, Athlete> athletes = new HashMap<>();
        StreamSupport.stream(athleteProbeRepository.findAll().spliterator(), false)
                .filter(athleteProbe -> (athleteProbe.getProbe().equals(probe)
                        && probeRepository.findOne(probe).getAgeCategory() == category))
                .forEach(athleteProbe -> {
                    String athlete = athleteProbe.getAthlete();
                    if (athletes.get(athlete) == null) {
                        athletes.put(athlete, athleteRepository.findOne(athlete));
                    }
                });
        return athletes.values();
    }

    public Probe addProbe(String name, ProbeType type, LocalDateTime date, AgeCategory ageCategory) {
        // TODO complete code
        try {
            if (probeRepository.save(new Probe(name, type, date, ageCategory)) == null) {
                LOGGER.info("probe added successfully");
                // TODO code for success in adding a new probe;
            }
        } catch (Exception e) {
            LOGGER.warning(e.getMessage());
        }
        return null;
    }

}
