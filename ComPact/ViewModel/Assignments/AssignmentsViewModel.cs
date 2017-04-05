using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ComPact.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.ViewModel
{
	public class AssignmentsViewModel: ViewModelBase, INotifyPropertyChanged
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


		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public AssignmentsViewModel(INavigationService navigationService, IAssignmentDataService assignmentDataService, IDialogService dialogService, IUserDataService userDataService)
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
				await GetUser();
			});
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
		void DetailAssignmentRedirect(Assignment assignment)
		{
			_navigationService.NavigateTo(LocatorViewModel.DetailAssignmentPageKey, assignment);
		}

		public async Task GetAssignments()
		{
			User responseUser = await _userDataService.GetUser();
			IEnumerable<Assignment> list = await _assignmentDataService?.GetAssignments(responseUser.LoginToken);
			Assignments = Convert<Assignment>(list);
		}
		public async Task GetUser()
		{
			User = await _userDataService.GetUser();
		}

		//If you're working with non-generic IEnumerable you can do it this way:
		ObservableCollection<object> Convert(IEnumerable original)
		{
			return new ObservableCollection<object>(original.Cast<object>());
		}
		//If you're working with generic IEnumerable<T> you can do it this way:
		ObservableCollection<T> Convert<T>(IEnumerable<T> original)
		{
			return new ObservableCollection<T>(original);
		}
		//If you're working with non-generic IEnumerable but know the type of elements, you can do it this way:
		ObservableCollection<T> Convert<T>(IEnumerable original)
		{
			return new ObservableCollection<T>(original.Cast<T>());
		}
		//void 
		#endregion
	}
}
