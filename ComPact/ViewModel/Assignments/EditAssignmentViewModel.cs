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
	public class EditAssignmentViewModel : BaseViewModel
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

		private Member _user;
		public Member User
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
		Assignment _assignment;
		public Assignment Assignment
		{
			get
			{
				return _assignment;
			}
			set
			{
				_assignment = value;
				RaisePropertyChanged(nameof(Assignment));
				//Set(ref _members, value);
			}
		}
		private ObservableCollection<Member> _members = new ObservableCollection<Member>();
		public ObservableCollection<Member> Members
		{
			get
			{
				return _members;
			}
			set
			{


				_members = value;
				RaisePropertyChanged(nameof(Members));
				//Set(ref _members, value);
			}
		}
		string _iconName;
		public string IconName
		{
			get
			{
				return _iconName;
			}
			set
			{
				_iconName = value;
				RaisePropertyChanged(nameof(IconName));
			}
		}
		private string memberEmail;
		private string item;


		#endregion
		#region Commands
		public RelayCommand<Assignment> UpdateAssignmentCommand { get; set; }
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand<Member> MemberSelectedCommand { get; set; }
		public RelayCommand<int> AssignmentsOptionsCommand { get; set; }
		public RelayCommand GetMembersCommand { get; set; }


		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public EditAssignmentViewModel(INavigationService navigationService, IUserDataService userDataService,IAssignmentDataService assignmentDataService, IMemberDataService memberDataService, IDialogService dialogService, IPopUpService popUpService)
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
			UpdateAssignmentCommand = new RelayCommand<Assignment>(async (assignment) =>
			{
				await UpdateAssignment(assignment);
			});
			BackRedirectCommand = new RelayCommand(_navigationService.GoBack);

			MemberSelectedCommand = new RelayCommand<Member>(user =>
			{
				System.Diagnostics.Debug.WriteLine(user.Email);
				memberEmail = user.Email;
			});

			//Returns position itemSelected
			//AssignmentsOptionsCommand = new RelayCommand<int>(pos =>
			//{
			//	Debug.WriteLine(AssignmentsOptions[pos]);
			//	item = AssignmentsOptions[pos];
			//});

			GetMembersCommand = new RelayCommand(async () =>
			{
				await GetMembers();
			});


		}

		async
		#endregion

		#region Methods
		Task UpdateAssignment(Assignment assignment)
		{
			try
			{
				string adminId = (await UserDataService?.GetUser()).Id;
				assignment.AdminId = adminId;
				await _assignmentDataService.Update(assignment);
				_popUpService.Show("Task successfully updated!", "long");
				//TODO NAVIGEER TERUG
				_navigationService.NavigateTo(LocatorViewModel.HomePageKey);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		public async Task GetMembers()
		{
			IEnumerable<Member> list = await _memberDataService?.GetAll();
			Members = Convert<Member>(list);
		}
		#endregion
	}
}
