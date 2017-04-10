using System;
using SQLite;

namespace ComPact
{
	public class RepoAssignment
	{
		[PrimaryKey]
		public string Id { get; set; }
		public string ItemName { get; set; }
		public string Description { get; set; }
		public string AdminId { get; set; }
		public string MemberId { get; set; }
		public string IconName { get; set; }
		public bool Done { get; set; }

		public override string ToString()
		{	
			return string.Format("[Assignment: Id={0}, ItemName={1}, Description={2}, memberId={3}, IconName={4}, Done={5}]", Id, ItemName, Description, MemberId, IconName, Done);
		}
	}
}
