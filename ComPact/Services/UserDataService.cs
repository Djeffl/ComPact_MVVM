using System;
using System.Diagnostics;
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
		private readonly IUserRepository _userRepository;

		const string BasePath = "/api/users/";
		const string CreateUserPath = "/api/users/create";
		const string LoginUserPath = "/api/users/authenticate";
		const string LoginTokenPath = "/api/users/login";

		public UserDataService(IUserWebservice userWebservice, IUserRepository userRepository)
		{
			_userWebservice = userWebservice;
			_userRepository = userRepository;
		}

		public async Task<bool> Login(User user)
		{
			try
			{
				user = await _userWebservice.Create(LoginUserPath, user);
				//Save data to localstorage & encrypt
				Debug.WriteLine("THE USER WHEN LOGGED IN " + user);
				await _userRepository.Insert(user);
				return true;
			}
			catch (Exception err)
			{
				throw err;
			}
		}
		public async Task<bool> ValidToken()
		{
			try
			{
				//Token wrapped in Userobject
				var userToken = _userRepository.All();
				var user= await _userWebservice.Create(LoginUserPath, userToken);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		/**
		 * Create User
		 */
		public async Task<User> Create(User user)
		{
			return await _userWebservice.Create(CreateUserPath, user);
		}
		public async Task<User> Get(string email)
		{
			string getUserPathEmailPassword = "/api/users/user?name=\"" + email + "\"";
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
		public async Task<bool> ControlToken()
		{
			try
			{
				var users = _userRepository.All();
				var enumerator = users.GetEnumerator();

				while (enumerator.MoveNext() || enumerator.Current != null)
				{
					Debug.WriteLine("okeokfoefo " + enumerator.Current);
					User user = enumerator.Current;
					User response = await _userWebservice.Create(LoginTokenPath, user);
					return true;
				}
				return false;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public void LogOut()
		{
			try
			{
				var enumer = _userRepository.All().GetEnumerator();
				_userRepository.All().GetEnumerator().Dispose();
				while (enumer.MoveNext() || enumer.Current != null)
				{
					_userRepository.Delete(enumer.Current);
					Debug.WriteLine("okeokfoefo " + enumer.Current);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}
		public void GetDataLocalStorage()
		{
			var users = _userRepository.All().GetEnumerator();
			Debug.WriteLine(users);
			int i = 0;
			while(users.MoveNext())
			{
				Debug.WriteLine(i +  ": " + users.Current);
				i++;
			}
		}
	}
}
	
		