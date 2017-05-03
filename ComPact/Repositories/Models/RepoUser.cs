using System;
namespace ComPact.Models
{
	public class RepoUser: RepoMember
	{
		public bool Admin { get; set; }
		public string LoginToken { get; set; }
		public string RefreshToken { get; set; }
	}
}
