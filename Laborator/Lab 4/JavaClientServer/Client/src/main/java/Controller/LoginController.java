package Controller;

import Domain.Admin;
import Services.Error;
import Services.IService;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;
import javafx.stage.WindowEvent;

import java.io.IOException;
import java.net.URL;

public class LoginController {

    IService server;
    private MenuController Ctrl;
    private Admin crtUser;
    @FXML
    TextField TextUsername;
    @FXML
    PasswordField TextPassword;

    Parent mainChatParent;

    public void setServer(IService s) {
        server = s;
    }


    public void setParent(Parent p) {
        mainChatParent = p;
    }

    public void handleLogin(ActionEvent actionEvent) {
        //Parent root;
        String nume = TextUsername.getText();
        String passwd = TextPassword.getText();
        crtUser = new Admin(nume, " ", passwd);


        try {
            server.login(nume, passwd, Ctrl);
            Stage stage = new Stage();
            stage.setTitle("Logged in as:  " + crtUser.getId());
            stage.setScene(new Scene(mainChatParent));

            stage.setOnCloseRequest(new EventHandler<WindowEvent>() {
                @Override
                public void handle(WindowEvent event) {
                    Ctrl.logout();
                    System.exit(0);
                }
            });

            stage.show();
            Ctrl.setUser(crtUser);
            Ctrl.setListOfEvents();
            ((Node) (actionEvent.getSource())).getScene().getWindow().hide();

        } catch (Error e) {
            MessageAlert.showErrorMessage(null, e.getMessage());
        }


    }

    public void setController(MenuController chatController) {
        this.Ctrl = chatController;
    }
}
