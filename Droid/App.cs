using System;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Threading;

namespace ComPact.Droid
{
	public static class App
	{
		private static LocatorViewModel _locator;
		public static LocatorViewModel Locator
		{
			get
			{
				if (_locator == null)
				{
					// Initialize the MVVM Light DispatcherHelper.
					// This needs to be called on the UI thread.
					DispatcherHelper.Initialize();

					// Configure and register the MVVM Light NavigationService
					var nav = new NavigationService();
					//var uds = new UserDataService(new UserWebservice());

					SimpleIoc.Default.Register<INavigationService>(() => nav);
					//SimpleIoc.Default.Register<IUserDataService>(() => uds);

					RegisterViews(nav);

					RegisterIoc();

					_locator = new LocatorViewModel();
				}

				return _locator;
			}
		}
		/**
		 * Register every page with the key
		 */
		private static void RegisterViews(NavigationService nav)
		{
			nav.Configure(LocatorViewModel.LoginPageKey, typeof(LoginActivity));
			nav.Configure(LocatorViewModel.RegisterPageKey, typeof(RegisterActivity));
			nav.Configure(LocatorViewModel.PasswordRetrievalPageKey, typeof(PasswordRetrievalActivity));
		}
		private static void RegisterIoc()
		{
			SimpleIoc.Default.Register<IDeviceInfo, DeviceInfo>();
		}
	}
}
