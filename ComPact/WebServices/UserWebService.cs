using System;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact.WebServices
{
	public class UserWebService: BaseWebservice<User>, IUserWebService
	{
		public async Task<User> Login(string urlExtend, User user)
		{
			return await Create(urlExtend, user);
		}



		//async Task<User> Login(string urlExtend, User user)
		//{
		//	return await base.Create(urlExtend, user);
		//}
	}
}
