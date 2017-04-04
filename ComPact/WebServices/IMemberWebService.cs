using System;
using System.Net.Http;
using System.Threading.Tasks;
using ComPact.WebServices;

namespace ComPact
{
	public interface IMemberWebService: IBaseWebservice<Member>
	{
		Task<Member> Forgot(string urlExtend, Member obj);
	}
}
