﻿using System;
using System.Collections.Generic;
using ComPact.Models;

namespace ComPact
{
	public class RepositoryMapper : IRepositoryMapper
	{
		public User Map(RepoUser user)
		{
			var returnUser = new User
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				AdminId = user.AdminId,
				Admin = user.Admin,
				RefreshToken = user.RefreshToken,
				LoginToken = user.LoginToken
			};
			return returnUser;
		}

		public RepoUser InvertMap(User user)
		{
			var returnUser = new RepoUser
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				AdminId = user.AdminId,
				Admin = user.Admin,
				RefreshToken = user.RefreshToken,
				LoginToken = user.LoginToken
			};
			return returnUser;
		}
		public Member Map(RepoMember member)
		{
			var returnMember = new Member
			{
				Id = member.Id,
				FirstName = member.FirstName,
				LastName = member.LastName,
				Email = member.Email,
				AdminId = member.AdminId,
			};
			return returnMember;
		}

		public RepoMember InvertMap(Member member)
		{
			var returnMember = new RepoMember
			{
				Id = member.Id,
				FirstName = member.FirstName,
				LastName = member.LastName,
				Email = member.Email,
				AdminId = member.AdminId,
			};
			return returnMember;
		}

		public IEnumerable<Member> Map(IEnumerable<RepoMember> members)
		{
			List<Member> returnMembers = new List<Member>();
			foreach (RepoMember member in members)
			{
				returnMembers.Add(Map(member));
			}
			return returnMembers;

		}

		public IEnumerable<RepoMember> InvertMap(IEnumerable<Member> members)
		{
			List<RepoMember> returnMembers = new List<RepoMember>();
			foreach (Member member in members)
			{
				returnMembers.Add(InvertMap(member));
			}
			return returnMembers;
		}

		public Assignment Map(RepoAssignment assignment)
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
		public RepoAssignment InvertMap(Assignment assignment)
		{
			var returnAssignment = new RepoAssignment
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

		public IEnumerable<Assignment> Map(IEnumerable<RepoAssignment> assignments)
		{
			List<Assignment> returnAssignments = new List<Assignment>();
			foreach (RepoAssignment assignment in assignments)
			{
				returnAssignments.Add(Map(assignment));
			}
			return returnAssignments;

		}

		public IEnumerable<RepoAssignment> InvertMap(IEnumerable<Assignment> assignments)
		{
			List<RepoAssignment> returnAssignments = new List<RepoAssignment>();
			foreach (Assignment assignment in assignments)
			{
				returnAssignments.Add(InvertMap(assignment));
			}
			return returnAssignments;
		}
	}
}