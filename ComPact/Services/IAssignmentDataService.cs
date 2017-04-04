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
	}
}
