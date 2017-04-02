using System;
using System.Threading.Tasks;
using ComPact.models;
using Refit;

namespace ComPact.WebServices
{
	public interface IRefitService
	{
		[Get("/api/users?email={email}")]
		Task<User> GetUserByEmail(string email);
		[Get("/api/users?id={id}")]
		Task<User> GetUserById(string id);
		[Get("/api/users")]
		Task<User> GetAllUsers();
	}
}
