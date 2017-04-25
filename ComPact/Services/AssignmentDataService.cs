using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.WebServices;
using ComPact.Models;
using System.Linq;

namespace ComPact.Services
{
	public class AssignmentDataService : IAssignmentDataService
	{
		readonly IApiService _apiService;
		readonly IAssignmentRepository _assignmentRepository;
		readonly IMemberRepository _memberRespository;
		readonly IDialogService _dialogService;
		readonly IRepositoryMapper _mapper;

		public AssignmentDataService(IApiService apiService, IAssignmentRepository assignmentRepository, IMemberRepository memberRepository, IDialogService dialogService, IRepositoryMapper mapper)
		{
			_apiService = apiService;
			_assignmentRepository = assignmentRepository;
			_dialogService = dialogService;
			_memberRespository = memberRepository;
			_mapper = mapper;
		}

		public async Task<Assignment> Create(Assignment assignment)
		{
			Assignment response = await _apiService.AddAssignment(assignment);
			RepoAssignment data = _mapper.InvertMap(response);
			return _mapper.Map(await _assignmentRepository.Insert(data));
			
		}
		public async Task<IEnumerable<Assignment>> GetAll(bool isAdmin)
		{
			IEnumerable<Assignment> assignments  = _mapper.Map(await _assignmentRepository?.All());
			if (isAdmin)
			{
				IEnumerable<Member> members = _mapper.Map(await _memberRespository?.All());
				foreach (var assignment in assignments)
				{
					foreach (var member in members)
					{
						if (assignment.Member.Id == member.Id)
						{
							assignment.Member = member;
						}
					}
				}
			}
			return assignments;
		}

		public async Task<IEnumerable<Assignment>> GetAll(string userId, bool isAdmin)
		{
			IEnumerable<Assignment> response = await _apiService.GetAssignments(userId, isAdmin);
			IEnumerable<RepoAssignment> data = _mapper.InvertMap(response);
			await _assignmentRepository.Insert(data);
			return await GetAll(isAdmin);
		}
		public async Task<IEnumerable<Assignment>> GetAllUnfinished(bool isAdmin)
		{
			IEnumerable<Assignment> assignments = _mapper.Map(await _assignmentRepository.GetAllUnfinished());
			if (isAdmin)
			{
				IEnumerable<Member> members = _mapper.Map(await _memberRespository?.All());
				foreach (var assignment in assignments)
				{
					foreach (var member in members)
					{
						if (assignment.Member.Id == member.Id)
						{
							assignment.Member = member;
						}
					}
				}
			}
			return assignments;
		}

		public async Task<Assignment> Update(Assignment assignment)
		{
			Assignment response = await _apiService.UpdateAssignment(assignment);
			RepoAssignment data = _mapper.InvertMap(response);
			await _assignmentRepository.Update(data);
			return response;
		}
		public async Task<Assignment> Get(string id, bool isAdmin)
		{
			Assignment assignment = _mapper.Map(await _assignmentRepository.Get(id));
			if (isAdmin)
			{
				IEnumerable<Member> members = _mapper.Map(await _memberRespository?.All());

				foreach (var member in members)
				{
					if (assignment.Member.Id == member.Id)
					{
						assignment.Member = member;
					}
				}

			}
			return assignment;
		}

		public async Task<bool> Delete(string id)
		{
			bool isSuccessful = await _apiService.DeleteAssignment(id);
			if (isSuccessful)
			{
				IEnumerable<RepoAssignment> response = await _assignmentRepository.Where((x => x.Id == id));
				await _assignmentRepository.Delete(response.FirstOrDefault());
				isSuccessful = true;
			}
			return isSuccessful;

		}

		public async Task LogOut()
		{
			await _assignmentRepository.Delete(await _assignmentRepository?.All());
		}
	}
}
