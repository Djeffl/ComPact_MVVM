using System;
using SQLite;

namespace ComPact.Models
{
	public class RepoMember
	{
		[PrimaryKey]
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string AdminId { get; set; }
		public byte[] ProfilePicture { get; set; }
	}
}
