using System;
using ChatPersistance.repository;
using ChatPersistance.repository.file;

namespace ChatPersistance
{
    public class FilePersistence:Persistence
    {
        public override  UserRepository createUserRepository()
        {
            Console.WriteLine("Creating UserRepositoryTextFile");
            return new UserRepositoryTextFile();
        }
    }
}
