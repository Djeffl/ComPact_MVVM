using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ComPact
{
	public class WebLocation
	{
		[JsonProperty("adminId")]
		public string AdminId { get; set; }
		[JsonProperty("_id")]
		public string Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("city")]
		public string City { get; set; }
		[JsonProperty("streetAndNumber")]
		public string Street { get; set; }
		[JsonProperty("radius")]
		public float Radius { get; set; }
		[JsonProperty("membersIds")]
		public IEnumerable<string> MembersIds { get; set; }
	}
}
