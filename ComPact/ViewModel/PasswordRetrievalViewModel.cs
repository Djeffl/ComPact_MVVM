using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace ComPact
{
	public class PasswordRetrievalViewModel: ViewModelBase
	{
		private readonly INavigationService _navigationService;

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
		public PasswordRetrievalViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			//Commands
		}
		
	}
}
