using System;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Threading;
using Android.Content;
using ComPact.Helpers;
using ComPact.Droid.Helpers;
using ComPact.Droid.Activities;

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
					//var dpAlrt = new DialogService();

					SimpleIoc.Default.Register<INavigationService>(() => nav);
					//SimpleIoc.Default.Register<IDialogService>(() => dpAlrt);

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
			nav.Configure(LocatorViewModel.HomePageKey, typeof(HomeActivity));
		}

		private static void RegisterIoc()
		{
			SimpleIoc.Default.Register<IDialogService, DialogService>();
			SimpleIoc.Default.Register<IBackService, BackService>();
			SimpleIoc.Default.Register<IPopUpService, PopUpService>();
		}
	}
}
