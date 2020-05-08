using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public interface IService
    {
        void Login(string username, string password, IObserver client);
        void Logout(string username, IObserver client);
        List<Child> GetFilteredChildren(int idEvent, int ageMin, int ageMax);
        List<Event> GetAllEvents();
        Child SaveChild(Child c);
        int CountChildren(int idEvent);
    }
}
