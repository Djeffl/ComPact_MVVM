using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using ComPact.Helpers;
using ComPact.Models;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Payments
{
	public class EditPaymentViewModel : BaseViewModel
	{
		/**
		 * Import Services
		 */
		readonly INavigationService _navigationService;
		readonly IPaymentDataService _paymentDataService;
		readonly IPopUpService _popUpService;
		#region Parameters
		/**
		 * Parameters
		 */
		Payment _payment = new Payment();
		public Payment Payment
		{
			get
			{
				return _payment;
			}
			set
			{
				_payment = value;
				RaisePropertyChanged(nameof(Payment));
			}
		}
		#endregion
		#region Commands
		public RelayCommand EditPaymentRedirectCommand { get; set; }
		public RelayCommand BackRedirectCommand { get; set; }
		public RelayCommand UpdatePaymentCommand { get; set; }
		public RelayCommand<Payment> SetPaymentCommand { get; set; }

		#endregion
		#region Constructor
		public EditPaymentViewModel(INavigationService navigationService, IUserDataService userDataService, IPaymentDataService paymentDataService, IPopUpService popUpService)
			: base(userDataService)
		{
			_navigationService = navigationService;
			_paymentDataService = paymentDataService;
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
			EditPaymentRedirectCommand = new RelayCommand(EditPaymentRedirect);
			UpdatePaymentCommand = new RelayCommand(async () =>
			{
				await UpdatePayment();
			});
			SetPaymentCommand = new RelayCommand<Payment>((payment) =>
			{
				Payment = payment;
			});
		}

	#endregion
		#region Methods
		void EditPaymentRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.EditPaymentPageKey);
		}
		async Task UpdatePayment()
		{
			try
			{
				await _paymentDataService.Update(Payment);
				_popUpService.Show("Payment successfully updated!", "long");
				//TODO NAVIGEER TERUG
				_navigationService.GoBack();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		#endregion
	}

}
