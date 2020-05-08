using System;
using Networking.Dto;

namespace Networking.Protocols.Object
{
	public interface Request 
	{
	}

	[Serializable]
	public class LoginRequest : Request
	{
		private AdminDto user;

		public LoginRequest(AdminDto user)
		{
			this.user = user;
		}

		public virtual AdminDto User
		{
			get { return user; }
		}
	}
 
	[Serializable]
	public class LogoutRequest : Request
	{
		private AdminDto user;

		public LogoutRequest(AdminDto user)
		{
			this.user = user;
		}

		public virtual AdminDto User
		{
			get { return user; }
		}
	}

    [Serializable]
    public class SaveChildRequest : Request
    {
        private ChildDto child;

        public SaveChildRequest(ChildDto child)
        {
            this.child = child;
        }

        public virtual ChildDto Child
        {
            get { return child; }
        }
    }

    [Serializable]
    public class CountChildrenRequest : Request
    {
        private int eventID;

        public CountChildrenRequest(int eventID)
        {
            this.eventID = eventID;
        }

        public virtual int EventID
        {
            get { return eventID; }
        }
    }

    [Serializable]
    public class FilterChildrenRequest : Request
    {
        private string data;

        public FilterChildrenRequest(string data)
        {
            this.data = data;
        }

        public virtual string Data
        {
            get { return data; }
        }
    }

    [Serializable]
    public class GetEventsRequest : Request
    {
        public GetEventsRequest()
        {
        }
    }
}