using System;
using System.Threading.Tasks;
using ComPact;

namespace ComPact
{
	public class UserDataService: IUserDataService
	{
		//private static UserRepository userRepository = new UserRepository();
		private readonly IUserWebservice _userWebservice;
		private static IUserRepository _userRepository;

		public UserDataService(IUserWebservice userWebservice)
		{
			_userWebservice = userWebservice;
		}

		public async Task<Tuple<int, User>> CreateUserAsync(User user)
		{
			return await _userWebservice.CreateUserAsync(user);
		}
		/**
		 * return ResponseCode, IdUser
		 */
		public async Task<Tuple<int, Object>> LoginUserAsync(string email, string password)
		{
			return await _userWebservice.LoginUserAsync(email, password);
		}
	}
}
