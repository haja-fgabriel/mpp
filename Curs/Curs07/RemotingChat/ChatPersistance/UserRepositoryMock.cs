using System;
using System.Collections.Generic;
using ChatModel;

namespace ChatPersistance
{
    namespace repository.mock
    {
    public class UserRepositoryMock: UserRepository{
    private readonly IDictionary<String, User> allUsers;

    public UserRepositoryMock() {
        allUsers=new Dictionary<String, User>();
        populateUsers();
    }
    public bool verifyUser(User user) {
        User userR=allUsers[user.Id];
        if (userR==null)
            return false;
        return userR.Password==user.Password;
    }

    public User[] getFriends(User user) {
        User userR=allUsers[user.Id];
        if (userR!=null)
            return userR.Friends;
        return new User[0];
    }

    private void populateUsers(){
        User ana=new User("ana", "ana", "Popescu Ana");
        User mihai=new User("mihai", "mihai", "Ionescu Mihai");
        User ion=new User("ion", "ion", "Vasilescu Ion");
        User maria=new User("maria", "maria", "Marinescu Maria");
        User test=new User("test", "test", "Test user");

        ana.addFriend(mihai);
        ana.addFriend(test);

        mihai.addFriend(ana);
        mihai.addFriend(test);
        mihai.addFriend(ion);

        ion.addFriend(maria);
        ion.addFriend(test);
        ion.addFriend(mihai);

        maria.addFriend(ion);
        maria.addFriend(test);

        test.addFriend(ana);
        test.addFriend(mihai);
        test.addFriend(ion);
        test.addFriend(maria);

        allUsers[ana.Id]=ana;
        allUsers[mihai.Id]= mihai;
        allUsers[ion.Id]= ion;
        allUsers[maria.Id]= maria;
        allUsers[test.Id]= test;


    }



        }
    }
}
