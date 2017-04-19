using System;
namespace ComPact.Models
{
	public class Payment
	{
		
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public string AdminId { get; set; }
		public DateTime CreatedAt { get; set; }
		public Member Member { get; set; }

		public override string ToString()
		{
			return string.Format("[Payment: Id={0}, Name={1}, Description={2}, Amount={3}, AdminId={4}, Member={5}]", Id, Name, Description, Price, AdminId, Member);
		}
	}
}
