using System;
using System.Threading.Tasks;
using ComPact.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.ViewModel
{
	public class SplashViewModel : ViewModelBase
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
		public SplashViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
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
			bool LoggedIn = await _authenticationService.AuthenticateToken();
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
