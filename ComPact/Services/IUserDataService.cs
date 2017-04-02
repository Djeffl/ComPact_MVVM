using System;
using System.Threading.Tasks;
using ComPact;

namespace ComPact
{
	public interface IUserDataService
	{
		Task<User> Create(User user);
		Task<User> Get(string email);
		void Forgot(User user);
	}
}
