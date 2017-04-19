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
using Onboarding.Droid.Helpers;
using ComPact.Droid.Members;
using ComPact.Droid.Assignments;
using ComPact.Droid.Tasks;
using ComPact.Droid.Payments;
using ComPact.Services;

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

					SimpleIoc.Default.Register<INavigationService>(() => nav);
	
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
			nav.Configure(LocatorViewModel.SplashPageKey, typeof(SplashActivity));
			nav.Configure(LocatorViewModel.LoginPageKey, typeof(LoginActivity));
			nav.Configure(LocatorViewModel.LoginQrPageKey, typeof(LoginQrActivity));
			nav.Configure(LocatorViewModel.RegisterPageKey, typeof(RegisterActivity));
			nav.Configure(LocatorViewModel.PasswordRetrievalPageKey, typeof(PasswordRetrievalActivity));

			nav.Configure(LocatorViewModel.HomePageKey, typeof(HomeActivity));

			nav.Configure(LocatorViewModel.HelpPageKey, typeof(HelpActivity));
			nav.Configure(LocatorViewModel.SettingsPageKey, typeof(SettingsActivity));

			nav.Configure(LocatorViewModel.TasksPageKey, typeof(Fragments.AssignmentsFragment));
			nav.Configure(LocatorViewModel.AddTaskPageKey, typeof(AddAssignmentActivity));
			nav.Configure(LocatorViewModel.DetailAssignmentPageKey, typeof(DetailAssignmentActivity));
			nav.Configure(LocatorViewModel.EditAssignmentPageKey, typeof(EditAssignmentActivity));

			nav.Configure(LocatorViewModel.MembersPageKey, typeof(MembersActivity));
			nav.Configure(LocatorViewModel.AddMembersPageKey, typeof(AddMembersActivity));

			nav.Configure(LocatorViewModel.PaymentsPageKey, typeof(Fragments.PaymentFragment));
			nav.Configure(LocatorViewModel.AddPaymentPageKey, typeof(AddPaymentActivity));
			nav.Configure(LocatorViewModel.DetailPaymentPageKey, typeof(DetailPaymentActivity));
			nav.Configure(LocatorViewModel.EditPaymentPageKey, typeof(EditPaymentActivity));
		}

		private static void RegisterIoc()
		{
			SimpleIoc.Default.Register<IDialogService, DialogService>();
			SimpleIoc.Default.Register<IPopUpService, PopUpService>();
			SimpleIoc.Default.Register<IDatabase, Database>();
			SimpleIoc.Default.Register<IMenuDialogService, MenuDialogService>();
			SimpleIoc.Default.Register<IFileSystem, FileSystem>();
		}
	}
}