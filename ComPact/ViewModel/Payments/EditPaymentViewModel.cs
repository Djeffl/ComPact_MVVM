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
	public class EditPaymentViewModel : BaseViewModel
	{
		/**
		 * Import Services
		 */
		readonly INavigationService _navigationService;
		readonly IPaymentDataService _paymentDataService;
		readonly IMemberDataService _memberDataService;
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
		#endregion
		#region Constructor
		public EditPaymentViewModel(INavigationService navigationService, IUserDataService userDataService, IPaymentDataService paymentDataService)
			: base(userDataService)
		{
			_navigationService = navigationService;
			_paymentDataService = paymentDataService;

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

		}
		#endregion
		#region Methods
		void EditPaymentRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.EditPaymentPageKey);
		}

		#endregion
	}

}
