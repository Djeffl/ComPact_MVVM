using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComPact.Models
{
	public class WebMember
	{
		[JsonProperty("_id")]
		public string Id { get; set; }
		[JsonProperty("firstName")]
		public string FirstName { get; set; }
		[JsonProperty("lastName")]
		public string LastName { get; set; }
		[JsonProperty("email")]
		public string Email { get; set; }
		[JsonProperty("password")]
		public string Password { get; set; }
		[JsonProperty("adminId")]
		public string AdminId { get; set; }
		[JsonProperty("qrCode")]
		public byte[] QrCode { get; set; }
		[JsonProperty("AssignmentIds")]
		public IEnumerable<string> AssignmentsIds { get; set; }

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
