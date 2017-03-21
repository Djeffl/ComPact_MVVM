using System;
using System.Threading.Tasks;
using ComPact;

namespace ComPact
{
	public interface IUserDataService
	{
		Task<Tuple<int, User>> CreateUserAsync(User user);
		/**
		 * return ResponseCode, IdUser
		 */
		Task<bool> LoginUserAsync(string email, string password);
		bool HasUser();
	}
}
