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

		Payment Map(RepoPayment payment);
		RepoPayment InvertMap(Payment payment);
		IEnumerable<Payment> Map(IEnumerable<RepoPayment> payments);
		IEnumerable<RepoPayment> InvertMap(IEnumerable<Payment> payments);

		Location Map(Tuple<RepoLocation, IEnumerable<RepoLocationMember>> location);
		Tuple<RepoLocation, IEnumerable<RepoLocationMember>> InvertMap(Location location);
		IEnumerable<Location> Map(IEnumerable<Tuple<RepoLocation, IEnumerable<RepoLocationMember>>> locations);
		IEnumerable<Tuple<RepoLocation, IEnumerable<RepoLocationMember>>> InvertMap(IEnumerable<Location> locations);


	}
}
