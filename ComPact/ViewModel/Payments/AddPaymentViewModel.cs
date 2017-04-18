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
	public class AddPaymentViewModel : BaseViewModel
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
		Payment _payment = new Payment
		{
			Name = null,
			Description = null
		};
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
		public RelayCommand CreatePaymentCommand { get; set; }
		public RelayCommand BackRedirectCommand { get; set; }
		#endregion
		#region Constructor
		public AddPaymentViewModel(INavigationService navigationService, IUserDataService userDataService, IPaymentDataService paymentDataService, IPopUpService popUpService, IDialogService dialogService)
			: base(userDataService)
		{
			_navigationService = navigationService;
			_paymentDataService = paymentDataService;
			_dialogService = dialogService;
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
			CreatePaymentCommand = new RelayCommand(async () =>
			{
				await CreatePayment();
			});
		}
		#endregion
		#region Methods
		async Task CreatePayment()
		{
			try
			{
				Payment.CreatedAt = DateTime.Now.ToLocalTime();
				string memberId = (await UserDataService?.GetUser()).Id;
				Payment.MemberId = memberId;
				await _paymentDataService.Create(Payment);
				_popUpService.Show("Payment succesfully created", "long");
			}
			catch (ArgumentException)
			{
				_dialogService.ShowMessage("Please fill in all the fields");
			}
			catch (Exception ex)
			{
				_dialogService.ShowMessage(ex.ToString());
			}
			_payment.Description = null;
			_payment.Name = null;

			_navigationService.GoBack();
		}


		#endregion
	}
	
}
