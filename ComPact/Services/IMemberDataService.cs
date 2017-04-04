using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact;

namespace ComPact
{
	public interface IMemberDataService
	{
		Task<Member> Create(Member user);
		Task<Member> Get(string email);
		void Forgot(Member user);
		Task<IEnumerable<Member>> Save(IEnumerable<Member> members);
		Task<IEnumerable<Member>> GetAll();
	}
}
