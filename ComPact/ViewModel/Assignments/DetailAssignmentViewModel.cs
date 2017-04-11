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

		private Member _member;
		public Member Member
		{
			get
			{
				return _member;
			}
			set
			{
				Debug.WriteLine(_member);
				Set(ref _member, value);
			}
		}
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
		public RelayCommand<string> GetMemberCommand { get; set; }
		public RelayCommand DeleteAssignmentCommand { get; set; }
		public RelayCommand GetUserCommand { get; private set; }


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

			GetUserCommand = new RelayCommand(async () =>
			{
				User = await GetUser();
			});
			GetMemberCommand = new RelayCommand<string>(async(id) =>
			{
				await GetMember(id);
			});
			DeleteAssignmentCommand = new RelayCommand(async () =>
			{
				await DeleteAssignment();
				_popUpService.Show("Succesfully deleted", "long");
				_navigationService.GoBack();
			});
		}

		#endregion

		#region Methods
		void EditRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.EditAssignmentPageKey, Assignment);
		}

		async Task GetMember(string id)
		{
			if (User.Admin)
			{
				Member = await _memberDataService?.Get(id);
			}
		}
		async Task<bool> DeleteAssignment()
		{
			return await _assignmentDataService.Delete(Assignment.Id);
		}
		#endregion
	}
}
