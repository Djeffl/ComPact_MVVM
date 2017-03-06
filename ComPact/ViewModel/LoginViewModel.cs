using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ComPact
{
	public class LoginViewModel: ViewModelBase
	{
		private readonly INavigationService _navigationService;
		public ICommand RegisterRedirectCommand { get; set; }
		public ICommand PasswordRetrievalRedirectCommand { get; set; }

		void RegisterRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.RegisterPageKey);
		}
		void PasswordRedirect()
		{
			_navigationService.NavigateTo(LocatorViewModel.PasswordRetrievalPageKey);
		}
		/**
		 * 
		 */
		public LoginViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;

			//Commands
			RegisterRedirectCommand = new RelayCommand(RegisterRedirect);
			PasswordRetrievalRedirectCommand = new RelayCommand(PasswordRedirect);


			//NextText = "Next";
			//WelcomeText = "Welcome to the MvvmLight example!";
		}
	}
}
