using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ComPact;
using ComPact.Helpers;
using ComPact.Models;
namespace ComPact
{
	public class AssignmentRepository : BaseRepository<RepoAssignment, string>, IAssignmentRepository
	{
		public AssignmentRepository(IDatabase database)
			: base(database)
		{
		}
	}
}
