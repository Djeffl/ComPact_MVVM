using System;
using ComPact.Helpers;
using ComPact.Models;

namespace ComPact.Repositories
{
	public class LocationRepository : BaseRepository<RepoLocation, string>, ILocationRepository
	{
		public LocationRepository(IDatabase database)
						: base(database)
		{
		}
	}
}
