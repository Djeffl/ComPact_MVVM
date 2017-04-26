using ComPact.Helpers;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact
{
	public class PasswordRetrievalViewModel: BaseViewModel
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IMemberDataService _userDataService;
		readonly IPopUpService _popUpService;
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
		#endregion
		#region Commands
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand PasswordResetCommand { get; set; }
		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public PasswordRetrievalViewModel(INavigationService navigationService, IUserDataService userDataService, IMemberDataService memberDataService, IPopUpService popUpService)
			:base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_popUpService = popUpService;

			Init();

			RegisterCommands();
		}
		void Init()
		{
			//Register values
		}
		void RegisterCommands()
		{
			BackRedirectCommand = new RelayCommand(_navigationService.GoBack);
			PasswordResetCommand = new RelayCommand(PasswordReset);
		}
		#endregion

		#region Methods
		void PasswordReset()
		{
			var user = new Member
			{
				Email = Email
			};
			//_userDataService.Forgot(user);
			_popUpService.Show("Please check your mail", PopUpLength.Long);
			_navigationService.GoBack();
		}
		#endregion
		
	}
}
