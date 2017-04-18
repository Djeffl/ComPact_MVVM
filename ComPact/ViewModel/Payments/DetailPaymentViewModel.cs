using System;
using System.Globalization;
using System.Threading.Tasks;
using ComPact.Helpers;
using ComPact.Models;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Payments
{
	public class DetailPaymentViewModel : BaseViewModel
	{
		/**
		 * Import Services
		 */
		readonly INavigationService _navigationService;
		readonly IPaymentDataService _paymentDataService;
		readonly IMemberDataService _memberDataService;
		readonly IPopUpService _popUpService;
		readonly IDialogService _dialogService;
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
		public RelayCommand DeletePaymentCommand { get; set; }
		#endregion
		#region Constructor
		public DetailPaymentViewModel(INavigationService navigationService, IUserDataService userDataService, IPaymentDataService paymentDataService, IPopUpService popUpService, IDialogService dialogService)
			: base(userDataService)
		{
			_navigationService = navigationService;
			_paymentDataService = paymentDataService;
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
			EditPaymentRedirectCommand = new RelayCommand(EditPaymentRedirect);
			DeletePaymentCommand = new RelayCommand(async () =>
			{
				await DeletePayment();
			});
		}

		#endregion
		#region Methods
		void EditPaymentRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.EditPaymentPageKey);
		}
		async Task DeletePayment()
		{
			var result = await _dialogService.ShowMessage("Are you sure?", "test");

			if (result)
			{
				await _paymentDataService.Delete(Payment.Id);
				_navigationService.GoBack();
				_popUpService.Show("Payment successfully deleted!", "long");
			}
			


		}
		#endregion
	}

}
