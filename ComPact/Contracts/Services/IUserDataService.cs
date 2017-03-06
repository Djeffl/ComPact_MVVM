using System;
using System.Threading.Tasks;

namespace ComPact
{
	public interface IUserDataService
	{
		Task<string> CreateUserAsync(User user);
	}
}
