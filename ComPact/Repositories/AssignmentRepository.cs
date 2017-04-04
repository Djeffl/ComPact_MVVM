using System;
using ComPact;
using ComPact.Helpers;
using ComPact.Models;

namespace ComPact
{
	public class AssignmentRepository: BaseRepository<Assignment, string>, IAssignmentRepository
	{
		public AssignmentRepository(IDatabase database)
			:base(database)
		{
		}
	}
}
