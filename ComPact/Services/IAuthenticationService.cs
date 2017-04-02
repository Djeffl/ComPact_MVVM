using System;
using System.Threading.Tasks;

namespace ComPact.Services
{
	public interface IAuthenticationService
	{
		Task<bool> AuthenticateToken();
		Task<bool> AuthenticateEmailAndPassword(string email, string password);
		bool LogOut();
	}
}
