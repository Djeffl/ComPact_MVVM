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
public class AddLocationViewModel : BaseViewModel
{
	/**
	 * Import Services
	 */
	readonly INavigationService _navigationService;
	readonly IMemberDataService _membersDataService;
	readonly ILocationDataService _locationDataService;
	readonly IPopUpService _popUpService;
	readonly IDialogService _dialogService;
	#region Parameters
	/**
	 * Parameters
	 */
	Location _location = new Location
	{
		Members = new List<Member>()
	};
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
	public RelayCommand CreateLocationCommand { get; set; }
	public RelayCommand BackRedirectCommand { get; set; }
	public RelayCommand ChangeVisibilityCommand { get; set; }
	public RelayCommand LoadDataCommand { get; set; }
	public RelayCommand<Member> AddMemberCommand { get; set; }

	#endregion
	#region Constructor
	public AddLocationViewModel(INavigationService navigationService, IUserDataService userDataService, IMemberDataService membersDataService, ILocationDataService locationDataService, IPopUpService popUpService, IDialogService dialogService)
		: base(userDataService)
	{
		_navigationService = navigationService;
		_membersDataService = membersDataService;
		_locationDataService = locationDataService;
		_popUpService = popUpService;
		_dialogService = dialogService;

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
		CreateLocationCommand = new RelayCommand(async () =>
		{
			await CreateLocation();
		});
		ChangeVisibilityCommand = new RelayCommand(ChangeVisibility);
		LoadDataCommand = new RelayCommand(async () =>
		{
			await LoadData();
		});
		AddMemberCommand = new RelayCommand<Member>(AddMember);
	}

	#endregion
	#region Methods
	async Task CreateLocation()
	{
		System.Diagnostics.Debug.WriteLine(Location);
		System.Diagnostics.Debug.WriteLine(Location.Members.Count);
		try
		{
			string adminId = (await UserDataService?.GetUser()).Id;
			Location.AdminId = adminId;
			await _locationDataService.Create(Location);
			_navigationService.GoBack();
		}
		catch (ArgumentException)
		{
			_dialogService.ShowMessage("Please fill in all the fields");
		}
		catch (Exception ex)
		{
			_dialogService.ShowMessage(ex.ToString());
		}
	}

	void ChangeVisibility()
	{
		IsVisibleElementLocation = !IsVisibleElementLocation;
	}

	async Task LoadData()
	{
			Members = (await _membersDataService.GetAll()).Convert<Member>();
	}

	void AddMember(Member member)
	{
		if (Location.Members.Contains(member))
		{
			Location.Members.Remove(member);
		}
		else
			{
				Location.Members.Add(member);
			}
		}	
		#endregion
	}
}
