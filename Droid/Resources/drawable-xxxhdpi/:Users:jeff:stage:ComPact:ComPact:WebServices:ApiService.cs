using System;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.WebServices.Data;

namespace ComPact.WebServices
{
	public class ApiService : IApiService
	{
		IMapper _mapper;
		IAssignmentWebService _assignmentWebService;

		public ApiService(IMapper mapper, IAssignmentWebService assignmentWebService)
		{
			_mapper = mapper;
			assignmentWebService = _assignmentWebService;
		}

		public async Task<Assignment> AddAssignment(Assignment assignment)
		{
			var webAssignment = _mapper.InvertMap(assignment);
			var webResponse = await _assignmentWebService.Create(APICalls.CreateAssignmentPath, webAssignment);
			return _mapper.Map(webResponse);
		}
	}
}
