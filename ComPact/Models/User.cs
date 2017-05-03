using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace ComPact.Models
{
	public class User: Member
	{
		public bool Admin { get; set; }
		public string LoginToken { get; set; }
		public string RefreshToken { get; set; }
	}
}
