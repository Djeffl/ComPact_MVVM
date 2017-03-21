using System;
using ComPact;
using ComPact.Helpers;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ComPact
{
	public class LocatorViewModel
	{
		/**
		 * Set a key for each page => App.cs
		 */
		public const string LoginPageKey = "LoginPage";
		public const string LoginQrPageKey = "LoginQrPage";
		public const string RegisterPageKey = "RegisterPage";
		public const string PasswordRetrievalPageKey = "PasswordRetrievalPage";
		public const string HomePageKey = "HomePagekey";
		public const string HelpPageKey = "HelpPageKey";
		public const string SettingsPageKey = "SettingsPageKey";

		public const string TasksPageKey = "TasksPageKey";

		public LoginViewModel LoginViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<LoginViewModel>();
			}
		}
		public LoginQrViewModel LoginQrViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<LoginQrViewModel>();
			}
		}

		public RegisterViewModel RegisterViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<RegisterViewModel>();
			}
		}

		public PasswordRetrievalViewModel PasswordRetrievalViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<PasswordRetrievalViewModel>();
			}
		}

		public HomeViewModel HomeViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<HomeViewModel>();
			}
		}
		public HelpViewModel HelpViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<HelpViewModel>();
			}
		}
		public SettingsViewModel SettingsViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<SettingsViewModel>();
			}
		}
		public TasksViewModel TasksViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<TasksViewModel>();
			}
		}

		/**
		 * Register every ViewModel to the IOC container
		 */
		public LocatorViewModel()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			RegisterViewModels();
			RegisterRepositories();
			RegisterWebServices();
			RegisterServices();
		}

		void RegisterViewModels() 
		{
			SimpleIoc.Default.Register<LoginViewModel>();
			SimpleIoc.Default.Register<LoginQrViewModel>();
			SimpleIoc.Default.Register<RegisterViewModel>();
			SimpleIoc.Default.Register<PasswordRetrievalViewModel>();
			SimpleIoc.Default.Register<HomeViewModel>();
			SimpleIoc.Default.Register<HelpViewModel>();
			SimpleIoc.Default.Register<SettingsViewModel>();
			SimpleIoc.Default.Register<TasksViewModel>();
		}

		void RegisterServices()
		{
			SimpleIoc.Default.Register<IUserDataService, UserDataService>();
		}
		void RegisterWebServices()
		{
			SimpleIoc.Default.Register<IUserWebservice, UserWebservice>();
		}
		void RegisterRepositories()
		{
			SimpleIoc.Default.Register<IUserRepository, UserRepository>();
		}
	}
}
