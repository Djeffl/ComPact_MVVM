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

namespace ComPact.Members
{
	public class AddAssignmentViewModel : BaseViewModel
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
		string _itemName;
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
		string _description;
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
		Assignment _ass;
		public Assignment Ass
		{
			get
			{
				return _ass;
			}
			set
			{
				Set(ref _ass, value);
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
		Member _selectedMember;
		public Member SelectedMember
		{
			get
			{
				return _selectedMember;
			}
			set
			{
				_selectedMember = value;
				RaisePropertyChanged(nameof(SelectedMember));
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

		#endregion
		#region Commands
		public RelayCommand CreateTaskCommand { get; set; }
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand<Member> MemberSelectedCommand { get; set; } 
		public RelayCommand<int> AssignmentsOptionsCommand { get; set; }
		public RelayCommand GetMembersCommand { get; set; }
		public RelayCommand<Assignment> CreateAssignmentCommand { get; set; }


		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public AddAssignmentViewModel(IUserDataService userDataService, INavigationService navigationService, IAssignmentDataService assignmentDataService, IMemberDataService memberDataService, IDialogService dialogService, IPopUpService popUpService)
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
			CreateTaskCommand = new RelayCommand(CreateTask);
			BackRedirectCommand = new RelayCommand(_navigationService.GoBack);

			MemberSelectedCommand = new RelayCommand<Member>(member =>
			{
				SelectedMember = member;
			});

			GetMembersCommand = new RelayCommand(async () =>
			{
				await GetMembers();
			});
			CreateAssignmentCommand = new RelayCommand<Assignment>(async (assignment) =>
			{
				await CreateAssignment(assignment);
			});


		}

		
		#endregion

		#region Methods
		async void CreateTask()
		{
			//TODO Create dataService that 
			//_userDataService

			User user = await GetUser();

			//var newTask = new Assignment
			//{
			//	LoginToken = user.LoginToken,
			//	MemberEmail = SelectedMember.Email,
			//	ItemName = ItemName,
			//	Description = Description,
			//	IconName = IconName
			//};
			//try
			//{
			//await _assignmentDataService.Create(newTask);
			//}
			//catch (Exception ex)
			//{
			//}
			_popUpService.Show("Task succesfully created!", PopUpLength.Long);
		}

		async Task CreateAssignment(Assignment assignment)
		{
			//TODO Create dataService that 
			try
			{
			//	//Member mem = assignment.Member;
			//	//assignment.Member = new Member
			//	//{
			//	//	AdminId = mem.AdminId,
			//	//	Id = "58f8688afa3bfe02e185b68c"
			//	//}
			//}
			string adminId = (await UserDataService?.GetUser()).Id;
			assignment.AdminId = adminId;
			await _assignmentDataService.Create(assignment);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			_popUpService.Show("Task succesfully created!", PopUpLength.Long);
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
