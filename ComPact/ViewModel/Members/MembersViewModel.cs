using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.ViewModel.Members
{
	public class MembersViewModel: BaseViewModel
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IMemberDataService _memberDataService;
		#region Parameters
		/**
		 * Parameters
		 */
		ObservableCollection<Member> _members = new ObservableCollection<Member>();
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
			}
		}
		#endregion
		#region Commands
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand AddMembersRedirectCommand { get; set; }
		public RelayCommand LoadDataCommand { get; set; }

		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public MembersViewModel(INavigationService navigationService, IUserDataService userDataService, IMemberDataService memberDataService)
			:base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_memberDataService = memberDataService;

			Init();

			RegisterCommands();
		}
		void Init()
		{
			//Register values
		}
		void RegisterCommands()
		{
			BackRedirectCommand = new RelayCommand(BackRedirect);
			AddMembersRedirectCommand = new RelayCommand(AddMembersRedirect);
			LoadDataCommand = new RelayCommand(async () =>
			{
				await LoadData();
			});
		}
		#endregion

		#region Methods
		void BackRedirect()
		{
			_navigationService.GoBack();
		}
		void AddMembersRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.AddMembersPageKey);
		}
		async Task LoadData()
		{
			Members = Convert<Member>(await _memberDataService.GetAll());
		}
		#endregion
	}
}
