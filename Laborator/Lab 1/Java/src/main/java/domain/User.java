package domain;

public class User extends Entity<String> {
    private String password;

    public User(String name, String password) {
        setId(name);
        this.password = password;
    }

    public String getName() {
        return getId();
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    @Override
    public String toString() {
        return "User{name='" + getId() + '\'' +
                "password='" + password + '\'' +
                '}';
    }
}
