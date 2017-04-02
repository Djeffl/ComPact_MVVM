// Helpers/Settings.cs
using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ComPact.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string LoginTokenKey = "Logintoken_key";
		private static readonly string LoginTokenDefault = string.Empty;

		private const string EmailKey = "email_key";
		private static readonly string EmailDefault = string.Empty;

		private const string FirstNameKey = "firstName_key";
		private static readonly string FirstNameDefault = string.Empty;

		private const string LastNameKey = "lastName_key";
		private static readonly string LastNameDefault = string.Empty;
		#endregion

		public static string LoginToken
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(LoginTokenKey, LoginTokenDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(LoginTokenKey, value);
			}
		}
		public static string GetLoginTokenDefault()
		{
			return LoginTokenDefault;
		}
		public static string Email
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(EmailKey, EmailDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(EmailKey, value);
			}
		}
		public static string FirstName
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(FirstNameKey, FirstNameDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(FirstNameKey, value);
			}
		}
		public static string LastName
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(LastNameKey, LastNameDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(LastNameKey, value);
			}
		}
		public static void ClearAllExceptToken()
		{
			Email = EmailDefault;
			FirstName = FirstNameDefault;
			LastName = LastNameDefault;
		}
		public static void ClearAll()
		{
			ClearAllExceptToken();
			LoginToken = LoginTokenDefault;
		}

	}
}