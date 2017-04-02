using System;
using System.Threading.Tasks;
using ComPact.Data;
using ComPact.Exceptions;
using ComPact.Helpers;
using ComPact.Models;
using ComPact.Repositories;
using ComPact.WebServices;

namespace ComPact.Services
{
	public class AuthenticationService: IAuthenticationService
	{
		readonly IPersonalUserWebService _personalUserWebService;
		readonly IDialogService _dialogService;
		readonly IPersonalRepository _personalRepository;


		/**
		 * Constructor
		 */

		public AuthenticationService(IPersonalUserWebService personalUserWebService, IPersonalRepository personalRepository, IDialogService dialogService)
		{
			_personalUserWebService = personalUserWebService;
			_personalRepository = personalRepository;
			_dialogService = dialogService;
		}

		public async Task<bool> AuthenticateEmailAndPassword(string email, string password)
		{
			bool validLogin = false;
			if (email != "" && password != "" && email != null && password != null)
			{
				try
				{
					PersonalUser user = CreatePersonalUser(email, password);
					//PersonalUser webResponse = await _personalUserWebService.Create(APICalls.LoginAuthUserPath, user);
					if (webResponse == null)
					{
						_dialogService.ShowMessage("Email and password do not match.");
					}
					else
					{
						//await _personalRepository.Insert(webResponse);
						validLogin = true;
					}
				}
				catch (Exception ex)
				{
					if (ex is WebException)
					{
						_dialogService.ShowMessage("Please check your network connection.");
					}
				}
			}
			else
			{
				_dialogService.ShowMessage("Please fill in all fields.");
			}
			return validLogin;
		}

		public async Task<bool> Create(PersonalUser user)
		{
			bool validTry = false;
			try
			{
				user = await _personalUserWebService.Create(APICalls.CreatePersonalUserPath, user);
				await _personalRepository.Insert(user);
				validTry = true;
			}
			catch (Exception)
			{
				_dialogService.ShowMessage("Something went wrong, please try again later.");
			}
			return validTry;
		}

		public async Task<bool> AuthenticateToken()
		{
			bool validLogin = false;
			if (LoginToken == null)
			{
			}
			else
			{
				try
				{

					PersonalUser user = CreatePersonalUser(LoginToken);
					PersonalUser webResponse = await _personalUserWebService.Create(APICalls.LoginTokenPath, user);

					validLogin = true;
				}
				catch (Exception)
				{
					
				}
			}
			return validLogin;
		}

		public bool LogOut()
		{
			bool validLogOut = false;
			try
			{
				Settings.ClearAll();
				validLogOut = true;
			}
			catch (Exception)
			{
				throw;
			}
			return validLogOut;
		}

		public string LoginToken
		{
			get
			{
				if (HasToken())
				{
					return Settings.LoginToken;
				}
				else
				{
					return null;
				}
			}
		}

		bool HasToken()
		{
			try
			{
				bool hasToken;
				if (Settings.LoginToken != Settings.GetLoginTokenDefault())
				{
					hasToken = true;
				}
				else
				{
					hasToken = false;
				}
				return hasToken;

			}
			catch (Exception)
			{
				throw;
			}
		}
		PersonalUser CreatePersonalUser(string loginToken)
		{
			return new PersonalUser
			{
				LoginToken = loginToken
			};
		}
		PersonalUser CreatePersonalUser(string email, string password)
		{
			return new PersonalUser
			{
				Email = email,
				Password = password
			};
		}
	}
}
