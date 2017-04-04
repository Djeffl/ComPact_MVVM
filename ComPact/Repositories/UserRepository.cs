using System;
using ComPact.Helpers;
using ComPact.Models;

namespace ComPact.Repositories
{
	public class UserRepository: BaseRepository<User, string>, IUserRepository
	{

		public UserRepository(IDatabase database)
			:base(database)
		{
		}
	}
}
