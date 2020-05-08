using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Services;

namespace Client
{
    public class ClientController : IObserver
    {
        public event EventHandler<AthleticsEventArgs> updateEvent;
        private readonly IService server;
        private string currentUser;

        public ClientController(IService server)
        {
            this.server = server;
        }

        public void Login(string username, string password)
        {
            server.Login(username, password, this);
            currentUser = username;
        }
        
        public void Logout()
        {
            server.Logout(currentUser, this);
            currentUser = null;
        }

        public List<Child> GetFilteredChildren(int idEvent, int ageMin, int ageMax)
        {
            return server.GetFilteredChildren(idEvent, ageMin, ageMax);
        }

        public List<Event> GetAllEvents()
        {
            return server.GetAllEvents();
        }

        public Child SaveChild(Child c)
        {
            Child result = server.SaveChild(c);

            return result;
            //throw new NotImplementedException();
        }

        public int CountChildren(int idEvent)
        {
            return server.CountChildren(idEvent);
            //throw new NotImplementedException();
        }

        public void ChildSaved(Child c)
        {
            OnEvent(new AthleticsEventArgs(AthleticsEvent.ChildSaved, c));
            //updateEvent.BeginInvoke()
            //throw new NotImplementedException();
        }

        protected virtual void OnEvent(AthleticsEventArgs c)
        {
            if (updateEvent == null)
                return;

            updateEvent(this, c);
        }
    }
}
