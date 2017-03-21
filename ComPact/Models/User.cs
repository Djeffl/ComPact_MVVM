using System;
using SQLite.Net.Attributes;

namespace ComPact
{
	public class User: BaseModel
	{

		///// <summary>
		///// List of base 64-encoded images
		///// </summary>
		///// <value>The images.</value>
		//[Newtonsoft.Json.JsonProperty("images")]
		//public List<string> Images { get; set; }
		//[Newtonsoft.Json.JsonProperty("approved")]
		//public int Approved { get; set; }

		[PrimaryKey]
		[Newtonsoft.Json.JsonProperty("_id")]
		public string UserId { get; set; }
		[Ignore]
		[Newtonsoft.Json.JsonProperty("firstName")]
		public string FirstName { get; set; }
		[Ignore]
		[Newtonsoft.Json.JsonProperty("lastName")]
		public string LastName { get; set; }
		[Ignore]
		[Newtonsoft.Json.JsonProperty("email")]
		public string Email { get; set; }
		[Ignore]
		[Newtonsoft.Json.JsonProperty("password")]
		public string Password { get; set; }
		[Ignore]
		[Newtonsoft.Json.JsonProperty("admin")]
		public bool? Admin { get; set; }
		[Ignore]
		[Newtonsoft.Json.JsonProperty("qrCode")]
		public byte[] QrCode { get; set; }

		/**
		 * Params id, firstname, lastname, email, password
		 */
		public User(string id, string firstName, string lastName, string email, string password, bool? admin)
		{
			UserId = id;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Password = password;
			Admin = admin;
		}
		public User()
		{
		}
		private string fullName()
		{
			return FirstName + " " + LastName;
		}
		//public override string ToString()
		//{
		//	return fullName();
		//}
		public override string ToString()
		{
			return string.Format("[User: UserId={0}, FirstName={1}, LastName={2}, Email={3}, Password={4}, Admin={5}, QrCode={6}]", UserId, FirstName, LastName, Email, Password, Admin, QrCode);
		}
	}
}
