using System;
using ComPact.Helpers;
using ComPact.Models;

namespace ComPact.Repositories
{
	public class LocationMemberRepository: BaseRepository<RepoLocationMember, Guid>, ILocationMemberRepository
	{
		public LocationMemberRepository(IDatabase database)
			:base(database)
		{
		}
	}
}
