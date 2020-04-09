package dto;

import java.io.Serializable;

public class ChildDto implements Serializable {
    private int id;
    private String name;
    private int age;
    private int IdEvent1;
    private int IdEvent2;

    public ChildDto(int id, String name, int age, int idEvent1, int idEvent2) {
        this.id = id;
        this.name = name;
        this.age = age;
        IdEvent1 = idEvent1;
        IdEvent2 = idEvent2;
    }


    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public int getIdEvent1() {
        return IdEvent1;
    }

    public void setIdEvent1(int idEvent1) {
        IdEvent1 = idEvent1;
    }

    public int getIdEvent2() {
        return IdEvent2;
    }

    public void setIdEvent2(int idEvent2) {
        IdEvent2 = idEvent2;
    }

}
