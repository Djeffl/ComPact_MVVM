using System;
using SQLite;

namespace ComPact.Models
{
	public class RepoLocationMember
	{
		[PrimaryKey]
		public Guid Id { get; set; }
		public string LocationId { get; set; }
		public string MemberId { get; set; }
	}
}
