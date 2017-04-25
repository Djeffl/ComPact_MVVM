using System;
using ComPact.Models;
using Newtonsoft.Json;

namespace ComPact
{
	public class WebPayment
	{
		[JsonProperty("_id")]
		public string Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("price")]
		public double Price { get; set; }
		[JsonProperty("adminId")]
		public string AdminId { get; set; }
		[JsonProperty("memberId")]
		public string MemberId { get; set; }
		[JsonProperty("createdAt")]
		public DateTime CreatedAt { get; set; }
		[JsonProperty("image")]
		public Image Image { get; set; }

		public override string ToString()
		{
			return string.Format("[WebPayment: Id={0}, Name={1}, Description={2}, Price={3}, AdminId={4}, MemberId={5}, CreatedAt={6}, Image={7}]", Id, Name, Description, Price, AdminId, MemberId, CreatedAt, Image);
		}
	}
}
