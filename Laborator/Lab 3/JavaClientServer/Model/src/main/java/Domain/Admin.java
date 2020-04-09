package Domain;

public class Admin extends Entity<String> {
    private String name;
    private String password;

    public Admin(String username, String name, String password) {
        super(username);
        this.name = name;
        this.password = password;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }


    @Override
    public String toString() {
        return super.toString() +
                "name='" + name + '\'' +
                ", password='" + password + '\''+'}';
    }
}
