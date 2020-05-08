using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Services;
using Persistence.Repository;
using log4net;

namespace Server.Service
{
    public class ConcurrentService : IService
    {
        private IRepositoryAdmin<string, Admin> admins;
        private IRepositoryChild<int, Child> children;
        private IRepositoryEvent<int, Event> events;
        private ConcurrentDictionary<String, IObserver> loggedAdmins;

        private static ILog logger = LogManager.GetLogger(typeof(ConcurrentService));

        public ConcurrentService(
            IRepositoryAdmin<string, Admin> admins, 
            IRepositoryChild<int, Child> children, 
            IRepositoryEvent<int, Event> events)
        {
            this.admins = admins;
            this.children = children;
            this.events = events;
            loggedAdmins = new ConcurrentDictionary<String, IObserver>();
            logger.Debug("Created new ConcurrentService instance");
        }

        public int CountChildren(int idEvent)
        {
            return children.CountByEvent(idEvent);
        }

        public List<Event> GetAllEvents()
        {
            return events.FindAll().Select(x =>
            {
                if (x.ID != 0)
                    x.No = children.CountByEvent(x.ID);
                return x;
            }).ToList();
        }

        public List<Child> GetFilteredChildren(int idEvent, int ageMin, int ageMax)
        {
            return children.Filter(idEvent, ageMin, ageMax);
        }

        public void Login(string username, string password, IObserver client)
        {
            logger.Debug("Trying to log in with username " + username);

            Admin a = admins.Find(username);

            if (a != null)
            {
                if (a.Password.Equals(password))
                {
                    if (loggedAdmins.ContainsKey(username))
                        throw new Error("User already logged in");
                    else
                    {
                        logger.Info("User " + username + " logged in successfully");
                        loggedAdmins[username] = client;
                    }
                }
                else
                {
                    logger.Debug("Throwing error for username " + username);
                    throw new Error("Invalid password for username " + username);
                }
            }
            else
            {
                logger.Debug("Throwing error for username " + username);
                throw new Error("Invalid username");
            }
        }

        public void Logout(string username, IObserver client)
        {
            logger.Debug("Trying to log out " + username);
            Admin a = admins.Find(username);

            if (a != null)
            {
                IObserver returned;
                if (loggedAdmins.TryRemove(username, out returned))
                {
                    logger.Debug("User " + username + " successfully logged out");
                }
                else
                {
                    logger.Debug("User " + username + " was already logged out");
                }
            }
        }

        public Child SaveChild(Child c)
        {
            Child toReturn = children.Save(c);
            notifyAdd(c);
            return toReturn;
        }

        private void notifyAdd(Child c)
        {
            loggedAdmins.ToList().ForEach(adm =>
            {
                adm.Value.ChildSaved(c);
            });
        }
    }
}