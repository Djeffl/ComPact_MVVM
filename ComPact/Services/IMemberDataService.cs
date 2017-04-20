using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact;

namespace ComPact
{
	public interface IMemberDataService
	{
		Task<Member> Create(Member user);
		Task<Member> Get(string memberId);
		Task<IEnumerable<Member>> GetAll(string adminId);
		Task<IEnumerable<Member>> GetAll();

		Task LogOut();
	}
}
