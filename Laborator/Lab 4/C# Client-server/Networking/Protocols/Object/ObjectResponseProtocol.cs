using System;
using Model;
using Networking.Dto;

namespace Networking.Protocols.Object
{
    //using UserDTO = chat.network.dto.UserDTO;
    //using MessageDTO = chat.network.dto.MessageDTO;

    public interface Response
    {
    }

    [Serializable]
    public class OkResponse : Response
    {

    }

    [Serializable]
    public class ErrorResponse : Response
    {
        private string message;

        public ErrorResponse(string message)
        {
            this.message = message;
        }

        public virtual string Message
        {
            get { return message; }
        }
    }

    public interface UpdateResponse : Response
    {
    }

    [Serializable]
    public class NewChildResponse : UpdateResponse
    {
        private ChildDto child;

        public NewChildResponse(ChildDto child)
        {
            this.child = child;
        }

        public virtual ChildDto Child
        {
            get { return child; }
        }
    }

    [Serializable]
    public class SavedChildResponse : Response
    {
        private ChildDto child;

        public SavedChildResponse(ChildDto child)
        {
            this.child = child;
        }

        public virtual ChildDto Child
        {
            get { return child; }
        }
    }

    [Serializable]
    public class ListOfEventsResponse : Response
    {
        private EventDto[] events;

        public ListOfEventsResponse(EventDto[] events)
        {
            this.events = events;
        }

        public virtual EventDto[] Events
        {
            get { return events; }
        }
    }

    [Serializable]
    public class ListOfChildrenResponse : Response
    {
        private ChildDto[] children;

        public ListOfChildrenResponse(ChildDto[] children)
        {
            this.children = children;
        }

        public virtual ChildDto[] Children
        {
            get { return children; }
        }
    }

    [Serializable]
    public class CounterOfChildrenResponse : Response
    {
        private int count;

        public CounterOfChildrenResponse(int cnt)
        {
            count = cnt;
        }

        public virtual int Count
        {
            get { return count; }
        }
    }

    // TODO add responses for each of the service implemented methods
}