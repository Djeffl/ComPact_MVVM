using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.WebServices.Data;
using ComPact.WebServices.Models;

namespace ComPact.WebServices
{
	public class ApiService : IApiService
	{
		IWebMapper _mapper;
		IAssignmentWebService _assignmentWebService;
		IUserWebService _userWebService;
		IMemberWebService _memberWebService;
		IPaymentWebService _paymentWebService;
		ILocationWebService _locationWebService;

		public ApiService(IWebMapper mapper, IAssignmentWebService assignmentWebService,
						  IUserWebService userWebService, IMemberWebService memberWebService,
						  IPaymentWebService paymentWebService, ILocationWebService locationWebService)
		{
			_mapper = mapper;
			_assignmentWebService = assignmentWebService;
			_userWebService = userWebService;
			_memberWebService = memberWebService;
			_paymentWebService = paymentWebService;
			_locationWebService = locationWebService;       
		}


		public async Task<User> AddUser(User user)
		{
			WebUser data = _mapper.InvertMap(user);
			WebUser response = await _userWebService.Create(ApiCalls.CreateUserPath, data);
			return _mapper.Map(response);
		}

		public async Task<User> LoginUser(User user)
		{
			WebUser data = _mapper.InvertMap(user);
			WebUser response = await _userWebService.Create(ApiCalls.LoginAuthPath, data);
			return _mapper.Map(response);
		}

		public async Task<User> GetUser(string email)
		{
			string url = ApiCalls.BaseAuthPath + String.Format("?email={0}", email);
			try
			{
				WebUser response = await _userWebService.Read(url);
				return _mapper.Map(response);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<Assignment> AddAssignment(Assignment assignment)
		{
			WebAssignment data = _mapper.InvertMap(assignment);
			WebAssignment response = await _assignmentWebService.Create(ApiCalls.CreateAssignmentPath, data);
			return _mapper.Map(response);
		}

		public async Task<Member> AddMember(Member member)
		{
			WebMember data = _mapper.InvertMap(member);
			WebMember response = await _memberWebService.Create(ApiCalls.CreateMemberPath, data);
			return _mapper.Map(response);
		}

		public async Task<IEnumerable<Member>> GetMembers(string adminId)
		{
			string url = ApiCalls.BaseUserPath + String.Format("?adminId={0}", adminId);
			IEnumerable<WebMember> response = await _memberWebService.ReadAll(url);
			return _mapper.Map(response);

		}

		public Task<Member> GetMember(string adminId)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Assignment>> GetAssignments(string userId, bool isAdmin)
		{
			string url;
			if (isAdmin)
			{
				url = ApiCalls.BaseAssignemntPath + String.Format("?adminId={0}", userId);
			}
			else
			{
				url = ApiCalls.BaseAssignemntPath + String.Format("?memberId={0}", userId);
			}
			IEnumerable<WebAssignment> response = await _assignmentWebService.ReadAll(url);
			return _mapper.Map(response);
		}

		public async Task<Assignment> UpdateAssignment(Assignment assignment)
		{
			string url = ApiCalls.BaseAssignemntPath + String.Format("/{0}", assignment.Id);
			WebAssignment data = _mapper.InvertMap(assignment);
			WebAssignment response = await _assignmentWebService.Update(url, data);
			return _mapper.Map(response);
		}

		public async Task<bool> DeleteAssignment(string assignmentId)
		{
			bool isSuccessful = false;
			string url = ApiCalls.BaseAssignemntPath + String.Format("/{0}", assignmentId);
			try
			{
				isSuccessful = (await _assignmentWebService.Delete(url)).success;
			}
			catch (Exception)
			{
			}
			return isSuccessful;
		}

		public async Task<Payment> AddPayment(Payment payment)
		{
			WebPayment data = _mapper.InvertMap(payment);
			WebPayment response = await _paymentWebService.Create(ApiCalls.BasePaymentPath, data);
			return _mapper.Map(response);
		}

		public async Task<IEnumerable<Payment>> GetPayments(string userId, bool isAdmin)
		{
			string url;
			if (isAdmin)
			{
				url = ApiCalls.BasePaymentPath + String.Format("?adminId={0}", userId);
			}
			else
			{
				url = ApiCalls.BasePaymentPath + String.Format("?memberId={0}", userId);
			}
			IEnumerable<WebPayment> response = await _paymentWebService.ReadAll(url);
			return _mapper.Map(response);
		}

		public async Task<bool> DeletePayment(string paymentId)
		{
			bool isSuccessful = false;
			string url = ApiCalls.BasePaymentPath + String.Format("/{0}", paymentId);
			try
			{
				isSuccessful = (await _paymentWebService.Delete(url)).success;
			}
			catch (Exception)
			{

			}
			return isSuccessful;
		}

		public async Task<Payment> UpdatePayment(Payment payment)
		{
			string url = ApiCalls.BasePaymentPath + String.Format("/{0}", payment.Id);
			WebPayment data = _mapper.InvertMap(payment);
			WebPayment response = await _paymentWebService.Update(url, data);
			return _mapper.Map(response);
		}

		public async Task<Location> AddLocation(Location location)
		{
			WebLocation data = _mapper.InvertMap(location);
			WebLocation response = await _locationWebService.Create(ApiCalls.BaseLocationPath, data);
			return _mapper.Map(response);
		}

		public async Task<Location> UpdateLocation(Location location)
		{
			string url = ApiCalls.BaseLocationPath + String.Format("/{0}", location.Id);
			WebLocation data = _mapper.InvertMap(location);
			WebLocation response = await _locationWebService.Update(url, data);
			return _mapper.Map(response);
		}

		public async Task<bool> DeleteLocation(string locationId)
		{
			bool isSuccessful = false;
			string url = ApiCalls.BaseLocationPath + String.Format("/{0}", locationId);
			try
			{
				isSuccessful = (await _locationWebService.Delete(url)).success;
			}
			catch (Exception)
			{
			}
			return isSuccessful;
		}

		public async Task<IEnumerable<Location>> GetLocations(string userId, bool isAdmin)
		{
			string url;
			if (isAdmin)
			{
				url = ApiCalls.BaseLocationPath + String.Format("?adminId={0}", userId);
			}
			else
			{
				url = ApiCalls.BaseLocationPath + String.Format("?memberId={0}", userId);
			}
			IEnumerable<WebLocation> response = await _locationWebService.ReadAll(url);
			return _mapper.Map(response);
		}
	}
}
