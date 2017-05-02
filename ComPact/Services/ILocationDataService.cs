using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact
{
	public interface ILocationDataService
	{
		Task<Location> Create(Location location);
		Task<IEnumerable<Location>> GetAll(bool isAdmin);
		Task<IEnumerable<Location>> GetAll(string userId, bool isAdmin);
		Task<Location> Get(string id, bool isAdmin);
		Task<Location> Update(Location location);
		Task<bool> Delete(string id);
		Task LogOut();
	}
}
