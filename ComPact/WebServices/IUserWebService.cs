using System;
using System.Threading.Tasks;

namespace ComPact.WebServices
{
	public interface IUserWebService: IBaseWebService<WebUser>
	{
		Task<WebUser> Login(string urlExtend, WebUser user);
	}
}
