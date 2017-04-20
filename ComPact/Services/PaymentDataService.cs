using System;
using System.Collections.Generic;
using System.Linq;
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
		readonly IMemberRepository _memberRespository;

		public PaymentDataService(IApiService apiService, IPaymentRepository paymentRepository, IMemberRepository memberRepository, IRepositoryMapper mapper)
		{
			_apiService = apiService;
			_paymentRepository = paymentRepository;
			_memberRespository = memberRepository;
			_mapper = mapper;
		}

		public async Task<Payment> Create(Payment payment)
		{
			Payment response = await _apiService.AddPayment(payment);
			RepoPayment data = _mapper.InvertMap(response);
			await _paymentRepository.Insert(data);
			return response;
		}

		public async Task<bool> Delete(string id)
		{
			bool isSuccessful = await _apiService.DeletePayment(id);
			if (isSuccessful)
			{
				IEnumerable<RepoPayment> response = await _paymentRepository.Where((x => x.Id == id));
				await _paymentRepository.Delete(response.FirstOrDefault());
				isSuccessful = true;
			}
			return isSuccessful;
		}

		public async Task<IEnumerable<Payment>> GetAll(bool isAdmin)
		{
			IEnumerable<Payment> payments = _mapper.Map(await _paymentRepository?.All());
			if (isAdmin)
			{

				IEnumerable<Member> members = _mapper.Map(await _memberRespository?.All());
				foreach (var payment in payments)
				{
					foreach (var member in members)
					{
						if (payment.Member.Id == member.Id)
						{
							payment.Member = member;
						}
					}
				}
			}
			return payments;
		}

		public async Task<IEnumerable<Payment>> GetAll(string userId, bool isAdmin)
		{
			IEnumerable<Payment> response = await _apiService.GetPayments(userId, isAdmin);
			IEnumerable<RepoPayment> data = _mapper.InvertMap(response);
			await _paymentRepository.Insert(data);
			return await GetAll(isAdmin);
		}

		public async Task LogOut()
		{
			await _paymentRepository.Delete(await _paymentRepository?.All());
		}

		public async Task<Payment> Update(Payment payment)
		{
			Payment response = await _apiService.UpdatePayment(payment);
			RepoPayment data = _mapper.InvertMap(response);
			await _paymentRepository.Update(data);
			return response;
		}

		public async Task<Payment> Get(string paymentId)
		{
			return _mapper.Map(await _paymentRepository.Get(paymentId));
		}
	}
}
