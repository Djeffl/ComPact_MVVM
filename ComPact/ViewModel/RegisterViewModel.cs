using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ComPact.Helpers;
using ComPact.Services;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact
{
	public class RegisterViewModel : BaseViewModel
	{
		private readonly INavigationService _navigationService;
		private readonly IAuthenticationService _authenticationService;
		private readonly IDialogService _dialogService;
		private readonly IBackService _backService;
		private readonly IPopUpService _popUpService;
		#region Parameters
		/**
		 * Parameters
		 */
		private string _firstName;
		public string FirstName
		{
			get
			{
				return _firstName;
			}
			set
			{
				Set(ref _firstName, value);
			}
		}
		private string _lastName;
		public string LastName
		{
			get
			{
				return _lastName;
			}
			set
			{
				Set(ref _lastName, value);
			}
		}
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
		private string _confirmPassword;
		public string ConfirmPassword
		{
			get
			{
				return _confirmPassword;
			}
			set
			{
				Set(ref _confirmPassword, value);
			}
		}
		private bool _admin;
		public bool Admin
		{
			get
			{
				return _admin;
			}
			set
			{
				Set(ref _admin, value);
			}
		}
		private Registration _registration;
		public Registration Registration
		{
			get
			{
				return _registration;
			}
			set
			{
				Set(ref _registration, value);
			}
		}
		#endregion
		#region Commands
		private RelayCommand _registerUserAsyncCommand;
		public RelayCommand RegisterUserAsyncCommand
		{
			get
			{
				if (_registerUserAsyncCommand == null)
				{
					return _registerUserAsyncCommand
						?? (_registerUserAsyncCommand = new RelayCommand(
							async () =>
							{
								await RegisterUserAsync();
							}));
				}
				return _registerUserAsyncCommand;
			}
		}

		private RelayCommand _backRedirectCommand;
		public RelayCommand BackRedirectCommand
		{
			get
			{
				if (_backRedirectCommand == null)
				{
					return _backRedirectCommand = new RelayCommand(BackRedirect);
				}
				return _backRedirectCommand;
			}
		}
		#endregion

		#region Constructor
		public RegisterViewModel(INavigationService navigationService, IUserDataService userDataService, IAuthenticationService authenticationService, IDialogService dialogService)
			:base(userDataService)
		{
			_authenticationService = authenticationService;
			_navigationService = navigationService;
			_dialogService = dialogService;

			Init();
		}

		void Init()
		{	
			//Register values
			//Register commands
			RegisterCommands();
		}

		void RegisterCommands()
		{

		}
		#endregion

		#region Methods
		/**
		 * Normaal gezien wil ik dat deze functie een registeration parameter krijgt dat doorgegeven wordt pas op de onclick
		 * Dit blijkt niet mogelijk te zijn aangezien enkel een binding dynamisch zijn waarden aanpast in een setCommand 
		 * Een binding is enkel mogelijk door visuals te koppelen aan waarden imo
		 */
		async Task RegisterUserAsync()
		{
			_registration = new Registration()
			{
				FirstName = FirstName,
				LastName = LastName,
				Email = Email,
				Password = Password,
				ConfirmPassword = ConfirmPassword,
				Admin = true
			};
			if (_registration.Email != null && _registration.FirstName != null && _registration.LastName != null && _registration.Password != null && _registration.ConfirmPassword != null) // && admin != null)
			{
				if (EmailIsValid(_registration.Email))
				{

					if (_registration.Password == _registration.ConfirmPassword)
					{
						bool isSuccessful = await _authenticationService.Register(_registration.FirstName, _registration.LastName, _registration.Email, _registration.Password, _registration.Admin);
						if (isSuccessful)
						{
							_popUpService.Show("You succesfully created an account!", "long"); 
							_navigationService.NavigateTo(LocatorViewModel.HomePageKey);
						}
					}
					else
					{
						_dialogService.ShowMessage("Please make sure your passwords match eachother!");
					}
				}
				else
				{
					_dialogService.ShowMessage("Email is not valid!");
				}
			}
			else
			{
				_dialogService.ShowMessage("Fill all fields in please");
			}
		}

		void BackRedirect()
		{
			_navigationService.GoBack();
		}

		bool EmailIsValid(string email)
		{
			string expresion;
			expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
			if (Regex.IsMatch(email, expresion))
			{
				if (Regex.Replace(email, expresion, string.Empty).Length == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
		#endregion



	}
}