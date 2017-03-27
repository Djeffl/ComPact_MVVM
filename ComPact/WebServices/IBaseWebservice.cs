using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComPact.WebServices
{
	public interface IBaseWebservice<T>
	{
		Task<T> Create(string urlExtend, T obj);
		Task<T> Create(string urlExtend, IEnumerable<T> obj);
		Task<T> Read(string urlExtend);
	}
}
