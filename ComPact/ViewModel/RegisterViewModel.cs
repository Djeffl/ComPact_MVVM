using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using ComPact.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact
{
	public class RegisterViewModel : ViewModelBase
	{
		private readonly INavigationService _navigationService;
		private readonly IUserDataService _userDataService;
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
		public RegisterViewModel(INavigationService navigationService, IUserDataService userDataService, IDialogService dialogService, IBackService backService, IPopUpService popUpService)
		{
			_userDataService = userDataService;
			_navigationService = navigationService;
			_dialogService = dialogService;
			_backService = backService;
			_popUpService = popUpService;

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
			Admin = true;
			_registration = new Registration(FirstName, LastName, Email, Password, ConfirmPassword, Admin);
			string firstName = _registration.FirstName;
			string lastName = _registration.LastName;
			string email = _registration.Email;
			string password = _registration.Password;
			string confirmPassword = _registration.ConfirmPassword;
			bool admin = _registration.Admin;
			if (firstName != null && lastName != null && email != null && password != null && confirmPassword != null && admin != null)
			{

				if (EmailIsValid(email))
				{

					if (password == confirmPassword)
					{

						try
						{
							User user = await _userDataService.Get(email);
							_dialogService.ShowMessage("Email already taken!");
						}
						catch (Exception)
						{
							try
							{
								var newUser = new User
								{
									Id = null,
									FirstName = firstName,
									LastName = lastName,
									Email = email,
									Password = password,
									Admin = admin,
								};
								//User response = await _userDataService.Create(newUser);
								_popUpService.Show("Account created!", "long");
							}
							catch (Exception)
							{
								_dialogService.ShowMessage("Oops! Something went wrong, please try again later");
							}
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
			//_navigationService.
			_backService.GoBack();
		}

		void LoginRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.LoginPageKey);
		}

		async Task<User> CreateUserAsync(User user)
		{
			//1)Responsecode 2)UserObject
			User response = await _userDataService.Create(user);

			return response;
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