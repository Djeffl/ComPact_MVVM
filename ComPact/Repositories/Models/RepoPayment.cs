using System;
using SQLite;

namespace ComPact
{
	public class RepoPayment
	{
		[PrimaryKey]
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Amount { get; set; }
		public string AdminId { get; set; }
		public string MemberId { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
