using System;
using Newtonsoft.Json;
using SQLite;

namespace ComPact
{
	public class User
	{

		///// <summary>
		///// List of base 64-encoded images
		///// </summary>
		///// <value>The images.</value>
		//[Newtonsoft.Json.JsonProperty("images")]
		//public List<string> Images { get; set; }
		//[Newtonsoft.Json.JsonProperty("approved")]
		//public int Approved { get; set; }

		[Ignore]
		[PrimaryKey]
		[JsonProperty("_id")]
		public string Id { get; set; }
		[Ignore]
		[JsonProperty("firstName")]
		public string FirstName { get; set; }
		[Ignore]
		[JsonProperty("lastName")]
		public string LastName { get; set; }
		[Ignore]
		[JsonProperty("email")]
		public string Email { get; set; }
		[JsonProperty("password")]
		public string Password { get; set; }
		[JsonProperty("admin")]
		public bool Admin { get; set; }
		[Ignore]
		[JsonProperty("qrCode")]
		public byte[] QrCode { get; set; }
		[JsonProperty("loginToken")]
		public string LoginToken { get; set; }

		private string FullName()
		{
			return FirstName + " " + LastName;
		}
		//public override string ToString()
		//{
		//	return fullName();
		//}
		public override string ToString()
		{
			return string.Format("[User: Id={0}, FirstName={1}, LastName={2}, Email={3}, Password={4}, Admin={5}, QrCode={6}, LoginToken={7}]", Id, FirstName, LastName, Email, Password, Admin, QrCode, LoginToken);
		}
	}
}
