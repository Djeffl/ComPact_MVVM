using System;
using ComPact.Models;
using ComPact.Repositories;

namespace ComPact.Repositories
{
	public interface IUserRepository: IBaseRepository<RepoUser, string>
	{
	}
}
