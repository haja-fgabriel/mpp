using System;
using System.Reflection;
using ChatPersistance.repository;

namespace ChatPersistance
{
    public abstract class Persistence
    {
        private static Persistence instance = null;
        public static Persistence getInstance()
        {
            if (instance == null)
            {

                Assembly assem = Assembly.GetExecutingAssembly();
                Type[] types = assem.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof (Persistence)))
                        instance = (Persistence)Activator.CreateInstance(type);
                }
            }
            return instance;
        }

        public abstract UserRepository createUserRepository();
    }
}
