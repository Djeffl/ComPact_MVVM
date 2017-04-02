using System;
using Newtonsoft.Json;

namespace ComPact.Models
{
	public class Task
	{
		[JsonProperty("_id")]
		public string Id { get; set; }
		[JsonProperty("itemName")]
		public string ItemName { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("member")]
		public User Member { get; set; }
		public byte[] Image { get; set; }
		public bool Done { get; set; }
		public override string ToString()
		{
			return string.Format("{0}: {1}", ItemName, Description);
		}

	}
}
