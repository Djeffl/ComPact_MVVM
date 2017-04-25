using System;
using ComPact.Helpers;

namespace ComPact
{
	public class PaymentRepository: BaseRepository<RepoPayment,string>, IPaymentRepository
	{
		public PaymentRepository(IDatabase database)
			:base(database)
		{
		}
	}
}
