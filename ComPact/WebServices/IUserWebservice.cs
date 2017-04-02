using System;
using System.Net.Http;
using System.Threading.Tasks;
using ComPact.WebServices;

namespace ComPact
{
	public interface IUserWebservice: IBaseWebservice<User>
	{
		Task<User> Forgot(string urlExtend, User obj);
	}
}
