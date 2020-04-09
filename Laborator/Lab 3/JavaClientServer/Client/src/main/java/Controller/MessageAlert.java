package Controller;


import javafx.scene.control.Alert;
import javafx.stage.Stage;

public class MessageAlert {


    static void showErrorMessage(Stage owner, String text){
        Alert message=new Alert(Alert.AlertType.ERROR);
        message.initOwner(owner);
        message.setTitle("Error");
        message.setContentText(text);
        message.showAndWait();
    }
}
