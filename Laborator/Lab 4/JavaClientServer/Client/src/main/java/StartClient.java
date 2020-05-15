import Controller.LoginController;
import Controller.MenuController;
import Services.IService;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import protobuf.ProtobufProxy;
import rpcprotocol.ServerProxy;

import java.io.IOException;
import java.net.URL;
import java.util.Properties;

public class StartClient extends Application {
    private Stage primaryStage;

    private static int defaultChatPort = 55555;
    private static String defaultServer = "localhost";
    private static Properties clientProps = new Properties();

    public static void main(String[] args) {
        launch(args);
    }

    public void start(Stage primaryStage) throws Exception {
        System.out.println("In start");
        if (loadProperties())
            return;
        String serverIP = clientProps.getProperty("chat.server.host", defaultServer);
        int serverPort = defaultChatPort;

        try {
            serverPort = Integer.parseInt(clientProps.getProperty("chat.server.port"));
        } catch (NumberFormatException ex) {
            System.err.println("Wrong port number " + ex.getMessage());
            System.out.println("Using default port: " + defaultChatPort);
        }
        System.out.println("Using server IP " + serverIP);
        System.out.println("Using server port " + serverPort);

        //IService server = new ServerProxy(serverIP, serverPort);
        IService server = new ProtobufProxy(serverIP, serverPort);

        URL resource = getClass().getClassLoader().getResource("View/Login.fxml");
        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(resource);
        Parent root = loader.load();


        LoginController ctrl =
                loader.<LoginController>getController();
        ctrl.setServer(server);

        URL resource2 = getClass().getClassLoader().getResource("View/MainMenu.fxml");
        FXMLLoader cloader = new FXMLLoader();
        cloader.setLocation(resource2);
        Parent croot = cloader.load();


        MenuController Ctrl =
                cloader.<MenuController>getController();
        Ctrl.setService(server);

        ctrl.setController(Ctrl);
        ctrl.setParent(croot);

        primaryStage.setTitle("Login");
        primaryStage.setScene(new Scene(root, 430, 340));
        primaryStage.show();
    }

    private boolean loadProperties() {
        try {
            clientProps.load(StartClient.class.getResourceAsStream("/client.properties"));
            System.out.println("Client properties set. ");
            clientProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find client.properties " + e);
            return true;
        }
        return false;
    }
}
