using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ComPact.Helpers;
using ComPact.Models;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Assignments
{
	public class DetailAssignmentViewModel: BaseViewModel
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IAssignmentDataService _assignmentDataService;
		readonly IMemberDataService _memberDataService;
		readonly IPopUpService _popUpService;
		readonly IDialogService _dialogService;

		#region Parameters
		/**
		 * Parameters
		 */
		//private string _itemName;
		//public string ItemName
		//{
		//	get
		//	{
		//		return _itemName;
		//	}
		//	set
		//	{
		//		Set(ref _itemName, value);
		//	}
		//}
		//private string _description;
		//public string Description
		//{
		//	get
		//	{
		//		return _description;
		//	}
		//	set
		//	{
		//		Set(ref _description, value);
		//	}
		//}

		User _user = new User();
		public User User
		{
			get
			{
				return _user;
			}
			set
			{
				Set(ref _user, value);
			}
		}
		Assignment _assignment;
		public Assignment Assignment
		{
			get
			{
				return _assignment;
			}
			set
			{
				value.ItemName = FirstCharToUpper(value.ItemName);
				Set(ref _assignment, value);
			}
		}
		private string memberEmail;
		private string item;


		#endregion
		#region Commands
		public RelayCommand EditRedirectCommand { get; set; }
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand DeleteAssignmentCommand { get; set; }
		public RelayCommand<Models.Assignment> SetAssignmentCommand { get; set; }
		public RelayCommand LoadDataCommand { get; set; }

		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public DetailAssignmentViewModel(INavigationService navigationService, IAssignmentDataService assignmentDataService, IMemberDataService memberDataService, IDialogService dialogService, IPopUpService popUpService, IUserDataService userDataService)
			:base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_assignmentDataService = assignmentDataService;
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
			EditRedirectCommand = new RelayCommand(EditRedirect);
			BackRedirectCommand = new RelayCommand(_navigationService.GoBack);
			DeleteAssignmentCommand = new RelayCommand(async () =>
			{
				await DeleteAssignment();
			});
			LoadDataCommand = new RelayCommand(async () =>
			{
				await LoadData();
			});
			SetAssignmentCommand = new RelayCommand<Models.Assignment>((assignment) =>
			{
				SetAssignment(assignment);
			});
		}

		#endregion

		#region Methods
		void EditRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.EditAssignmentPageKey, Assignment);
		}

		async Task DeleteAssignment()
		{
			var result = await _dialogService.ShowMessage("Are you sure?", "Delete Payment");

			if (result)
			{
				await _assignmentDataService.Delete((string)Assignment.Id);
				_popUpService.Show("Succesfully deleted", PopUpLength.Long);
				_navigationService.GoBack();
			}
		}
		void SetAssignment(Models.Assignment assignment)
		{
			Assignment = assignment;
		}
		async Task LoadData()
		{
			User = await GetUser();
			Assignment = await _assignmentDataService.Get((string)Assignment.Id, User.Admin);
		}
		#endregion
	}
}
