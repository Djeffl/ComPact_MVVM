using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite.Net;

namespace ComPact
{
	public interface IUserRepository
	{
		Task SaveUser(User user);
		IEnumerable<User> GetUser();
	}
}
