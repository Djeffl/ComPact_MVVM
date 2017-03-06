using System;
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

		public LoggedInViewModel LoggedInViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<LoggedInViewModel>();
			}
		}

		/**
		 * Register every ViewModel to the IOC container
		 */
		public LocatorViewModel()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			SimpleIoc.Default.Register<LoginViewModel>();
			SimpleIoc.Default.Register<RegisterViewModel>();
			SimpleIoc.Default.Register<PasswordRetrievalViewModel>();
			SimpleIoc.Default.Register<LoggedInViewModel>();
		}
	}
}
