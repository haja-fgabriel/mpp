using System;
using ChatPersistance.repository;
using ChatPersistance.repository.mock;

namespace ChatPersistance
{
    public class MockPersistence/*:Persistence*/
    {
        public /*override*/ UserRepository createUserRepository()
        {
            Console.WriteLine("UserRepositoryMock created");
            return new UserRepositoryMock();
        }
    }
}
