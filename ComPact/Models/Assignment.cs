using System;
using Newtonsoft.Json;
using SQLite;

namespace ComPact.Models
{
	public class Assignment
	{
		[JsonProperty("_id")]
		public string Id { get; set; }
		[JsonProperty("itemName")]
		public string ItemName { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("member")]
		public string AdminId { get; set; }
		//public Member Member { get; set; }
		//public byte[] Image { get; set; }
		public bool Done { get; set; }
		public override string ToString()
		{
			return string.Format("{0}: {1}", ItemName, Description);
		}

	}
}
