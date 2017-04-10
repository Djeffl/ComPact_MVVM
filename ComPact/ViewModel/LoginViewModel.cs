using ComPact.Services;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact
{
	public class LoginViewModel : BaseViewModel
	{
		private readonly INavigationService _navigationService;
		private readonly IAuthenticationService _authenticationService;
		private readonly IDialogService _dialogService;
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
		//private RelayCommand _loginUserAsyncCommand;
		//public RelayCommand LoginUserAsyncCommand
		//{
		//	get
		//	{
		//		if (_loginUserAsyncCommand == null)
		//		{
		//			return _loginUserAsyncCommand
		//				?? (_loginUserAsyncCommand = new RelayCommand(
		//					async () =>
		//					{
		//				await LoginUserAsync();
		//					}));
		//		}
		//		return _loginUserAsyncCommand;
		//	}
		//}
		#endregion
		#region Commands
		public RelayCommand RegisterRedirectCommand { get; set; }
		public RelayCommand PasswordRetrievalRedirectCommand { get; set; }
		public RelayCommand QrLoginRedirectCommand { get; set; }
		public RelayCommand LoginCommand { get; set; }
		#endregion
		#region Constructor
		/**
		 * 
		 */
		public LoginViewModel(INavigationService navigationService, IUserDataService userDataService, IAuthenticationService authenticationService, IDialogService dialogService)
			:base(userDataService)
		{
			_navigationService = navigationService;
			_authenticationService = authenticationService;
			_dialogService = dialogService;

			RegisterCommands();
			Init();


		}
		void Init()
		{
			//Register values
			//Register commands
		}
		void RegisterCommands()
		{
			RegisterRedirectCommand = new RelayCommand(RegisterRedirect);
			PasswordRetrievalRedirectCommand = new RelayCommand(PasswordRedirect);
			QrLoginRedirectCommand = new RelayCommand(QrLoginRedirect);
			LoginCommand = new RelayCommand(Login);
		}
		#endregion

		#region Methods
		void RegisterRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.RegisterPageKey);
		}
		void PasswordRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.PasswordRetrievalPageKey);
		}
		void QrLoginRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.LoginQrPageKey);
		}

		async void Login()
		{
			bool isSuccessful = await _authenticationService.Login(Email, Password);
			//ClearFields();
			if (isSuccessful)
			{
				_navigationService.NavigateTo(LocatorViewModel.HomePageKey);
			}
		}
		//void ClearFields()
		//{
		//	Email = "";
		//	Password = "";
		//}
		#endregion
	}
}
