using System;
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
		public const string RegisterPageKey = "RegisterPage";
		public const string PasswordRetrievalPageKey = "PasswordRetrievalPage";
		public const string HomePageKey = "HomePagekey";

		public LoginViewModel LoginViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<LoginViewModel>();
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
			//RegisterHelpers();
		}

		void RegisterViewModels() 
		{
			SimpleIoc.Default.Register<LoginViewModel>();
			SimpleIoc.Default.Register<RegisterViewModel>();
			SimpleIoc.Default.Register<PasswordRetrievalViewModel>();
			SimpleIoc.Default.Register<HomeViewModel>();
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
			//SimpleIoc.Default.Register<IUserRepository, UserRepository>();
		}
	}
}
