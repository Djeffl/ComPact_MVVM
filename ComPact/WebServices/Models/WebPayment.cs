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

	}
}