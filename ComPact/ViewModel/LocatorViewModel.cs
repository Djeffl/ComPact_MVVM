using ComPact.Assignments;
using ComPact.Locations;
using ComPact.Members;
using ComPact.Payments;
using ComPact.Repositories;
using ComPact.Services;
using ComPact.ViewModel;
using ComPact.ViewModel.Members;
using ComPact.WebServices;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ComPact
{
	public class LocatorViewModel
	{
		/**
		 * Set a key for each page => App.cs
		 */
		public const string SplashPageKey = "SplashPageKey";
		public const string LoginPageKey = "LoginPage";
		public const string LoginQrPageKey = "LoginQrPage";
		public const string RegisterPageKey = "RegisterPage";
		public const string PasswordRetrievalPageKey = "PasswordRetrievalPage";
		public const string HomePageKey = "HomePagekey";
		public const string HelpPageKey = "HelpPageKey";
		public const string SettingsPageKey = "SettingsPageKey"
			;
		public const string TasksPageKey = "TasksPageKey";
		public const string AddTaskPageKey = "AddTaskPageKey";
		public const string DetailAssignmentPageKey = "DetailAssignmentPageKey";
		public const string EditAssignmentPageKey = "EditAssignmentPageKey";

		public const string MembersPageKey = "MembersPageKey";
		public const string AddMembersPageKey = "AddMembersPagekey";

		public const string PaymentsPageKey = "PaymentsPageKey";
		public const string AddPaymentPageKey = "AddPaymentPageKey";
		public const string DetailPaymentPageKey = "DetailPaymentPageKey";
		public const string EditPaymentPageKey = "EditPaymentPageKey";

		public const string LocationsPageKey = "LocationsPageKey";
		public const string AddLocationPageKey = "AddLocationPageKey";
		public const string DetailLocationPagekey = "DetailLocationPageKey";
		public const string EditLocationPagekey = "EditLocationPagekey";

		public LoginViewModel LoginViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<LoginViewModel>();
			}
		}

		//public LoginQrViewModel LoginQrViewModel
		//{
		//	get
		//	{
		//		return ServiceLocator.Current.GetInstance<LoginQrViewModel>();
		//	}
		//}

		public SplashViewModel SplashViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SplashViewModel>();
			}
		}

		public RegisterViewModel RegisterViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<RegisterViewModel>();
			}
		}

		public PasswordRetrievalViewModel PasswordRetrievalViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<PasswordRetrievalViewModel>();
			}
		}

		public HomeViewModel HomeViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<HomeViewModel>();
			}
		}

		public HelpViewModel HelpViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<HelpViewModel>();
			}
		}

		public SettingsViewModel SettingsViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SettingsViewModel>();
			}
		}
		#region Assignments
		public AssignmentsViewModel TasksViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<AssignmentsViewModel>();
			}
		}

		public AddAssignmentViewModel AddTaskViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<AddAssignmentViewModel>();
			}
		}

		public DetailAssignmentViewModel DetailAssignmentViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<DetailAssignmentViewModel>();
			}
		}

		public EditAssignmentViewModel EditAssignmentViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<EditAssignmentViewModel>();
			}
		}
		#endregion

		#region Members
		public MembersViewModel MembersViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MembersViewModel>();
			}
		}
		public AddMembersViewModel AddMembersViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<AddMembersViewModel>();
			}
		}
		#endregion
		#region Payments
		public PaymentsViewModel PaymentsViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<PaymentsViewModel>();
			}
		}
		public AddPaymentViewModel AddPaymentViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<AddPaymentViewModel>();
			}
		}
		public DetailPaymentViewModel DetailPaymentViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<DetailPaymentViewModel>();
			}
		}
		public EditPaymentViewModel EditPaymentViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<EditPaymentViewModel>();
			}
		}
		#endregion
		#region Locations
		public LocationsViewModel LocationsViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<LocationsViewModel>();
			}
		}
		public AddLocationViewModel AddLocationViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<AddLocationViewModel>();
			}
		}
		public DetailLocationViewModel DetailLocationViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<DetailLocationViewModel>();
			}
		}
		public EditLocationViewModel EditLocationViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<EditLocationViewModel>();
			}
		}
		#endregion
		/**
		 * Register every ViewModel to the IOC container
		 */
		public LocatorViewModel()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
			RegisterRepositories();
			RegisterWebServices();
			RegisterServices();
			RegisterViewModels();
		}

		void RegisterViewModels()
		{
			SimpleIoc.Default.Register<SplashViewModel>();
			SimpleIoc.Default.Register<LoginViewModel>();
			SimpleIoc.Default.Register<LoginQrViewModel>();
			SimpleIoc.Default.Register<RegisterViewModel>();
			SimpleIoc.Default.Register<PasswordRetrievalViewModel>();
			SimpleIoc.Default.Register<HomeViewModel>();
			SimpleIoc.Default.Register<HelpViewModel>();
			SimpleIoc.Default.Register<SettingsViewModel>();

			SimpleIoc.Default.Register<AssignmentsViewModel>();
			SimpleIoc.Default.Register<AddAssignmentViewModel>();
			SimpleIoc.Default.Register<DetailAssignmentViewModel>();
			SimpleIoc.Default.Register<EditAssignmentViewModel>();

			SimpleIoc.Default.Register<MembersViewModel>();
			SimpleIoc.Default.Register<AddMembersViewModel>();

			SimpleIoc.Default.Register<PaymentsViewModel>();
			SimpleIoc.Default.Register<AddPaymentViewModel>();
			SimpleIoc.Default.Register<DetailPaymentViewModel>();
			SimpleIoc.Default.Register<EditPaymentViewModel>();

			SimpleIoc.Default.Register<LocationsViewModel>();
			SimpleIoc.Default.Register<AddLocationViewModel>();
			SimpleIoc.Default.Register<DetailLocationViewModel>();
			SimpleIoc.Default.Register<EditLocationViewModel>();
		}

		void RegisterServices()
		{
			SimpleIoc.Default.Register<IRepositoryMapper, RepositoryMapper>();
			SimpleIoc.Default.Register<IWebMapper, WebMapper>();
			SimpleIoc.Default.Register<IApiService, ApiService>();
			SimpleIoc.Default.Register<IFileDownloadService, FileDownloadService>();

			SimpleIoc.Default.Register<IMemberDataService, MemberDataService>();
			SimpleIoc.Default.Register<IAssignmentDataService, AssignmentDataService>();
			SimpleIoc.Default.Register<IUserDataService, UserDataService>();
			SimpleIoc.Default.Register<IAuthenticationService, AuthenticationService>();
			SimpleIoc.Default.Register<IPaymentDataService, PaymentDataService>();
			SimpleIoc.Default.Register<ILocationDataService, LocationDataService>();
		}
		void RegisterWebServices()
		{
			SimpleIoc.Default.Register<IMemberWebService, MemberWebService>();
			SimpleIoc.Default.Register<IAssignmentWebService, AssignmentWebService>();
			SimpleIoc.Default.Register<IUserWebService, UserWebService>();
			SimpleIoc.Default.Register<IPaymentWebService, paymentWebService>();
			SimpleIoc.Default.Register<ILocationWebService, LocationWebService>();

		}
		void RegisterRepositories()
		{
			SimpleIoc.Default.Register<IUserRepository, UserRepository>();
			SimpleIoc.Default.Register<IMemberRepository, MemberRepository>();
			SimpleIoc.Default.Register<IAssignmentRepository, AssignmentRepository>();
			SimpleIoc.Default.Register<IPaymentRepository, PaymentRepository>();
			SimpleIoc.Default.Register<ILocationRepository, LocationRepository>();
			SimpleIoc.Default.Register<ILocationMemberRepository, LocationMemberRepository>();
		}
	}
}
