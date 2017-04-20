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

		public RelayCommand LoadDataCommand { get; set; }

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

			LoadDataCommand = new RelayCommand(async () =>
			{
				await LoadData();
			});
			AssignmentDoneCommand = new RelayCommand<Assignment>(async (assignment) =>
			{
				await AssignmentDone(assignment);
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

		async Task LoadData()
		{
			User = await GetUser();
			Assignments = await GetAssignments();
		}

		async Task<ObservableCollection<Assignment>> GetAssignments()
		{
			IEnumerable<Assignment> assignments = await _assignmentDataService?.GetAllUnfinished(User.Admin);
			return Convert<Assignment>(assignments);
		}
		async Task AssignmentDone(Assignment assignment)
		{
			bool isConfirmed = await _dialogService.ShowMessage("Are you sure?", "Finish Task");
			if (isConfirmed)
			{
				Assignment data = new Assignment
				{
					Done = true,
					Id = assignment.Id

				};
				await _assignmentDataService.Update(data);
				Assignments.Remove(assignment);
			}
		}
		#endregion
	}
}
