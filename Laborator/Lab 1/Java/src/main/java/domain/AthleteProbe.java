package domain;

import com.sun.tools.javac.util.Pair;

public class AthleteProbe extends Entity<Pair<String, String>> {
    public AthleteProbe(String athlete, String probe) {
        setId(Pair.of(athlete, probe));
    }

    public String getAthlete() {
        return getId().fst;
    }

    public String getProbe() {
        return getId().snd;
    }

    @Override
    public String toString() {
        return "AthleteProbe{}";
    }
}
