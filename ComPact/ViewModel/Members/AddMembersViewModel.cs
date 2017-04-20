using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ComPact.Helpers;
using ComPact.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Members
{
	public class AddMembersViewModel: BaseViewModel
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IUserDataService _userDataService;
		readonly IMemberDataService _memberDataService;
		readonly IDialogService _dialogService;
		readonly IPopUpService _popUpService;


		#region Parameters
		/**
		 * Parameters
		 */
		private Registration _registration = new Registration();
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
		public RelayCommand CreateMemberCommand { get; private set; }
		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public AddMembersViewModel(INavigationService navigationService, IUserDataService userDataService, IMemberDataService memberDataService, IDialogService dialogService, IPopUpService popUpService)
			:base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_memberDataService = memberDataService;
			_dialogService = dialogService;
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
			CreateMemberCommand = new RelayCommand(async () =>
			{
				await CreateMember();
			});
		}
		#endregion

		#region Methods
		void BackRedirect()
		{
			_navigationService.GoBack();
		}

		async Task CreateMember()
		{
			if (!(string.IsNullOrEmpty(Registration.Email) && string.IsNullOrEmpty(Registration.FirstName) && string.IsNullOrEmpty(Registration.LastName) && string.IsNullOrEmpty(Registration.Password) && string.IsNullOrEmpty(Registration.ConfirmPassword)))
			{
				if (EmailIsValid(Registration.Email))
				{
					if (Registration.Password == Registration.ConfirmPassword)
					{
						bool isSuccessful = await CreateMember(Registration.FirstName, Registration.LastName, Registration.Email, Registration.Password);
						if (isSuccessful)
						{
							_popUpService.Show("You succesfully created an account!", PopUpLength.Long);
							_navigationService.GoBack();
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

		async Task<bool> CreateMember(string firstName, string lastName, string email, string password)
		{
			bool isSuccessful = false;
			var member = new Member
			{
				Id = null,
				FirstName = firstName,
				LastName = lastName,
				Email = email,
				Password = password,
				AdminId = (await GetUser()).Id
			};
			try
			{
				await _memberDataService.Create(member);
				isSuccessful = true;
			}
			//TODO Add other exception catches
			catch (ArgumentException)
			{
				_dialogService.ShowMessage("This email is already being used.");
			}
			catch (Exception)
			{
				_dialogService.ShowMessage("Something went wrong, please try again later.");
			}
			return isSuccessful;
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
