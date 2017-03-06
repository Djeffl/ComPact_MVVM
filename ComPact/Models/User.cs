using System;
namespace ComPact
{
	public class User: BaseModel
	{
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public bool Admin { get; set; }

		/**
		 * Params id, firstname, lastname, email, password
		 */
		public User(string id, string firstName, string lastName, string email, string password, bool admin)
		{
			UserId = id;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Password = password;
			Admin = admin;
		}
		public User(){ }
		private string fullName()
		{
			return FirstName + " " + LastName;
		}
		public override string ToString()
		{
			return fullName();
		}
	}
}
