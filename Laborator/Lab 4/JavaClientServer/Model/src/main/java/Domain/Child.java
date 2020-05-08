package Domain;

public class Child extends Entity<Integer> {
    private String name;
    private int age;
    private int idEvent1;
    private int idEvent2;

    public Child(Integer integer, String name, int age, int idEvent1, int idEvent2) {
        super(integer);
        this.name = name;
        this.age = age;
        this.idEvent1 = idEvent1;
        this.idEvent2 = idEvent2;
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
        return idEvent1;
    }

    public void setIdEvent1(int idEvent1) {
        this.idEvent1 = idEvent1;
    }

    public int getIdEvent2() {
        return idEvent2;
    }

    public void setIdEvent2(int idEvent2) {
        this.idEvent2 = idEvent2;
    }

    @Override
    public String toString() {
        return super.toString() +
                "name='" + name + '\'' +
                ", age=" + age +
                ", IdEvent1=" + idEvent1 +
                ", IdEvent2=" + idEvent2 +
                '}';
    }
}
