using System;
using ComPact.Helpers;
using ComPact.Models;

namespace ComPact.Repositories
{
	public class PersonalRepository: BaseRepository<PersonalUser, string>, IPersonalRepository
	{

		public PersonalRepository(IDatabase database)
			:base(database)
		{
		}
	}
}
