using ComPact.Helpers;

namespace ComPact
{
	public class LocationRepository: BaseRepository<RepoLocation, string>,ILocationRepository
	{
		public LocationRepository(IDatabase database)
					: base(database)
		{
		}
	}
}
