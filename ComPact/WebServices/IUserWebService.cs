using System;
using System.Threading.Tasks;

namespace ComPact.WebServices
{
	public interface IUserWebService: IBaseWebservice<WebUser>
	{
		Task<WebUser> Login(string urlExtend, WebUser user);
	}
}
