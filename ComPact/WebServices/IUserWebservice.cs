using System;
using System.Threading.Tasks;

namespace ComPact
{
	public interface IUserWebservice
	{
		Task<Tuple<int,User>> CreateUserAsync(User user);
		/**
		 * return ResponseCode, IdUser
		 */
		Task<Tuple<int, Object>> LoginUserAsync(string email, string password);
	}
}
