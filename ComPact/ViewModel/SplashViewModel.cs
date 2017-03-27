using System;
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
		private readonly IUserDataService _userDataService;

		#region Parameters
		/**
		 * Parameters
		 */
		private string _example;
		public string Example
		{
			get
			{
				return _example;
			}
			set
			{
				Set(ref _example, value);
			}
		}
		#endregion
		#region Commands
		public RelayCommand LoginStartCommand { get; set; }
		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public SplashViewModel(INavigationService navigationService, IUserDataService userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_userDataService = userDataService;

			Init();

			RegisterCommands();
		}
		void Init()
		{
			//Register values
		}
		void RegisterCommands()
		{
			LoginStartCommand = new RelayCommand(LoginStart);
		}


		#endregion

		#region Methods
		async void LoginStart()
		{
			if (await _userDataService.ControlToken())
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
