using System;
using Newtonsoft.Json;
using SQLite;

namespace ComPact.Models
{
	public class Assignment
	{
		[PrimaryKey]
		[JsonProperty("_id")]
		public string Id { get; set; }
		[JsonProperty("itemName")]
		public string ItemName { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("loginToken")]
		public string LoginToken { get; set; }
		[JsonProperty("memberEmail")]
		public string MemberEmail { get; set; }
		//public Member Member { get; set; }
		//public byte[] Image { get; set; }
		public bool Done { get; set; }
		public override string ToString()
		{
			return string.Format("{0}: {1}", ItemName, Description);
		}

	}
}
