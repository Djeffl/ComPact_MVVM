using System;
using System.Threading.Tasks;
namespace ComPact.Services
{
	public interface ITaskDataService
	{
		Task<Models.Task> Create(Models.Task task);
	}
}
