using System;
using System.Threading.Tasks;
using ComPact.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.ViewModel
{
	public class LoginQrViewModel: ViewModelBase
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IBackService _backService;
		readonly IDialogService _dialogService;
		private readonly IUserDataService _userDataService;
		#region Parameters
		/**
		 * Parameters
		 */
		private string _email;
		public string Email
		{
			get
			{
				return _email;
			}
			set
			{
				Set(ref _email, value);
			}
		}
		private string _password;
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				Set(ref _password, value);
			}
		}
		#endregion
		#region Commands
		public RelayCommand BackRedirectCommand { get; set; }
		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public LoginQrViewModel(INavigationService navigationService, IDialogService dialogService, IBackService backService, IUserDataService userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_userDataService = userDataService;
			_dialogService = dialogService;
			_backService = backService;

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
		}
		#endregion

		#region Methods
		public async Task LoginUserAsync()
		{
			try
			{
				_dialogService.ShowMessage(Email + " " + Password);
				bool positiveResponseCode = await _userDataService.LoginUserAsync(Email, Password);


				if (positiveResponseCode)
				{
					HomeRedirect();
					_dialogService.ShowMessage("okey!");
				}

				else
				{
					_dialogService.ShowMessage("Invalid QR code");
					BackRedirect();
				}
			}
			catch (Exception err)
			{
				//comment me
				_dialogService.ShowMessage(err.ToString());

				_dialogService.ShowMessage("Oops something went wrong");
			}
		}
		void BackRedirect()
		{
			_backService.GoBack();
		}
		void HomeRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.HomePageKey);
		}
		#endregion
	}
}
