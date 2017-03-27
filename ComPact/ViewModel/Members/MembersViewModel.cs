using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.ViewModel.Members
{
	public class MembersViewModel: ViewModelBase
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		//private readonly IUserDataService _userDataService;
		//private readonly IDialogService _dialogService;
		#region Parameters
		/**
		 * Parameters
		 */
		private string _example;
		public string Example
		{
			get
			{
				return _example;
			}
			set
			{
				Set(ref _example, value);
			}
		}
		#endregion
		#region Commands
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand AddMembersRedirectCommand { get; set; }

		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public MembersViewModel(INavigationService navigationService)
		{
			//Init Services
			_navigationService = navigationService;
			//_userDataService = userDataService;
			//_dialogService = dialogService;

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
			AddMembersRedirectCommand = new RelayCommand(AddMembersRedirect);
		}
		#endregion

		#region Methods
		void BackRedirect()
		{
			_navigationService.GoBack();
		}
		void AddMembersRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.AddMembersPageKey);
		}
		#endregion
	}
}
