package domain;

import java.time.LocalDateTime;
import java.time.temporal.ChronoUnit;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

public class Athlete extends Entity<String> {
    private LocalDateTime birthDate;

    public Athlete(String name, LocalDateTime birthDate) {
        setId(name);
        this.birthDate = birthDate;
    }

    public String getName() {
        return getId();
    }

    public void setName(String name) {
        setId(name);
    }

    public LocalDateTime getBirthDate() {
        return birthDate;
    }

    public void setBirthDate(LocalDateTime birthDate) {
        this.birthDate = birthDate;
    }

    public long computeAge() {
        return getBirthDate().until(LocalDateTime.now(), ChronoUnit.YEARS);
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Athlete athlete = (Athlete) o;
        return getName().equals(athlete.getName());
    }


    @Override
    public String toString() {
        return "Athlete{" + "name=" + getId() +", " +
                "birthDate=" + birthDate +
                '}';
    }
}
