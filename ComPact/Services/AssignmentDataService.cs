using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.Data;
using ComPact.Models;
using ComPact.WebServices;

namespace ComPact.Services
{
	public class AssignmentDataService : IAssignmentDataService
	{
		readonly IAssignmentWebService _assignmentWebService;
		readonly IAssignmentRepository _assignmentRepository;
		readonly IDialogService _dialogService;

		const string BasePath = "/api/assignments/";
		const string CreateTaskPath = "/api/assignments/create";

		public AssignmentDataService(IAssignmentWebService assignmentWebService, IAssignmentRepository assignmentRepository, IDialogService dialogService)
		{
			_assignmentWebService = assignmentWebService;
			_assignmentRepository = assignmentRepository;
			_dialogService = dialogService;
		}

		public async Task<Assignment> Create(Assignment assignment)
		{
			try
			{

				Assignment response = await _assignmentWebService.Create(CreateTaskPath, assignment);
				return await _assignmentRepository.Insert(response);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public async Task<IEnumerable<Assignment>> GetAssignments()
		{
			return await _assignmentRepository.All();
		}
		public async Task<IEnumerable<Assignment>> GetAssignments(string loginToken)
		{
			string apiUrlCall = APICalls.BaseAssignemntPath + "?loginToken=" + loginToken;
			var assignments = await _assignmentWebService.ReadAll(apiUrlCall);
			await _assignmentRepository.Insert(assignments);
			return assignments;
		}
	}
}
