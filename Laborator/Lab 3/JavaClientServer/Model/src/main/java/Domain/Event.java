package Domain;

public class Event extends Entity<Integer> {
    public Event(int id, int ageMin, int ageMax, int distance) {
        super(id);
        this.ageMin = ageMin;
        this.ageMax = ageMax;
        this.distance = distance;
        this.no = 0;
    }

    public Event(Integer integer, int ageMin, int ageMax, int distance, int no) {
        super(integer);
        this.ageMin = ageMin;
        this.ageMax = ageMax;
        this.distance = distance;
        this.no = no;
    }

    public int getAgeMin() {
        return ageMin;
    }

    public void setAgeMin(int ageMin) {
        this.ageMin = ageMin;
    }

    public int getAgeMax() {
        return ageMax;
    }

    public void setAgeMax(int ageMax) {
        this.ageMax = ageMax;
    }

    public int getDistance() {
        return distance;
    }

    public void setDistance(int distance) {
        this.distance = distance;
    }

    private int ageMin;
    private int ageMax;
    private int distance;
    private int no;

    @Override
    public String toString() {
        return "Event{" +
                "ageMin=" + ageMin +
                ", ageMax=" + ageMax +
                ", distance=" + distance +
                '}';
    }

    public int getNo() {
        return no;
    }

    public void setNo(int no) {
        this.no = no;
    }
}
