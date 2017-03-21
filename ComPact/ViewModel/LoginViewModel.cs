using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;

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
		private RelayCommand _loginUserAsyncCommand;
		public RelayCommand LoginUserAsyncCommand
		{
			get
			{
				if (_loginUserAsyncCommand == null)
				{
					return _loginUserAsyncCommand
						?? (_loginUserAsyncCommand = new RelayCommand(
							async () =>
							{
						await LoginUserAsync();
							}));
				}
				return _loginUserAsyncCommand;
			}
		}
		#endregion
		#region Commands
		public RelayCommand RegisterRedirectCommand { get; set; }
		public RelayCommand PasswordRetrievalRedirectCommand { get; set; }
		public RelayCommand QrLoginRedirectCommand { get; set; }
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

			RegisterCommands();
			Init();
		}
		void Init()
		{
			//Register values
			//Register commands
			//if (_userDataService.HasUser())
			//{
			//	HomeRedirect();
			//}
		}
		void RegisterCommands()
		{
			RegisterRedirectCommand = new RelayCommand(RegisterRedirect);
			PasswordRetrievalRedirectCommand = new RelayCommand(PasswordRedirect);
			QrLoginRedirectCommand = new RelayCommand(QrLoginRedirect);
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
		void QrLoginRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.LoginQrPageKey);
		}

		async Task LoginUserAsync()
		{
			string email = Email;
			string password = Password;
			if (email != "" && password != "" && email != null && password != null)
			{
				try
				{
					bool positiveResponseCode = await _userDataService.LoginUserAsync(email, password);


					if (positiveResponseCode)
					{
						HomeRedirect();
						//_dialogService.ShowMessage("okey!");
					}

					else
					{
						_dialogService.ShowMessage("Your email and password do not match!");
					}
				}
				catch (Exception err)
				{
					//comment me
					_dialogService.ShowMessage(err.ToString());

					_dialogService.ShowMessage("Oops something went wrong");
				}
			}
			else
			{
				_dialogService.ShowMessage("Please fill in the fields.");
			}
		}
		#endregion
	}
}
