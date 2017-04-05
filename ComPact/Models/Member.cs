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

		[Ignore]
		[JsonProperty("_id")]
		public string Id { get; set; }
		[JsonProperty("firstName")]
		public string FirstName { get; set; }
		[JsonProperty("lastName")]
		public string LastName { get; set; }
		[PrimaryKey]
		[JsonProperty("email")]
		public string Email { get; set; }
		[Ignore]
		[JsonProperty("password")]
		public string Password { get; set; }
		[JsonProperty("admin")]
		public bool Admin { get; set; }
		[Ignore]
		[JsonProperty("qrCode")]
		public byte[] QrCode { get; set; }
		[Ignore]
		public List<Assignment> Tasks { get; set; }
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
