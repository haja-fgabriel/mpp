package Controller;

import Domain.Admin;
import Domain.Child;
import Domain.Event;
import Services.Error;
import Services.IObserver;
import Services.IService;
import com.sun.glass.ui.Menu;
import com.sun.glass.ui.PlatformFactory;
import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.Node;
import javafx.scene.control.ChoiceBox;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.stage.Stage;

import java.net.URL;
import java.util.Arrays;
import java.util.List;
import java.util.Random;
import java.util.ResourceBundle;

public class MenuController implements Initializable, IObserver {
    private IService server;
    private Admin user;
    @FXML
    TableView<Event> TableEvent;
    @FXML
    TableColumn<Event, Integer> Id;
    @FXML
    TableColumn<Event, Integer> Max;
    @FXML
    TableColumn<Event, Integer> Min;
    @FXML
    TableColumn<Event, Integer> Dist;
    @FXML
    ChoiceBox<Event> ChoiceEvent;
    @FXML
    ChoiceBox<Event> ChoiceEvent1;
    @FXML
    ChoiceBox<Event> ChoiceEvent2;
    @FXML
    TextField TextAge;
    @FXML
    TextField TextName;
    @FXML
    TableView<Child> TableCopil;
    @FXML
    TableColumn<Child, Integer> ColVarsta;
    @FXML
    TableColumn<Child, String> ColNume;
    @FXML
    TableColumn<Event, Integer> TableNo;
    private Event e;
    ObservableList<Event> events = FXCollections.observableArrayList();


    @Override
    public void childSaved(Child c) {
        System.out.println("am ajuns!!!!");
        Platform.runLater(() -> {
            TableEvent.getItems().clear();
            List<Event> list = events;
            list.forEach(x -> {
                if (x.getId() != 0) {
                    if (x.getId() == c.getIdEvent1() || x.getId() == c.getIdEvent2())
                        x.setNo(x.getNo() + 1);
                    TableEvent.getItems().add(x);
                }
            });
            System.out.println("reajuns!!!!");
            if (e != null)
                if (c.getIdEvent2() == e.getId() || c.getIdEvent1() == e.getId())
                    TableCopil.getItems().add(c);
        });

    }

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        initTable();
    }

    private void initTable() {
        Max.setCellValueFactory(new PropertyValueFactory<Event, Integer>("ageMax"));
        Min.setCellValueFactory(new PropertyValueFactory<Event, Integer>("ageMin"));
        Dist.setCellValueFactory(new PropertyValueFactory<Event, Integer>("distance"));
        TableNo.setCellValueFactory(new PropertyValueFactory<Event, Integer>("no"));
        Id.setCellValueFactory(new PropertyValueFactory<Event, Integer>("id"));
        ColVarsta.setCellValueFactory(new PropertyValueFactory<Child, Integer>("age"));
        ColNume.setCellValueFactory(new PropertyValueFactory<Child, String>("name"));

    }

    public void handleSearch(ActionEvent actionEvent) {
        e = ChoiceEvent.getSelectionModel().getSelectedItem();
        if (e != null)
            setListOfChildren(e);
        else
            MessageAlert.showErrorMessage(null, "Select a Event");
    }

    private void setListOfChildren(Event e) {
        try {
            TableCopil.getItems().clear();
            List<Child> children = server.getFilteredChildren(e.getId(), e.getAgeMin(), e.getAgeMax());
            for (Child c : children) {
                TableCopil.getItems().add(c);
            }
        } catch (Error error) {
            MessageAlert.showErrorMessage(null, error.getMessage());
        }
    }

    public void handleAdd(ActionEvent actionEvent) {
        Event e1 = ChoiceEvent1.getSelectionModel().getSelectedItem();
        Event e2 = ChoiceEvent2.getSelectionModel().getSelectedItem();
        String nume = TextName.getText();
        Integer age = Integer.parseInt(TextAge.getText());
        if (e1 != null && e2 != null && nume != null) {
            try {
                System.out.println("Intra pe proxy");
                server.saveChild(new Child(generateId(), nume, age, e1.getId(), e2.getId()));
            } catch (Error error) {
                MessageAlert.showErrorMessage(null, error.getMessage());
            }
        } else
            MessageAlert.showErrorMessage(null, "Invalid intput");
    }

    public void handleLogout(ActionEvent actionEvent) {
        logout();
        ((Node) (actionEvent.getSource())).getScene().getWindow().hide();

    }

    public void setService(IService srv) {
        server = srv;
    }

    void logout() {
        try {
            server.logout(user.getId(), this);
        } catch (Error e) {
            System.out.println("Logout error " + e);
        }

    }

    public void setUser(Admin crtUser) {
        user = crtUser;
    }

    public void setListOfEvents() {
        try {

            List<Event> events = server.getAllEvents();
            this.events = FXCollections.observableArrayList(events);
            ChoiceEvent1.setItems(FXCollections.observableArrayList(events));
            ChoiceEvent2.setItems(FXCollections.observableArrayList(events));
            TableEvent.getItems().clear();
            for (Event e : events) {
                if (e.getId() != 0) {
                    TableEvent.getItems().add(e);
                    ChoiceEvent.getItems().add(e);
                }
            }
            if (TableEvent.getItems().size() > 0)
                TableEvent.getSelectionModel().select(0);
        } catch (Error e) {
            MessageAlert.showErrorMessage(null, e.getMessage());
        }
    }

    private int generateId() {
        Random r = new Random();
        return r.nextInt(99999);
    }
}
