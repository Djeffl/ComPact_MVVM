using System;
using System.Threading.Tasks;
using ComPact.Helpers;
using ComPact.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.ViewModel
{
	public class LoginQrViewModel : BaseViewModel
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IBackService _backService;
		readonly IDialogService _dialogService;
		private readonly IAuthenticationService _authenticationService;
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

		public RelayCommand<Member> ScanningFinishedCommand { get; set; }
		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public LoginQrViewModel(INavigationService navigationService, IUserDataService userDataService, IDialogService dialogService, IBackService backService, IAuthenticationService authenticationService)
			:base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_authenticationService = authenticationService;
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
			ScanningFinishedCommand = new RelayCommand<Member>(async (obj) => await Login(obj));

		}
		#endregion

		#region Methods
		public async Task Login(Member user)
		{
			try
			{
				//await _authenticationService.AuthenticateEmailAndPassword(user.Email, user.Password);
			}
			catch (Exception err)
			{
#if DEBUG
				_dialogService.ShowMessage(err.ToString());
#endif

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
