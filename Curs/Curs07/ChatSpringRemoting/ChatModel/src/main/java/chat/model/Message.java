package chat.model;

import java.io.Serializable;

public class Message implements Serializable{
    private User sender, receiver;
    private String text;

    public Message(User sender, String text, User receiver) {
        this.sender = sender;
        this.text = text;
        this.receiver = receiver;
    }

    public User getSender() {
        return sender;
    }

    public User getReceiver() {
        return receiver;
    }

    public String getText() {
        return text;
    }

    @Override
    public String toString() {
        return "Message{" +
                "sender=" + sender.getId() +
                ", receiver=" + receiver.getId() +
                ", text='" + text + '\'' +
                '}';
    }
}
