using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.WebServices;

namespace ComPact
{
	public class PaymentDataService : IPaymentDataService
	{
		readonly IApiService _apiService;
		readonly IPaymentRepository _paymentRepository;
		readonly IRepositoryMapper _mapper;

		public PaymentDataService(IApiService apiService, IPaymentRepository paymentRepository, IRepositoryMapper mapper)
		{
			_apiService = apiService;
			_paymentRepository = paymentRepository;
			_mapper = mapper;
		}

		public async Task<Payment> Create(Payment payment)
		{
			Payment response = await _apiService.AddPayment(payment);
			RepoPayment data = _mapper.InvertMap(response);
			await _paymentRepository.Insert(data);
			return response;
		}

		public Task<bool> Delete(string id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Payment>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Payment>> GetAll(string userId, bool isAdmin)
		{
			throw new NotImplementedException();
		}

		public Task LogOut()
		{
			throw new NotImplementedException();
		}

		public Task<Payment> Update(Payment payment)
		{
			throw new NotImplementedException();
		}
	}
}
