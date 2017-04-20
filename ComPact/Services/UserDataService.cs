using System;
using System.Linq;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.Repositories;
using ComPact.WebServices;

namespace ComPact
{
	public class UserDataService: IUserDataService
	{
		readonly IUserRepository _userRepository;
		readonly IApiService _apiService;
		readonly IRepositoryMapper _repositoryMapper;

		public UserDataService(IUserRepository userRepository, IApiService apiService, IRepositoryMapper repositoryMapper)
		{
			_userRepository = userRepository;
			_apiService = apiService;
			_repositoryMapper = repositoryMapper;
		}
		//TODO Make it so the responseUser his logintoken gets generated from his refreshToken
		public async Task<User> Create(User user)
		{
			var responseUser = await _apiService.AddUser(user);
			return await Login(user);
		}

		public async Task<User> Login(User user)
		{
			User responseUser = await _apiService.LoginUser(user);
			var repoUser = _repositoryMapper.InvertMap(responseUser);
			await _userRepository.InsertOrReplace(repoUser);
			return responseUser;
		}
		public async Task LogOut()
		{
			var repoUser = _repositoryMapper.InvertMap(await GetUser());
			await _userRepository.Delete(repoUser);
		}
		public async Task<User> GetUser()
		{
			return _repositoryMapper.Map((await _userRepository.All())?.FirstOrDefault());
		}
	}
}
