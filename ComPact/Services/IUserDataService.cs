using System;
using System.Threading.Tasks;
using ComPact;

namespace ComPact
{
	public interface IUserDataService
	{
		Task<User> Create(User user);
		Task<bool> Login(User user);
		Task<User> Get(string email);
		void LogOut();
		Task<bool> ControlToken();
		void GetDataLocalStorage();
	}
}
