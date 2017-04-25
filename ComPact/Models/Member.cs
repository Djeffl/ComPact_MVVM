using System;
using System.Collections.Generic;
using ComPact.Models;
using Newtonsoft.Json;
using SQLite;

namespace ComPact
{
	public class Member
	{

		///// <summary>
		///// List of base 64-encoded images
		///// </summary>
		///// <value>The images.</value>
		//[Newtonsoft.Json.JsonProperty("images")]
		//public List<string> Images { get; set; }
		//[Newtonsoft.Json.JsonProperty("approved")]
		//public int Approved { get; set; }

		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string AdminId { get; set; }
		public byte[] QrCode { get; set; }
		public byte[] ProfilePicture { get; set; }
		public string FullName()
		{
			return FirstName + " " + LastName;
		}

		public override string ToString()
		{
			return FullName();
		}
	}
}
