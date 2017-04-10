using System;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact
{
	public interface IUserDataService
	{
		Task<User> Create(User user);
		Task<User> GetUser();
		Task<User> Login(User user);
		Task LogOut();

	}
}
