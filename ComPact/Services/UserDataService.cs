using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ComPact;
using Newtonsoft.Json;

namespace ComPact
{
	public class UserDataService: IUserDataService
	{
		private readonly IUserWebservice _userWebservice;
		private static IUserRepository _userRepository;

		public UserDataService(IUserWebservice userWebservice, IUserRepository userRepository)
		{
			_userWebservice = userWebservice;
			_userRepository = userRepository;
		}

		public async Task<Tuple<int, User>> CreateUserAsync(User user)
		{
			return await _userWebservice.CreateUserAsync(user);
		}
		/**
		 * return ResponseCode, IdUser
		 */
		public async Task<bool> LoginUserAsync(string email, string password)
		{
			
			HttpResponseMessage response = await _userWebservice.LoginUserAsync(email, password);
			int statusCode = (int)response.StatusCode;
			//Save data to localstorage & encrypt
			//User userData = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);

			if (!(statusCode == 400 || statusCode == 401))
			{
				try
				{
					var user = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
					await _userRepository.SaveUser(user);
				}
				catch (Exception ex)
				{
					//throw new Exception("Internal error");
				}
				return true;
			}
			return false;
		}
		public bool HasUser()
		{

			if (_userRepository.GetUser() != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
