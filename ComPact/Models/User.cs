using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace ComPact.Models
{
	public class User: Member
	{
		[JsonProperty("loginToken")]
		public string LoginToken { get; set; }
		[JsonProperty("refreshToken")]
		public string RefreshToken { get; set; }
		[Ignore]
		[JsonProperty("members")]
		public IEnumerable<Member> members { get; set; }
	}
}
