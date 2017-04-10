using ComPact.Helpers;
using ComPact.Services;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact
{
	public class SettingsViewModel: BaseViewModel
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IBackService _backService;
		readonly IAuthenticationService _authenticationService;

		#region Parameters
		/**
		 * Parameters
		 */
		string _example;
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
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand LogOutCommand { get; set; }
		public RelayCommand MembersRedirectCommand { get; set; }

		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public SettingsViewModel(INavigationService navigationService, IUserDataService userDataService, IAuthenticationService authenticationService)
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
			BackRedirectCommand = new RelayCommand(BackRedirect);
			LogOutCommand = new RelayCommand(LogOut);
			MembersRedirectCommand = new RelayCommand(MembersPageRedirect);
		}
		#endregion

		#region Methods
		void BackRedirect()
		{
			_navigationService.GoBack();
		}

		async void LogOut()
		{
			bool isSuccessful = await _authenticationService.LogOut();
			if (isSuccessful)
			{
				_navigationService.NavigateTo(LocatorViewModel.LoginPageKey);
			}

		}
		void MembersPageRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.MembersPageKey);
		}
		#endregion
	}
}