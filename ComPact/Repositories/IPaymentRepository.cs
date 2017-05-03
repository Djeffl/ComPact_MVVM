using System;
using ComPact.Repositories;

namespace ComPact
{
	public interface IPaymentRepository: IBaseRepository<RepoPayment, string>
	{
		
	}
}
