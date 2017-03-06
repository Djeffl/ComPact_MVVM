using System;
using System.Threading.Tasks;

namespace ComPact
{
	public interface IUserWebservice
	{
		Task<string> CreateUserAsync(User user);
	}
}
