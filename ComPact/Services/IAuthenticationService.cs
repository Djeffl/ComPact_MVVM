using System;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact.Services
{
	public interface IAuthenticationService
	{
		//Task<bool> AuthenticateToken();
		//Task<bool> AuthenticateEmailAndPassword(string email, string password);
		Task<bool> Register(string firstName, string lastName, string email, string password, bool admin);
		Task<bool> Login(string email, string password);
		Task<bool> LogOut();
	}
}
