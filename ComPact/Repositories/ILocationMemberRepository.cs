using System;
using ComPact.Models;
using ComPact.Repositories;

namespace ComPact.Repositories
{
	public interface ILocationMemberRepository: IBaseRepository<RepoLocationMember, Guid>
	{
	}
}
