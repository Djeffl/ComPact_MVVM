using System;
using Newtonsoft.Json;
using SQLite;

namespace ComPact.WebServices.Models
{
	public class WebAssignment
	{
		[JsonProperty("_id")]
		public string Id { get; set; }
		[JsonProperty("itemName")]
		public string ItemName { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("loginToken")]
		public string LoginToken { get; set; }
		[JsonProperty("iconName")]
		public string IconName { get; set; }
		[JsonProperty("done")]
		public bool Done { get; set; }
		[JsonProperty("adminId")]
		public string AdminId { get; set; }
		[JsonProperty("memberId")]
		public string MemberId { get; set; }

		public override string ToString()
		{
			return string.Format("[WebAssignment: Id={0}, ItemName={1}, Description={2}, LoginToken={3}, IconName={4}, Done={5}, AdminId={6}, MemberId={7}]", Id, ItemName, Description, LoginToken, IconName, Done, AdminId, MemberId);
		}
	}
}
