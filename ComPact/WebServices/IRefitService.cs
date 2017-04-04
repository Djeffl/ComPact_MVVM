using System;
using System.Threading.Tasks;
using ComPact.models;
using Refit;

namespace ComPact.WebServices
{
	public interface IRefitService
	{
		[Get("/api/users?email={email}")]
		Task<Member> GetUserByEmail(string email);
		[Get("/api/users?id={id}")]
		Task<Member> GetUserById(string id);
		[Get("/api/users")]
		Task<Member> GetAllUsers();
	}
}
