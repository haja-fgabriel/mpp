
using ChatModel;

namespace ChatServices
{
    public interface IChatServices
    {
         void login(User user, IChatObserver client);
         void sendMessage(Message message);
         void logout(User user, IChatObserver client) ;
         User[] getLoggedFriends(User user);

    }
}
