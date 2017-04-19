using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ComPact.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.ViewModel
{
	public class AssignmentsViewModel: BaseViewModel, INotifyPropertyChanged
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IAssignmentDataService _assignmentDataService;
		readonly IDialogService _dialogService;
		readonly IUserDataService _userDataService;

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
		private bool _done;
		public bool Done
		{
			get
			{
				return _done;
			}
			set
			{
				//_isSelected = value;
				//MyBoundMessage = _isSelected ? Msg1 : Msg2;
				//NotifyPropertyChanged(() => IsSelected);

				Set(ref _done, value);
			}
		}
		ObservableCollection<Assignment> _assignments;
		public ObservableCollection<Assignment> Assignments
		{
			get
			{
				return _assignments;
			}
			set
			{
				Set(ref _assignments, value);
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
				_assignment = value;
			}
		}

		#endregion
		#region Commands
		public RelayCommand AddAssignmentRedirectCommand { get; set; }
		public RelayCommand<Assignment> DetailAssignmentRedirectCommand { get; set; }
		public RelayCommand<int> AssignmentsPostionCommand { get; set; }

		public RelayCommand GetAssignmentsCommand { get; set; }

		public RelayCommand GetUserCommand { get; private set; }
		public RelayCommand<Assignment> AssignmentDoneCommand { get; set; }


		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public AssignmentsViewModel(INavigationService navigationService, IAssignmentDataService assignmentDataService, IDialogService dialogService, IUserDataService userDataService)
			:base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_dialogService = dialogService;
			_assignmentDataService = assignmentDataService;
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
			AddAssignmentRedirectCommand = new RelayCommand(AddTaskRedirect);
			DetailAssignmentRedirectCommand = new RelayCommand<Assignment>(DetailAssignmentRedirect);
			AssignmentsPostionCommand = new RelayCommand<int>(pos =>
			{
				Debug.WriteLine(pos);//Assignments[pos]);
			});

			GetAssignmentsCommand = new RelayCommand(async () =>
			{
				await GetAssignments();
			});
			GetUserCommand = new RelayCommand(async () =>
			{
				User = await GetUser();
			});
			AssignmentDoneCommand = new RelayCommand<Assignment>(async (assignment) =>
			{
				await AssignmentDone(assignment);
				Assignments.Remove(assignment);
			});
		}


		#endregion

		#region Methods
		void AddTaskRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.AddTaskPageKey);
		}
		void DetailAssignmentRedirect(Assignment assignment)
		{
			_navigationService.NavigateTo(LocatorViewModel.DetailAssignmentPageKey, assignment);
		}

		public async Task GetAssignments()
		{
			User responseUser = await _userDataService.GetUser();
			IEnumerable<Assignment> assignments = await _assignmentDataService?.GetAllUnfinished();
			Assignments = Convert<Assignment>(assignments);
		}
		public async Task AssignmentDone(Assignment assignment)
		{
			assignment.AdminId = null;
			assignment.Description = null;
			assignment.IconName = null;
			assignment.ItemName = null;
			assignment.Member = null;
			assignment.Done = true;
			await _assignmentDataService.Update(assignment);
		}
		#endregion
	}
}
