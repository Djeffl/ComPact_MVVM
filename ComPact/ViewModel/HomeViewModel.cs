using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.ViewModel
{
	public class HomeViewModel: BaseViewModel
	{
		/**
		 * Delcare Services
		 */
		private readonly INavigationService _navigationService;
		//private readonly IDialogService _dialogService;
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
		public RelayCommand HelpRedirectCommand { get; set; }
		public RelayCommand SettingsRedirectCommand { get; set; }
		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public HomeViewModel(INavigationService navigationService, IUserDataService userDataService)
			:base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			//_userDataService = userDataService;
			//_dialogService = dialogService;

			Init();

			RegisterCommands();
		}
		void Init()
		{
			//Register values
		}
		void RegisterCommands()
		{
			HelpRedirectCommand = new RelayCommand(HelpPageRedirect);
			SettingsRedirectCommand = new RelayCommand(SettingsPageRedirect);
		}
		#endregion

		#region Methods
		void HelpPageRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.HelpPageKey);
		}
		void SettingsPageRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.SettingsPageKey);
		}
		#endregion
	}
}
