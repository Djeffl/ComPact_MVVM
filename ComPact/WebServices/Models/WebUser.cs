using System;
using System.Collections.Generic;
using ComPact.Models;
using Newtonsoft.Json;

namespace ComPact
{
	public class WebUser: WebMember
	{
		[JsonProperty("admin")]
		public bool Admin { get; set; }
		[JsonProperty("loginToken")]
		public string LoginToken { get; set; }
		[JsonProperty("refreshToken")]
		public string RefreshToken { get; set; }
	}
}
