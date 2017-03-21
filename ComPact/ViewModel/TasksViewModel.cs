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
		private string _describtion;
		public string Descrition
		{
			get
			{
				return _describtion;
			}
			set
			{
				Set(ref _describtion, value);
			}
		}
		#endregion
		#region Commands
		public RelayCommand CreateTaskAsyncCommand { get; set; }
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

			Init();

			RegisterCommands();
		}
		void Init()
		{
			//Register values
		}
		void RegisterCommands()
		{
			CreateTaskAsyncCommand = new RelayCommand(CreateTaskAsync);
		}
		#endregion

		#region Methods
		void CreateTaskAsync()
		{
			//Do other stuff send to db and stuff
			Task newTask = new Task(ItemName, Descrition);
			_dialogService.ShowMessage(ItemName);
		}
		#endregion
	}
}
