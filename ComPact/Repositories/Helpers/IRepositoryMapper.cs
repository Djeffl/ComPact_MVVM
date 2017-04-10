using System;
using System.Collections.Generic;
using ComPact.Models;
using ComPact.WebServices.Models;

namespace ComPact
{
	public interface IRepositoryMapper
	{
		User Map(RepoUser user);
		RepoUser InvertMap(User user);

		Member Map(RepoMember member);
		RepoMember InvertMap(Member member);

		IEnumerable<Member> Map(IEnumerable<RepoMember> members);
		IEnumerable<RepoMember> InvertMap(IEnumerable<Member> members);

		RepoAssignment InvertMap(Assignment assignment);
		Assignment Map(RepoAssignment assignment);
		IEnumerable<Assignment> Map(IEnumerable<RepoAssignment> assignments);
		IEnumerable<RepoAssignment> InvertMap(IEnumerable<Assignment> assignments);
	}
}
