using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComPact.Models;

namespace ComPact
{
	public interface IPaymentDataService
	{
		Task<Payment> Create(Payment payment);
		Task<IEnumerable<Payment>> GetAll(bool isAdmin);
		Task<IEnumerable<Payment>> GetAll(string userId, bool isAdmin);
		Task<Payment> Update(Payment payment);
		Task<bool> Delete(string id);
		Task LogOut();
	}
}
