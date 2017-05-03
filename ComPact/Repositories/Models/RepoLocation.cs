using SQLite;

namespace ComPact.Models
{
	public class RepoLocation
	{
		[PrimaryKey]
		public string Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public float Radius { get; set; }
		public string AdminId { get; set; }
	}
}
