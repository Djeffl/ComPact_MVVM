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

		#endregion
		#region Commands
		public RelayCommand<Assignment> SetAssignmentCommand { get; set; }
		public RelayCommand UpdateAssignmentCommand { get; set; }
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand<Member> MemberSelectedCommand { get; set; }
		public RelayCommand<int> AssignmentsOptionsCommand { get; set; }
		public RelayCommand LoadDataCommand { get; set; }


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
			BackRedirectCommand = new RelayCommand(_navigationService.GoBack);

			SetAssignmentCommand = new RelayCommand<Assignment>(assignment =>
			{
				Assignment = assignment;
			});

			UpdateAssignmentCommand = new RelayCommand(async () =>
			{
				await UpdateAssignment(Assignment);
			});

			MemberSelectedCommand = new RelayCommand<Member>(user =>
			{
				System.Diagnostics.Debug.WriteLine(user.Email);
			});

			//Returns position itemSelected
			//AssignmentsOptionsCommand = new RelayCommand<int>(pos =>
			//{
			//	Debug.WriteLine(AssignmentsOptions[pos]);
			//	item = AssignmentsOptions[pos];
			//});

			LoadDataCommand = new RelayCommand(async () =>
			{
				await LoadData();
			});
		}

		async
		#endregion

		#region Methods
		Task UpdateAssignment(Assignment assignment)
		{
			try
			{
				await _assignmentDataService.Update(assignment);
				_popUpService.Show("Task successfully updated!", PopUpLength.Long);
				//TODO NAVIGEER TERUG
				_navigationService.GoBack();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		async Task LoadData()
		{
			await LoadMembers();
		}

		async Task LoadMembers()
		{
			IEnumerable<Member> list = await _memberDataService?.GetAll();
			Members = Convert<Member>(list);
		}
		#endregion
	}
}
