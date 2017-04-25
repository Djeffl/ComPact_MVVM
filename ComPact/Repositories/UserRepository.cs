using System;
using ComPact.Helpers;
using ComPact.Models;

namespace ComPact.Repositories
{
	public class UserRepository: BaseRepository<RepoUser, string>, IUserRepository
	{

		public UserRepository(IDatabase database)
			:base(database)
		{
		}
	}
}
