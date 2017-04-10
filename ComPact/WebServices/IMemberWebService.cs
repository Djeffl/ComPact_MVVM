using System;
using System.Net.Http;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.WebServices;

namespace ComPact
{
	public interface IMemberWebService: IBaseWebservice<WebMember>
	{
		Task<WebMember> Forgot(string urlExtend, WebMember obj);
	}
}
