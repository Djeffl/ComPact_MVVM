using System;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact
{
	public interface IUserDataService
	{
		Task Create(User user);
		Task<User> Login(User user);
		Task LogOut();
	}
}
