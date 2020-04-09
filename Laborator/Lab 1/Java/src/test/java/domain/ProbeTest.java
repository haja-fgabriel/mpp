package domain;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.TestInstance;

import java.time.LocalDateTime;

@TestInstance(TestInstance.Lifecycle.PER_CLASS)
public class ProbeTest {
    private Probe probe;
    private Probe secondProbe;

    @BeforeEach
    public void setUp() throws Exception {
        probe = new Probe("thermopyle", ProbeType.M1500, LocalDateTime.of(2020,04,12,11,30), null);
        secondProbe = new Probe("thermopyle", ProbeType.M1500, LocalDateTime.of(2020,04,12,11,30), null);
    }

    @Test
    public void testEquals() {
        assert(probe.equals(secondProbe));
    }
}