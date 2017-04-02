using System;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.WebServices;

namespace ComPact.Services
{
	public class TaskDataService : ITaskDataService
	{
		readonly ITaskWebService _taskWebService;
		readonly IDialogService _dialogService;

		const string BasePath = "/api/assignments/";
		const string CreateTaskPath = "/api/assignments/create";

		public TaskDataService(ITaskWebService taskWebService, IDialogService dialogService)
		{
			_taskWebService = taskWebService;
			_dialogService = dialogService;
		}

		public async Task<Models.Task> Create(Models.Task task)
		{
			//try
			//{

				return await _taskWebService.Create(CreateTaskPath, task);
			//}
			//catch (Exception ex)
			//{
			//	throw ex;
			//}
		}
	}
}
