using System;
namespace ComPact.Models
{
	public class Task
	{
		public string ItemName { get; set; }
		public string Describtion { get; set; }

		public Task(string itemName, string describition)
		{
			ItemName = itemName;
			Describtion = describition;
		}
		public Task()
			:this(null, null)
		{
		}
		public override string ToString()
		{
			return string.Format("{0}: {1}", ItemName, Describtion);
		}

	}
}
