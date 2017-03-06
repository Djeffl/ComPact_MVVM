using System;
using System.Threading.Tasks;

namespace ComPact
{
	public class UserDataService: IUserDataService
	{
		//private static UserRepository userRepository = new UserRepository();
		private readonly IUserWebservice _userWebservice;


		public UserDataService(IUserWebservice userWebservice)
		{
			_userWebservice = userWebservice;
		}

		public async Task<string> CreateUserAsync(User user)
		{
			return await _userWebservice.CreateUserAsync(user);
		}
	}
}
