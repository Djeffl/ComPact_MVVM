using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ComPact;
using ComPact.Data;
using ComPact.Helpers;
using Newtonsoft.Json;

namespace ComPact
{
	public class UserDataService: IUserDataService
	{
		private readonly IUserWebservice _userWebservice;
		private readonly IUserRepository _userRepository;

		public UserDataService(IUserWebservice userWebservice, IUserRepository userRepository)
		{
			_userWebservice = userWebservice;
			_userRepository = userRepository;
		}

		/**
		 * Create User
		 */
		public async Task<User> Create(User user)
		{
			return await _userWebservice.Create(APICalls.CreateUserPath, user);
		}
		public async Task<User> Get(string email)
		{
			string getUserPathEmailPassword = "/api/users?email=\"" + email + "\"";
			try
			{
				User user = await _userWebservice.Read(getUserPathEmailPassword);
				return user;
			}
			catch (Exception)
			{
				throw new Exception("Can't connect to server. Please check your network.");
			}
		}

		public void Forgot(User user)
		{
			_userWebservice.Forgot("/api/users/forgot", user);
		}
	}
}
	
		