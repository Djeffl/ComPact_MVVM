using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact
{
	public interface IAssignmentDataService
	{
		Task<Assignment> Create(Assignment assignment);
		Task<IEnumerable<Assignment>> GetAssignments();
		Task<IEnumerable<Assignment>> GetAssignments(string adminId);
		Task<IEnumerable<Assignment>> GetAll();
		Task<Assignment> Update(Assignment assignment);
	}
}
