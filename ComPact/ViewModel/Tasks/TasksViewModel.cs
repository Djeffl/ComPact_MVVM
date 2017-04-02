using System;
using ComPact.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.ViewModel
{
	public class TasksViewModel: ViewModelBase
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IDialogService _dialogService;

		#region Parameters
		/**
		 * Parameters
		 */
		private string _itemName;
		public string ItemName
		{
			get
			{
				return _itemName;
			}
			set
			{
				Set(ref _itemName, value);
			}
		}
		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				Set(ref _description, value);
			}
		}

		#endregion
		#region Commands
		public RelayCommand AddTaskRedirectCommand { get; set; }

		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public TasksViewModel(INavigationService navigationService, IDialogService dialogService)
		{
			//Init Services
			_navigationService = navigationService;
			_dialogService = dialogService;
			//_userDataService = userDataService;

			Init();

			RegisterCommands();
		}
		void Init()
		{
			//Register values
		}
		void RegisterCommands()
		{
			AddTaskRedirectCommand = new RelayCommand(AddTaskRedirect);
		}
		#endregion

		#region Methods
		//void CreateTaskAsync()
		//{
		//	//Do other stuff send to db and stuff
		//	Task newTask = new Task
		//	{
		//		ItemName = ItemName,
		//		Description = Description
		//	};
		//	_dialogService.ShowMessage(ItemName);
		//}
		void AddTaskRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.AddTaskPageKey);
		}
		#endregion
	}
}
