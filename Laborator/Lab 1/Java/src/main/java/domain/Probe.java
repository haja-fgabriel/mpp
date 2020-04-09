package domain;

import java.time.LocalDateTime;
import java.util.Objects;

public class Probe extends Entity<String> {
    private ProbeType type;
    private LocalDateTime date;
    private AgeCategory ageCategory;

    public Probe(String name, ProbeType type, LocalDateTime date, AgeCategory ageCategory) {
        super.setId(name);
        this.type = type;
        this.date = date;
        this.ageCategory = ageCategory;
    }

    public String getName() {
        return getId();
    }

    public void setName(String name) {
        setId(name);
    }

    public ProbeType getType() {
        return type;
    }

    public void setType(ProbeType type) {
        this.type = type;
    }

    public LocalDateTime getDate() {
        return date;
    }

    public void setDate(LocalDateTime date) {
        this.date = date;
    }

    public AgeCategory getAgeCategory() {
        return ageCategory;
    }

    public void setAgeCategory(AgeCategory ageCategory) {
        this.ageCategory = ageCategory;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Probe probe = (Probe) o;
        return type == probe.type &&
                date.equals(probe.date);
    }

    @Override
    public int hashCode() {
        return Objects.hash(type, date);
    }

    @Override
    public String toString() {
        return null;
    }
}
