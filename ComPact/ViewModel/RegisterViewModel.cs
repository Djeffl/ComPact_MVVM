using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
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
		//public ICommand RegisterUserAsyncCommand { get; set; }

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


		private RelayCommand<Registration> _registerUserAsyncCommand;
		public RelayCommand<Registration> RegisterUserAsyncCommand;
		//{
		//	get
		//	{
		//		return _registerUserAsyncCommand 
		//			?? (_registerUserAsyncCommand = new RelayCommand<Registration>(async registration =>
		//		{
		//			var service = _userDataService;
		//			var result =  await RegisterUserAsync(registration);
		//			//_navigationService.NavigateTo("loggedInPage");
		//			//return result;
		//		}));
		//	}
		//}


		private async Task<string> RegisterUserAsync()
		{

			//if (emailIsValid(emailTxt))
			//{
			//if (passwordTxt == confirmPswTxt && passwordTxt != "")
			//{
			//try
			//{
			string firstName = _firstName;
			string lastName = _lastName;
			string email = _email;
			string password = _password;
			string confirmPassword = _confirmPassword;
			bool admin = true;

			//string firstName = RegisterInfo.FirstName;
			//string lastName = RegisterInfo.LastName;
			//string email = RegisterInfo.Email;
			//string password = RegisterInfo.Password;
			//bool admin = RegisterInfo.Admin;

			User newUser = new User(null, firstName, lastName, email, password, admin);
			return await this.CreateUserAsync(newUser);
			//string msg = "Your account has succesfully been created";

			//if (response == "400")
			//{
			//	dialogBuilder(this, "error " + response, "Email is already taken!").Show();
			//}
			//else
			//{
			//	Toast.MakeText(this, "Account created", ToastLength.Long).Show();
			//}
			//}
			//catch (Exception err)
			//{
			//	dialogBuilder(this, "error " + response, "Oops! Something went wrong, please try again later").Show();
			//}
			//}
			//else
			//{
			//	dialogBuilder(this, "passwords do not match!", "Please make sure your passwords match eachother!").Show();
			//}
		}


		private async Task<string> CreateUserAsync(User user)
		{
			string response = await _userDataService.CreateUserAsync(user);

			return response;
		}
		private static bool emailIsValid(string email)
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





		//public RegisterViewModel()
		//{
		//	DetailText = "This is a detail page";
		//}
		//public RegisterViewModel(INavigationService navgiationService, IUserDataService userDataService, IDialogService dialogService)
		//{
		//	_dialogService = dialogService;
		//	//_navigationService.NavigateTo(LocatorViewModel.);
		//	_userDataService = userDataService;

		//	//Commands
		//	//RegisterUserAsyncCommand = new RelayCommand<string>(RegisterUserAsync);

		//		//(string firstName, string lastName, string email, string password) => RegisterUserAsync(firstName, lastName, email, password));

		//}
		public RegisterViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			_userDataService = new UserDataService(new UserWebservice());
			//_dialogService = new DialogService();
		}
	}
}