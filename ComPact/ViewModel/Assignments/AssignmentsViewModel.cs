using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
				_done = value; OnPropertyChanged();
			}
		}
		public List<Assignment> _assignments;
		public List<Assignment> Assignments
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


		#endregion
		#region Commands
		public RelayCommand AddAssignmentRedirectCommand { get; set; }
		public RelayCommand AssignmentDetailRedirectCommand { get; set; }
		public RelayCommand<int> AssignmentsPostionCommand { get; set; }

		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public AssignmentsViewModel(INavigationService navigationService, IAssignmentDataService assignmentDataService, IDialogService dialogService)
		{
			//Init Services
			_navigationService = navigationService;
			_dialogService = dialogService;
			_assignmentDataService = assignmentDataService;

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

			AssignmentsPostionCommand = new RelayCommand<int>(pos =>
			{
				Debug.WriteLine(pos);//Assignments[pos]);
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

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		//void 
		#endregion
	}
}
