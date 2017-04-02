using System;
using Newtonsoft.Json;
using SQLite;

namespace ComPact.Models
{
	public class PersonalUser: User
	{
		[JsonProperty("loginToken")]
		public string LoginToken { get; set; }
		[JsonProperty("refreshToken")]
		public string RefreshToken { get; set; }
	}
}
