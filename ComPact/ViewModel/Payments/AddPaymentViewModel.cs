using System;
using ComPact.Helpers;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Payments
{
	public class AddPaymentViewModel
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
		#endregion
		#region Commands

		#endregion
		#region Constructor
		public AddPaymentViewModel(INavigationService navigationService, IPaymentDataService paymentDataService)
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
			
		}
		#endregion
		#region Methods

		#endregion
	}
}
