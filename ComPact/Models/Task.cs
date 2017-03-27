using System;
namespace ComPact.Models
{
	public class Task
	{
		public string Id { get; set; }
		public string ItemName { get; set; }
		public string Description { get; set; }
		public User Member { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", ItemName, Description);
		}

	}
}
