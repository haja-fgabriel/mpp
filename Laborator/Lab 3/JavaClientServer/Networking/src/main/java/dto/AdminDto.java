package dto;

import java.io.Serializable;

public class AdminDto implements Serializable {

    public String getId() {
        return id;
    }

    public String getPass() {
        return pass;
    }

    private String id;
    private String pass;
    private String name;

    public AdminDto(String id){
        this(id,"","");
    }
    public AdminDto(String id,String pass){
        this(id,pass,"");
    }
    public AdminDto(String id, String pass,String name) {
        this.id = id;
        this.pass = pass;
        this.name=name;
    }

    public String getName() {
        return name;
    }
}
