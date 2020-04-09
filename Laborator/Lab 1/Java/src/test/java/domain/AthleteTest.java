package domain;

import jdk.vm.ci.meta.Local;
import org.junit.jupiter.api.*;

import javax.sound.sampled.FloatControl;
import java.time.LocalDateTime;
import java.time.temporal.ChronoUnit;
import java.util.ArrayList;
import java.util.Arrays;

@TestInstance(TestInstance.Lifecycle.PER_CLASS)
public class AthleteTest {
    private Athlete athlete;
    private Athlete toAddBack;

    @BeforeEach
    public void setUp() throws Exception {
        athlete = new Athlete("Florin", LocalDateTime.of(1999,04,12,11,30));
    }

    @Test
    public void getName() {
        System.out.println(athlete.getName());
    }

    @Test
    public void getBirthDate() {
        System.out.println(athlete.getBirthDate());
    }

    @Test
    public void setBirthDate() {
        athlete.setBirthDate(LocalDateTime.of(1999,04,12,23,59));
    }

    @Test
    public void computeAge() {
        assert(athlete.computeAge() == athlete.getBirthDate().until(LocalDateTime.now(), ChronoUnit.YEARS));
    }

    @Test
    public void addProbe() {
    }

    @AfterAll
    public void tearDown() {

    }
}
