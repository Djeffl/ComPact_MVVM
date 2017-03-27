using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Members
{
	public class AddTaskViewModel : ViewModelBase
	{
		/**
		 * Declare Services
		 */
		private readonly INavigationService _navigationService;
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

		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public AddTaskViewModel(INavigationService navigationService)
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
		}
		#endregion

		#region Methods

		#endregion
	}
}
