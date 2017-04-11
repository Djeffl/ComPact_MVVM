using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact
{
	public interface IAssignmentDataService
	{
		Task<Assignment> Create(Assignment assignment);
		Task<IEnumerable<Assignment>> GetAll();
		Task<IEnumerable<Assignment>> GetAll(string userId, bool isAdmin);
		Task<Assignment> Update(Assignment assignment);
		Task<IEnumerable<Assignment>> GetAllUnfinished();
		Task<bool> Delete(string id);
		Task LogOut();
	}
}
