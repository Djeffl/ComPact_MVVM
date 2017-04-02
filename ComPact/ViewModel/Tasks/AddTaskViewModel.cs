using System;
using System.Diagnostics;
using ComPact.Helpers;
using ComPact.Models;
using ComPact.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Members
{
	public class AddTaskViewModel : ViewModelBase
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly ITaskDataService _taskDataService;
		readonly IUserDataService _userDataService;
		readonly IPopUpService _popUpService;
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
		private User _user;
		public User User
		{
			get
			{
				return _user;
			}
			set
			{
				Debug.WriteLine(_user);
				Set(ref _user, value);
			}
		}

		#endregion
		#region Commands
		public RelayCommand CreateTaskCommand { get; set; }
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand<User> MemberCommand { get; set; }
		public RelayCommand<int> ItemNameCommand { get; set; }

		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public AddTaskViewModel(INavigationService navigationService, ITaskDataService taskDataService,IUserDataService userDataService, IDialogService dialogService, IPopUpService popUpService)
		{
			//Init Services
			_navigationService = navigationService;
			_taskDataService = taskDataService;
			_userDataService = userDataService;
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
			CreateTaskCommand = new RelayCommand(CreateTask);
			ItemNameCommand = new RelayCommand<int>(pos => 
			{
				System.Diagnostics.Debug.WriteLine(pos);
			});
			MemberCommand = new RelayCommand<User>(user =>
			{
				System.Diagnostics.Debug.WriteLine(user);
			});
			BackRedirectCommand = new RelayCommand(_navigationService.GoBack);
		}
		#endregion

		#region Methods
		void CreateTask()
		{
			//TODO Create dataService that 
			//_userDataService

			var newTask = new Task
			{
				ItemName = ItemName,
				Description = Description,
				//Implement id's
			};
			//try
			//{
				_taskDataService.Create(newTask);
			//}
			//catch (Exception ex)
			//{
			//}
			_popUpService.Show("Task succesfully created!", "long");
		}
		#endregion
	}
}
