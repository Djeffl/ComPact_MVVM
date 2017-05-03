using System;
using System.Collections.Generic;

namespace ComPact.Models
{
	public class Location
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public float Radius { get; set; }
		public string AdminId { get; set; }
		public List<Member> Members { get; set; }

		public override string ToString()
		{
			return string.Format("[Location: Name={0}, City={1}, Street={2}, radius={3}, Members={4}]", Name, City, Street, Radius, Members);
		}
	}
}
