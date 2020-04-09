package dto;

import java.io.Serializable;

public class EventDto implements Serializable {
    private int id;
    private int ageMin;
    private int ageMax;
    private int distance;
    private int no;

    public EventDto(int id, int ageMin, int ageMax, int distance, int no) {
        this.id = id;
        this.ageMin = ageMin;
        this.ageMax = ageMax;
        this.distance = distance;
        this.no = no;
    }

    public int getId() {
        return id;
    }

    public int getAgeMin() {
        return ageMin;
    }

    public int getAgeMax() {
        return ageMax;
    }

    public int getDistance() {
        return distance;
    }

    public int getNo() {
        return no;
    }
}
