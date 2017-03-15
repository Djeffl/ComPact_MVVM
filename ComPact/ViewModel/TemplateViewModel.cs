using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.ViewModel
{
	public class TemplateViewModel: ViewModelBase
	{
		/**
		 * Delcare Services
		 */
		//private readonly INavigationService _navigationService;
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
		public TemplateViewModel(INavigationService navigationService)
		{
			//Init Services
			//_navigationService = navigationService;
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
