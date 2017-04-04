using System;
using System.Collections.Generic;
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
		readonly IDialogService _dialogService;
		readonly IUserDataService _userDataService;
		readonly IMemberDataService _memberDataService;
		readonly IAssignmentDataService _assignmentDataService;


		/**
		 * Constructor
		 */

		public AuthenticationService(IUserDataService userDataService, IDialogService dialogService, IMemberDataService memberDataService, IAssignmentDataService assignmentDataService)
		{
			_userDataService = userDataService;
			_dialogService = dialogService;
			_memberDataService = memberDataService;
			_assignmentDataService = assignmentDataService;
		}

		//public async Task<bool> AuthenticateEmailAndPassword(string email, string password)
		//{
		//	bool validLogin = false;
		//	if (email != "" && password != "" && email != null && password != null)
		//	{
		//		try
		//		{
		//			User user = CreatePersonalUser(email, password);
		//			//PersonalUser webResponse = await _personalUserWebService.Create(APICalls.LoginAuthUserPath, user);
		//			if (webResponse == null)
		//			{
		//				_dialogService.ShowMessage("Email and password do not match.");
		//			}
		//			else
		//			{
		//				//await _personalRepository.Insert(webResponse);
		//				validLogin = true;
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			if (ex is WebException)
		//			{
		//				_dialogService.ShowMessage("Please check your network connection.");
		//			}
		//		}
		//	}
		//	else
		//	{
		//		_dialogService.ShowMessage("Please fill in all fields.");
		//	}
		//	return validLogin;
		//}

		public async Task<bool> Register(string firstName, string lastName, string email, string password, bool admin)
		{
			bool isSuccessful = false;
			var user = new User()
			{
				Id = null,
				FirstName = firstName,
				LastName = lastName,
				Email = email,
				Password = password,
				Admin = admin
			};
			try
			{
				await _userDataService.Create(user);
				isSuccessful = true;
			}
			//TODO Add other exception catches
			catch (Exception)
			{
				_dialogService.ShowMessage("Something went wrong, please try again later.");
			}
			return isSuccessful;
		}
		public async Task<bool> Login(string email, string password)
		{
			bool isSuccessful = false;
			var user = new User()
			{
				Email = email,
				Password = password
			};
			try
			{
				User responseUser = await _userDataService.Login(user);
				IEnumerable<Member> members = await _memberDataService.Save(responseUser.members);
				IEnumerable<Assignment> assignments = await _assignmentDataService.GetAssignments(responseUser.LoginToken);

				isSuccessful = true;
			}
			catch (Exception)
			{
				_dialogService.ShowMessage("Your combination does not match!");
			}
			return isSuccessful;
		}

		//public async Task<bool> AuthenticateToken()
		//{
		//	bool validLogin = false;
		//	if (LoginToken == null)
		//	{
		//	}
		//	else
		//	{
		//		try
		//		{

		//			User user = CreatePersonalUser(LoginToken);
		//			User webResponse = await _personalUserWebService.Create(APICalls.LoginTokenPath, user);

		//			validLogin = true;
		//		}
		//		catch (Exception)
		//		{

		//		}
		//	}
		//	return validLogin;
		//}

		public async Task<bool> LogOut()
		{
			bool isSuccessful = false;
			try
			{
				await _userDataService.LogOut();
				//_memberDataService.LogOut();
				//_assignmentDataService.LogOut();
				isSuccessful = true;
			}
			catch (Exception)
			{
				throw;
			}
			return isSuccessful;
		}

		//public string LoginToken
		//{
		//	get
		//	{
		//		if (HasToken())
		//		{
		//			return Settings.LoginToken;
		//		}
		//		else
		//		{
		//			return null;
		//		}
		//	}
		//}

		//bool HasToken()
		//{
		//	//TODO
		//}
	}
}
