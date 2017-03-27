using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact;
using ComPact.Helpers;
using SQLite;

namespace ComPact
{
	public class UserRepository: BaseRepository<User, string>, IUserRepository
	{
		//private SQLiteAsyncConnection database;

		public UserRepository(IDatabase database)
			:base(database)
		{
		}
	}
}
