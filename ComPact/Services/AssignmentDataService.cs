using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.WebServices;
using ComPact.Models;

namespace ComPact.Services
{
	public class AssignmentDataService : IAssignmentDataService
	{
		readonly IApiService _apiService;
		readonly IAssignmentRepository _assignmentRepository;
		readonly IDialogService _dialogService;
		readonly IRepositoryMapper _mapper;

		public AssignmentDataService(IApiService apiService, IAssignmentRepository assignmentRepository, IDialogService dialogService, IRepositoryMapper mapper)
		{
			_apiService = apiService;
			_assignmentRepository = assignmentRepository;
			_dialogService = dialogService;
			_mapper = mapper;
		}

		public async Task<Assignment> Create(Assignment assignment)
		{
			Assignment response = await _apiService.AddAssignment(assignment);
			RepoAssignment data = _mapper.InvertMap(response);
			return _mapper.Map(await _assignmentRepository.Insert(data));
			
		}
		public async Task<IEnumerable<Assignment>> GetAll()
		{
			return _mapper.Map(await _assignmentRepository.All());
		}

		public async Task<IEnumerable<Assignment>> GetAll(string userId, bool isAdmin)
		{
			IEnumerable<Assignment> response = await _apiService.GetAssignments(userId, isAdmin);
			IEnumerable<RepoAssignment> data = _mapper.InvertMap(response);
			await _assignmentRepository.Insert(data);
			return await GetAll();
		}
		public async Task<IEnumerable<Assignment>> GetAllUnfinished()
		{
			return _mapper.Map(await _assignmentRepository.GetAllUnfinished());
		}

		public async Task<Assignment> Update(Assignment assignment)
		{
			Assignment response = await _apiService.UpdateAssignment(assignment);
			RepoAssignment data = _mapper.InvertMap(response);
			await _assignmentRepository.Update(data);
			return response;
		}
		public async Task<Assignment> Get(string id)
		{
			return _mapper.Map(await _assignmentRepository.Get(id));
		}
	}
}
