using System;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact.WebServices
{
	public class UserWebService: BaseWebservice<WebUser>, IUserWebService
	{
		public async Task<WebUser> Login(string urlExtend, WebUser user)
		{
			return await Create(urlExtend, user);
		}



		//async Task<User> Login(string urlExtend, User user)
		//{
		//	return await base.Create(urlExtend, user);
		//}
	}
}
