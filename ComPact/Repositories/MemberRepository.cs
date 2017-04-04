using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact;
using ComPact.Helpers;
using SQLite;

namespace ComPact
{
	public class MemberRepository: BaseRepository<Member, string>, IMemberRepository
	{
		//private SQLiteAsyncConnection database;

		public MemberRepository(IDatabase database)
			:base(database)
		{
		}

	}
}
