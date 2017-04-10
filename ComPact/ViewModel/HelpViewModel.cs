using ComPact.Helpers;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact
{
	public class HelpViewModel: BaseViewModel
	{
		/**
		 * Declare Services
		 */
		readonly INavigationService _navigationService;
		readonly IBackService _backService;
		//private readonly IDialogService _dialogService;
		#region Parameters
		/**
		 * Parameters
		 */
		private string _example;
		public string Example
		{
			get
			{
				return _example;
			}
			set
			{
				Set(ref _example, value);
			}
		}
		#endregion
		#region Commands
		public RelayCommand BackRedirectCommand { get; set; }
		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public HelpViewModel(INavigationService navigationService, IUserDataService userDataService, IBackService backService)
			:base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_backService = backService;

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
		}
		#endregion

		#region Methods
		void BackRedirect()
		{
			_backService.GoBack();
		}
		#endregion
	}
}