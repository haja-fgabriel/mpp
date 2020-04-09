using ChatModel;

namespace ChatServices
{
    public interface IChatObserver
    {
         void messageReceived(Message message);
         void friendLoggedIn(User friend);
         void friendLoggedOut(User friend) ;

    }
}
