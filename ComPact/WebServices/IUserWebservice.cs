using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComPact
{
	public interface IUserWebservice
	{
		Task<Tuple<int,User>> CreateUserAsync(User user);
		/**
		 * return Response of Post
		 */
		Task<HttpResponseMessage> LoginUserAsync(string email, string password);
	}
}
