using System;
using ComPact.Models;
using ComPact.Repositories;

namespace ComPact.Repositories
{
	public interface IPersonalRepository: IBaseRepository<PersonalUser, string>
	{
	}
}
