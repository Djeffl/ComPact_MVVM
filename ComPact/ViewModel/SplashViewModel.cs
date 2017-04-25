using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.ViewModel
{
	public class SplashViewModel : BaseViewModel
	{
		/**
		 * Declare Services
		 */
		private readonly INavigationService _navigationService;
		private readonly IAuthenticationService _authenticationService;
		#region Commands
		public RelayCommand LoginCommand { get; set; }
		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public SplashViewModel(INavigationService navigationService, IUserDataService userDataService, IAuthenticationService authenticationService)
			:base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_authenticationService = authenticationService;

			Init();

			RegisterCommands();
		}
		void Init()
		{
			//Register values
		}
		void RegisterCommands()
		{
			LoginCommand = new RelayCommand(Login);
		}


		#endregion

		#region Methods
		async void Login()
		{
			bool LoggedIn = false;
			User user = await GetUser();
			if (user != null)
			{
				try
				{
					LoggedIn = await _authenticationService.Login(user.RefreshToken);
				}
				catch (Exception)
				{
				}
			}
			if (LoggedIn)
			{
				HomeRedirect();
			}
			else
			{
				LoginRedirect();
			}
		}
		void HomeRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.HomePageKey);
		}
		void LoginRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.LoginPageKey);
		}
		#endregion
	}
}
