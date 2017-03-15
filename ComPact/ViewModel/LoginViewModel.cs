using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact
{
	public class LoginViewModel : ViewModelBase
	{
		private readonly INavigationService _navigationService;
		private readonly IUserDataService _userDataService;
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
		#endregion
		#region Commands
		public RelayCommand RegisterRedirectCommand { get; set; }
		public RelayCommand PasswordRetrievalRedirectCommand { get; set; }
		public RelayCommand LoginUserAsyncCommand { get; set; }
		#endregion
		#region Constructor
		/**
		 * 
		 */
		public LoginViewModel(INavigationService navigationService, IUserDataService userDataService, IDialogService dialogService)
		{
			_navigationService = navigationService;
			_userDataService = userDataService;
			_dialogService = dialogService;

			Init();

			RegisterCommands();
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
			LoginUserAsyncCommand = new RelayCommand(LoginUserAsync);
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
		void HomeRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.HomePageKey);
		}

		async void LoginUserAsync()
		{
			string email = Email;
			string password = Password;
			if (email != null && password != null)
			{
				try
				{
					var newUser = new User(null, null, null, email, password, null);
					Tuple<int, Object> tupleResponse = await _userDataService.LoginUserAsync(email, password);

					//Item1 = responsecode
					int responseCode = tupleResponse.Item1;
					//Item2 = UserId
					var userId = tupleResponse.Item2;
					_dialogService.ShowMessage(userId.ToString());
					if (responseCode == 400 || responseCode == 401)
					{
						_dialogService.ShowMessage("The email and password combination doesn't match.");
					}
					else
					{
						//HomeRedirect();
					}
				}
				catch (Exception err)
				{
					_dialogService.ShowMessage("Oops! Something went wrong, please try again later");
				}
			}
		}
		#endregion
	}
}
