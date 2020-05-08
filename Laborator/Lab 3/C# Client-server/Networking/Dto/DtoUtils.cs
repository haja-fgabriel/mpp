using System;
using Model;

namespace Networking.Dto
{
    public class DtoUtils
    {
        public static Admin GetFromDto(AdminDto dto)
        {
            string id = dto.ID;
            string pass = dto.Password;
            return new Admin(id, pass);
        }

        public static AdminDto GetDto(Admin admin)
        {
            string id = admin.ID;
            string pass = admin.Password;
            return new AdminDto(id, pass);
        }

        public static Child GetFromDto(ChildDto dto)
        {
            return new Child(dto.ID, dto.Name, dto.Age, dto.IdEvent1, dto.IdEvent2);
        }

        public static ChildDto GetDto(Child child)
        {
            return new ChildDto(child.ID, child.Name, child.Age, child.IdEvent1, child.IdEvent2);
        }

        public static Child[] GetFromDto(ChildDto[] children)
        {
            Child[] result = new Child[children.Length];

            for (int i = 0; i < children.Length; i++)
            {
                ChildDto c = children[i];
                result[i] = new Child(c.ID, c.Name, c.Age, c.IdEvent1, c.IdEvent2);
            }

            return result;
        }

        public static ChildDto[] GetDto(Child[] children)
        {
            ChildDto[] result = new ChildDto[children.Length];

            for (int i = 0; i < children.Length; i++)
            {
                Child c = children[i];
                result[i] = new ChildDto(c.ID, c.Name, c.Age, c.IdEvent1, c.IdEvent2);
            }

            return result;
        }

        public static Event[] GetFromDto(EventDto[] events)
        {
            Event[] result = new Event[events.Length];

            for (int i = 0; i < events.Length; i++)
            {
                EventDto ev = events[i];
                result[i] = new Event(ev.ID, ev.AgeMin, ev.AgeMax, ev.Distance, ev.No);
            }

            return result;
        }

        public static EventDto[] GetDto(Event[] events)
        {
            EventDto[] result = new EventDto[events.Length];

            for (int i = 0; i < events.Length; i++)
            {
                Event ev = events[i];
                result[i] = new EventDto(ev.ID, ev.AgeMin, ev.AgeMax, ev.Distance, ev.No);
            }

            return result;
        }

        //public static Message GetFromDto(MessageDTO mdto)
        //{
        //    User sender = new User(mdto.SenderId);
        //    User receiver = new User(mdto.ReceiverId);
        //    string text = mdto.Text;
        //    return new Message(sender, receiver, text);

        //}

        //public static MessageDTO GetDto(Message message)
        //{
        //    string senderId = message.Sender.Id;
        //    string receiverId = message.Receiver.Id;
        //    string txt = message.Text;
        //    return new MessageDTO(senderId, txt, receiverId);
        //}

        //public static AdminDto[] GetDto(User[] users)
        //{
        //    AdminDto[] frDTO = new AdminDto[users.Length];
        //    for (int i = 0; i < users.Length; i++)
        //    {
        //        frDTO[i] = GetDto(users[i]);
        //    }
        //    return frDTO;
        //}

        //public static User[] GetFromDto(AdminDto[] users)
        //{
        //    User[] friends = new User[users.Length];
        //    for (int i = 0; i < users.Length; i++)
        //    {
        //        friends[i] = getFromDTO(users[i]);
        //    }
        //    return friends;
        //}
    }

}