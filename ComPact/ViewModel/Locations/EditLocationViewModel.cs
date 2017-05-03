using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ComPact.Extensions;
using ComPact.Helpers;
using ComPact.Models;
using ComPact.Services;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Locations
{
	public class EditLocationViewModel : BaseViewModel
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly ILocationDataService _locationDataService;
		readonly IMemberDataService _memberDataService;
		readonly IPopUpService _popUpService;
		readonly IDialogService _dialogService;

		#region Parameters
		/**
		 * Parameters
		*/
		Location _location;
		public Location Location
		{
			get
			{
				return _location;
			}
			set
			{
				_location = value;
				RaisePropertyChanged(nameof(Location));
			}
		}

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

		bool _isVisibleElementLocation;
		public bool IsVisibleElementLocation
		{
			get
			{
				return _isVisibleElementLocation;
			}
			set
			{
				_isVisibleElementLocation = value;
				RaisePropertyChanged(nameof(IsVisibleElementLocation));
			}
		}
		#endregion
		#region Commands
		public RelayCommand<Location> SetLocationCommand { get; set; }
		public RelayCommand ChangeVisibilityCommand { get; set; }
		public RelayCommand UpdateLocationCommand { get; set; }
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand<Member> MemberSelectedCommand { get; set; }
		public RelayCommand<int> AssignmentsOptionsCommand { get; set; }
		public RelayCommand LoadDataCommand { get; set; }
		public RelayCommand<Member> AddMemberCommand { get; set; }

		#endregion
		#region Constructor
		public EditLocationViewModel(INavigationService navigationService, IUserDataService userDataService, ILocationDataService locationDataService, IMemberDataService memberDataService, IDialogService dialogService, IPopUpService popUpService)
			: base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_locationDataService = locationDataService;
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
			ChangeVisibilityCommand = new RelayCommand(ChangeVisibility);

			SetLocationCommand = new RelayCommand<Location>(location =>
			{
				Location = location;
			});

			UpdateLocationCommand = new RelayCommand(async () =>
			{
				await UpdateLocation(Location);
			});

			LoadDataCommand = new RelayCommand(async () =>
			{
				await LoadData();
			});

			AddMemberCommand = new RelayCommand<Member>(AddMember);
		}
		#endregion
		#region Methods
		void ChangeVisibility()
		{
			IsVisibleElementLocation = !IsVisibleElementLocation;
		}

		void AddMember(Member mem)
		{

			//if (Location.Members.Contains(member))
			//{
			//	Location.Members.Remove(member);
			//}
			//else
			//{
			//	Location.Members.Add(member);
			//}
			List<string> membersIds = new List<string>();
			foreach (var member in Location.Members)
			{
				membersIds.Add(member.Id);
			}

			if ( membersIds.Contains(mem.Id))
			{
				int index = membersIds.IndexOf(mem.Id);
				bool tas = Location.Members.Remove(Location.Members[index]);
			}
			else
			{
				Location.Members.Add(mem);
			}
		}

		async Task UpdateLocation(Location location)
		{
			try
			{
				await _locationDataService.Update(location);
				_popUpService.Show("Location successfully updated!", PopUpLength.Long);
				_navigationService.GoBack();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}
		}

		async Task LoadData()
		{
			await LoadMembers();
		}

		async Task LoadMembers()
		{
			IEnumerable<Member> list = await _memberDataService?.GetAll();
			Members = list.Convert<Member>();
		}
		#endregion
	}
}
