using System;
using System.Threading.Tasks;
using ComPact.Extensions;
using ComPact.Helpers;
using ComPact.Models;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact
{
	public class DetailLocationViewModel : BaseViewModel
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IDialogService _dialogService;
		readonly IUserDataService _userDataService;
		readonly ILocationDataService _locationDataService;
		readonly IPopUpService _popUpService;

		#region Parameters
		/**
		 * Parameters
		*/
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

		Location _location = new Location();
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

		#endregion
		#region Commands
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand LoadDataCommand { get; set; }
		public RelayCommand EditLocationRedirectCommand { get; set; }
		public RelayCommand<Location> SetLocationCommand { get; set; }
		public RelayCommand DeleteLocationCommand { get; set; }
		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public DetailLocationViewModel(INavigationService navigationService, IUserDataService userDataService, ILocationDataService locationDataService, IPopUpService popUpService, IDialogService dialogService)
			: base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_userDataService = userDataService;
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
			BackRedirectCommand = new RelayCommand(BackRedirect);
			LoadDataCommand = new RelayCommand(async () =>
			{
				await LoadData();
			});
			EditLocationRedirectCommand = new RelayCommand(DetailLocationRedirect);
			SetLocationCommand = new RelayCommand<Location>(SetLocation);
			DeleteLocationCommand = new RelayCommand(async () =>
			{
				await DeleteLocation();
			});
		}

		#endregion
		#region Methods
		void BackRedirect()
		{
			_navigationService.GoBack();
		}

		async Task LoadData()
		{
			User = await GetUser();
			Location = await _locationDataService.Get(Location.Id, User.Admin);
		}

		async Task DeleteLocation()
		{
			var result = await _dialogService.ShowMessage("Are you sure?", "Delete Payment");

			if (result)
			{
				await _locationDataService.Delete((string)Location.Id);
				_popUpService.Show("Succesfully deleted", PopUpLength.Long);
				_navigationService.GoBack();
			}
		}

		void DetailLocationRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.EditLocationPagekey, Location);
		}

		void SetLocation(Location obj)
		{
			Location = obj;
		}

		#endregion
	}
}
