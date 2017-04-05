using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ComPact.Helpers;
using ComPact.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Assignments
{
	public class DetailAssignmentViewModel: ViewModelBase
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IAssignmentDataService _assignmentDataService;
		readonly IMemberDataService _memberDataService;
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
				Set(ref _assignment, value);
			}
		}
		private string memberEmail;
		private string item;


		#endregion
		#region Commands
		public RelayCommand EditRedirectCommand { get; set; }
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand GetMembersCommand { get; set; }

		public RelayCommand GetUserCommand { get; private set; }


		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public DetailAssignmentViewModel(INavigationService navigationService, IAssignmentDataService assignmentDataService, IMemberDataService memberDataService, IDialogService dialogService, IPopUpService popUpService, IUserDataService userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_assignmentDataService = assignmentDataService;
			_memberDataService = memberDataService;
			_dialogService = dialogService;
			_popUpService = popUpService;
			_userDataService = userDataService;

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
				await GetUser();
			});
		}

		#endregion

		#region Methods
		void EditRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.EditAssignmentPageKey, Assignment);
		}
		public async Task GetUser()
		{
			User = await _userDataService.GetUser();
		}
		#endregion
	}
}
