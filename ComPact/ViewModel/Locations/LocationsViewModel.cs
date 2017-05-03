using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ComPact.Extensions;
using ComPact.Models;
using ComPact.Services;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Locations
{
	public class LocationsViewModel : BaseViewModel
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IAssignmentDataService _assignmentDataService;
		readonly IDialogService _dialogService;
		readonly IUserDataService _userDataService;
		readonly ILocationDataService _locationDataService;

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

		ObservableCollection<Location> _locations;
		public ObservableCollection<Location> Locations
		{
			get
			{
				return _locations;
			}
			set
			{
				_locations = value;
				RaisePropertyChanged(nameof(Locations));
			}
		}

		#endregion
		#region Commands
		public RelayCommand AddLocationRedirectCommand { get; set; }
		public RelayCommand LoadDataCommand { get; set; }
		public RelayCommand<Location> DetailLocationRedirectCommand { get; set; }
		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public LocationsViewModel(INavigationService navigationService, IUserDataService userDataService, ILocationDataService locationDataService)
			: base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_userDataService = userDataService;
			_locationDataService = locationDataService;

			Init();

			RegisterCommands();
		}

		void Init()
		{
			//Register values
		}

		void RegisterCommands()
		{
			AddLocationRedirectCommand = new RelayCommand(AddLocationRedirect);
			LoadDataCommand = new RelayCommand(async () =>
			{
				await LoadData();
			});
			DetailLocationRedirectCommand = new RelayCommand<Location>(DetailLocationRedirect);
		}
		#endregion
		#region Methods
		void AddLocationRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.AddLocationPageKey);
		}

		async Task LoadData()
		{
			User = await GetUser();
			Locations = (await _locationDataService.GetAll(User.Admin)).Convert<Location>(); ;
		}

		void DetailLocationRedirect(Location location)
		{
			_navigationService.NavigateTo(LocatorViewModel.DetailLocationPagekey, location);
		}
		#endregion
	}
}
