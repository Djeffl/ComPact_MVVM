using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ComPact.Helpers;
using ComPact.Models;
using ComPact.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Members
{
	public class AddAssignmentViewModel : ViewModelBase
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
		private ObservableCollection<string> _assignmentsOptions = new ObservableCollection<string>();
		public ObservableCollection<string> AssignmentsOptions
		{
			get
			{
				return new ObservableCollection<string>() { "Choose an option...", "Take out trash", "Groceries", "Feed pet", "Other" };
			}
			//set
			//{
			//	Set(ref _assignmentsOptions, value);
			//}
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
		private string memberEmail;
		private string item;


		#endregion
		#region Commands
		public RelayCommand CreateTaskCommand { get; set; }
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand<Member> MemberSelectedCommand { get; set; } 
		public RelayCommand<int> AssignmentsOptionsCommand { get; set; }
		public RelayCommand GetMembersCommand { get; set; }


		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public AddAssignmentViewModel(INavigationService navigationService, IAssignmentDataService assignmentDataService, IMemberDataService memberDataService, IDialogService dialogService, IPopUpService popUpService, IUserDataService userDataService)
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
			CreateTaskCommand = new RelayCommand(CreateTask);
			BackRedirectCommand = new RelayCommand(_navigationService.GoBack);

			MemberSelectedCommand = new RelayCommand<Member>(user =>
			{
				System.Diagnostics.Debug.WriteLine(user.Email);
				memberEmail = user.Email;
			});

			//Returns position itemSelected
			AssignmentsOptionsCommand = new RelayCommand<int>(pos =>
			{
				Debug.WriteLine(AssignmentsOptions[pos]);
				item = AssignmentsOptions[pos];
			});

			GetMembersCommand = new RelayCommand(async () =>
			{
				await GetMembers();
			});


		}

		async
		#endregion

		#region Methods
		void CreateTask()
		{
			//TODO Create dataService that 
			//_userDataService

			User user = await _userDataService.GetUser();

			var newTask = new Assignment
			{
				LoginToken = user.LoginToken,
				MemberEmail = memberEmail,
				ItemName = item,
				Description = Description,

			};
			//try
			//{
			await _assignmentDataService.Create(newTask);
			//}
			//catch (Exception ex)
			//{
			//}
			_popUpService.Show("Task succesfully created!", "long");
		}

		public async Task GetMembers()
		{
			IEnumerable<Member> list = await _memberDataService?.GetAll();
			Members = Convert<Member>(list);
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
		#endregion
	}
}
