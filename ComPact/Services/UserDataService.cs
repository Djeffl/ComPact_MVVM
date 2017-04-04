using System;
using System.Linq;
using System.Threading.Tasks;
using ComPact.Data;
using ComPact.Models;
using ComPact.Repositories;
using ComPact.WebServices;

namespace ComPact
{
	public class UserDataService: IUserDataService
	{
		readonly IUserRepository _userRepository;
		readonly IUserWebService _userWebService;

		public UserDataService(IUserRepository userRepository, IUserWebService userWebService)
		{
			_userRepository = userRepository;
			_userWebService = userWebService;
		}

		public async Task Create(User user)
		{
			try
			{
				user = await _userWebService.Create(APICalls.CreateAuthPath, user);
				user = await _userRepository.Insert(user);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<User> Login(User user)
		{
			try
			{
				var responseUser = await _userWebService.Login(APICalls.LoginAuthPath, user);
				await _userRepository.InsertOrReplace(responseUser);
				return responseUser;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task LogOut()
		{
			try
			{
				User user = await GetUser();
				await _userRepository.Delete(user);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<User> GetUser()
		{
			var user = (await _userRepository.All())?.FirstOrDefault();
			return user;
		}
	}
}
