using System;

namespace Networking.Dto
{
	[Serializable]
	public class AdminDto
	{
		private string id;
		private string password;

		public AdminDto(string id) 
            : this(id, "") {}

		public AdminDto(string id, string password)
		{
			this.id = id;
			this.password = password;
		}

		public virtual string ID
		{
			get { return id; }
			set { id = value; }
		}

		public virtual string Password
		{
			get { return password; }
		}
	}

}