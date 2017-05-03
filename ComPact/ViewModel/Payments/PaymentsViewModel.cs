using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ComPact.Models;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Payments
{
	public class PaymentsViewModel : BaseViewModel
	{
		readonly INavigationService _navigationService;
		readonly IPaymentDataService _paymentDataService;
		readonly IMemberDataService _memberDataService;

		#region Parameters
		/**
		 * Parameters
		 */
		ObservableCollection<Payment> _payments = new ObservableCollection<Payment>();
		public ObservableCollection<Payment> Payments
		{
			get
			{
				return _payments;
			}
			set
			{
				_payments = value;
				RaisePropertyChanged(nameof(Payments));
			}
		}
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
		#endregion
		#region Commands
		public RelayCommand AddPaymentRedirectCommand { get; set; }
		public RelayCommand LoadDataCommand { get; set; }
		public RelayCommand<Payment> DetailPaymentRedirectCommand { get; set; }
		#endregion
		#region Constructor
		/**
		 * Init services & Init() & RegisterCommands();
		 */
		public PaymentsViewModel(INavigationService navigationService, IUserDataService userDataService, IPaymentDataService paymentDataService, IMemberDataService memberDataService)
			: base(userDataService)
		{
			//Init Services
			_navigationService = navigationService;
			_paymentDataService = paymentDataService;
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
			AddPaymentRedirectCommand = new RelayCommand(PaymentRedirect);
			LoadDataCommand = new RelayCommand(async () =>
			{
				await LoadData();
			});
			DetailPaymentRedirectCommand = new RelayCommand<Payment>(DetailPaymentRedirect);
		}


		#endregion

		#region Methods
		void PaymentRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.AddPaymentPageKey);
		}

		async Task LoadData()
		{
			User = await GetUser();
			Payments = Convert(await _paymentDataService.GetAll((await GetUser()).Admin));
		}
		void DetailPaymentRedirect(Payment payment)
		{
			_navigationService.NavigateTo(LocatorViewModel.DetailPaymentPageKey, payment);
		}
		#endregion
	}
}
