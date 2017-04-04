using System;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact.WebServices
{
	public interface IUserWebService: IBaseWebservice<User>
	{
		Task<User> Login(string urlExtend, User user);
	}
}
