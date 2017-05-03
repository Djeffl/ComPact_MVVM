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
		readonly INavigationService _navigationService;
		readonly IAuthenticationService _authenticationService;
		readonly IDialogService _dialogService;
		readonly IPopUpService _popUpService;
		#region Parameters
		/**
		 * Parameters
		 */
		Registration _registration = new Registration();
		public Registration Registration
		{
			get
			{
				return _registration;
			}
			set
			{
				_registration = value;
                RaisePropertyChanged(nameof(Registration));
			}
		}
		#endregion
		#region Commands
		//private RelayCommand _registerUserAsyncCommand;
		//public RelayCommand RegisterUserAsyncCommand
		//{
		//	get
		//	{
		//		if (_registerUserAsyncCommand == null)
		//		{
		//			return _registerUserAsyncCommand
		//				?? (_registerUserAsyncCommand = new RelayCommand(
		//					async () =>
		//					{
		//						await RegisterUser();
		//					}));
		//		}
		//		return _registerUserAsyncCommand;
		//	}
		//}
		public RelayCommand RegisterUserCommand{get;set;}
		public RelayCommand BackRedirectCommand { get; set; }
		#endregion

		#region Constructor
		public RegisterViewModel(INavigationService navigationService, IUserDataService userDataService, IAuthenticationService authenticationService, IDialogService dialogService, IPopUpService popUpService)
			:base(userDataService)
		{
			_authenticationService = authenticationService;
			_navigationService = navigationService;
			_dialogService = dialogService;
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
			RegisterUserCommand = new RelayCommand(async () =>
			{
				await RegisterUser();
			});
			BackRedirectCommand = new RelayCommand(BackRedirect);
		}
		#endregion

		#region Methods
		async Task RegisterUser()
		{
			if (!(string.IsNullOrEmpty(Registration.Email) && string.IsNullOrEmpty(Registration.FirstName) && string.IsNullOrEmpty(Registration.LastName) && string.IsNullOrEmpty(Registration.Password) && string.IsNullOrEmpty(Registration.ConfirmPassword))) // && admin != null)
			{
				if (EmailIsValid(Registration.Email))
				{
					if (Registration.Password == Registration.ConfirmPassword)
					{
						bool isSuccessful = await _authenticationService.Register(Registration.FirstName, Registration.LastName, Registration.Email, Registration.Password, Registration.Admin);
						if (isSuccessful)
						{
							_popUpService.Show("You succesfully created an account!", PopUpLength.Long); 
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