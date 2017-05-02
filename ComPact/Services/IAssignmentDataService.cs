using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact
{
	public interface IAssignmentDataService
	{
		Task<Models.Assignment> Create(Models.Assignment assignment);
		Task<IEnumerable<Models.Assignment>> GetAll(bool isAdmin);
		Task<IEnumerable<Models.Assignment>> GetAll(string userId, bool isAdmin);
		Task<Models.Assignment> Get(string id, bool isAdmin);
		Task<Models.Assignment> Update(Models.Assignment assignment);
		Task<IEnumerable<Models.Assignment>> GetAllUnfinished(bool isAdmin);
		Task<bool> Delete(string id);
		Task LogOut();
	}
}
