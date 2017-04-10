using System;
using System.Collections.Generic;
using ComPact.Models;
using ComPact.WebServices.Models;

namespace ComPact
{
	public class WebMapper : IWebMapper
	{
		public Assignment Map(WebAssignment assignment)
		{
			var returnAssignment = new Assignment
			{
				Id = assignment.Id,
				ItemName = assignment.ItemName,
				Description = assignment.Description,
				MemberId = assignment.MemberId,
				IconName = assignment.IconName,
				Done = assignment.Done
			};
			return returnAssignment;
		}
		public WebAssignment InvertMap(Assignment assignment)
		{
			var returnAssignment = new WebAssignment
			{
				Id = assignment.Id,
				ItemName = assignment.ItemName,
				Description = assignment.Description,
				MemberId = assignment.MemberId,
				AdminId = assignment.AdminId,
				IconName = assignment.IconName,
				Done = assignment.Done
			};
			return returnAssignment;
		}
		public IEnumerable<Assignment> Map(IEnumerable<WebAssignment> assignments)
		{
			List<Assignment> returnAssignments = new List<Assignment>();
			foreach (WebAssignment assignment in assignments)
			{
				returnAssignments.Add(Map(assignment));
			}
			return returnAssignments;

		}

		public IEnumerable<WebAssignment> InvertMap(IEnumerable<Assignment> assignments)
		{
			List<WebAssignment> returnAssignments = new List<WebAssignment>();
			foreach (Assignment assignment in assignments)
			{
				returnAssignments.Add(InvertMap(assignment));
			}
			return returnAssignments;
		}

		public User Map(WebUser user)
		{
			var returnUser = new User
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Password = user.Password,
				Admin = user.Admin,
				AdminId = user.AdminId,
				QrCode = user.QrCode,
				AssignmentsIds = user.AssignmentsIds,
				RefreshToken = user.RefreshToken
			};
			return returnUser;
		}

		public WebUser InvertMap(User user)
		{
			var returnUser = new WebUser
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Password = user.Password,
				Admin = user.Admin,
				AdminId = user.AdminId,
				QrCode = user.QrCode,
				AssignmentsIds = user.AssignmentsIds,
				LoginToken = user.LoginToken,
				RefreshToken = user.RefreshToken
			};
			return returnUser;
		}

		public Member Map(WebMember member)
		{
			var returnMember = new Member
			{
				Id = member.Id,
				FirstName = member.FirstName,
				LastName = member.LastName,
				Email = member.Email,
				Password = member.Password,
				AdminId = member.AdminId,
				QrCode = member.QrCode,
				AssignmentsIds = member.AssignmentsIds
			};
			return returnMember;
		}

		public WebMember InvertMap(Member member)
		{
			var returnMember = new WebMember
			{
				Id = member.Id,
				FirstName = member.FirstName,
				LastName = member.LastName,
				Email = member.Email,
				Password = member.Password,
				QrCode = member.QrCode,
				AssignmentsIds = member.AssignmentsIds,
				AdminId = member.AdminId
			};
			return returnMember;
		}
		public IEnumerable<Member> Map(IEnumerable<WebMember> members)
		{
			var returnMembers = new List<Member>();
			foreach (WebMember member in members)
			{
				returnMembers.Add(Map(member));
			}
			return returnMembers;

		}

		public IEnumerable<WebMember> Map(IEnumerable<Member> members)
		{
			var returnMembers = new List<WebMember>();
			foreach (Member member in members)
			{
				returnMembers.Add(InvertMap(member));
			}
			return returnMembers;
		}
	}
}
