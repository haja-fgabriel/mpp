using ChatModel;

namespace ChatPersistance
{
    namespace repository
    {
        public interface UserRepository
        {
             bool verifyUser(User user);
              User[] getFriends(User user);

        }

    
    }
    
}
