using System;
using System.Collections.Generic;
using ComPact.Models;
using ComPact.WebServices.Models;

namespace ComPact
{
	public interface IWebMapper
	{
		Assignment Map(WebAssignment assignment);
		WebAssignment InvertMap(Models.Assignment assignment);
		IEnumerable<Models.Assignment> Map(IEnumerable<WebAssignment> assignments);
		IEnumerable<WebAssignment> InvertMap(IEnumerable<Models.Assignment> assignments);


		User Map(WebUser user);
		WebUser InvertMap(User user);

		Member Map(WebMember member);
		WebMember InvertMap(Member member);
		IEnumerable<Member> Map(IEnumerable<WebMember> members);
		IEnumerable<WebMember> InvertMap(IEnumerable<Member> members);

		Payment Map(WebPayment payment);
		WebPayment InvertMap(Payment payment);
		IEnumerable<Payment> Map(IEnumerable<WebPayment> payments);
		IEnumerable<WebPayment> InvertMap(IEnumerable<Payment> payments);

		Location Map(WebLocation location);
		WebLocation InvertMap(Location location);
		IEnumerable<Location> Map(IEnumerable<WebLocation> locations);
		IEnumerable<WebLocation> InvertMap(IEnumerable<Location> locations);


	}
}
