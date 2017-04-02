using Android.OS;
using Android.Support.V7.App;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Views;
using Android.Support.V7.Widget;
using Android.Widget;
using Android.Views;
using Android;
using Android.App;

namespace ComPact.Droid
{
	[Activity(Label = "ComPact", Icon = "@mipmap/icon", MainLauncher = true)]
	public class LoginActivity : BaseActivity
	{
		// Keep track of bindings to avoid premature garbage collection
		private readonly List<Binding> bindings = new List<Binding>();
		//Elements
		private TextView _registerRedirectTextView;
		private TextView _passwordRetrievalRedirectTextView;
		private EditText _emailInputLoginEditText;
		private EditText _passwordInputLoginEditText;
		private Button _loginButton;
		private Button _qrCodeLoginButton;

		LoginViewModel ViewModel
		{
			get
			{
				return App.Locator.LoginViewModel;
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			//Set Lay out
			SetContentView(Resource.Layout.ActivityLogin);

			//Init elements
			Init();

			//bindings
			SetBindings();

			//Use Commands
			SetCommands();
		}
		/**
		 * Init Views
		 */
		void Init()
		{
			_emailInputLoginEditText = FindViewById<EditText>(Resource.Id.activityLoginEmailInputLoginEditText);
			_passwordInputLoginEditText = FindViewById<EditText>(Resource.Id.activityLoginPasswordInputLoginEditText);
			_loginButton = FindViewById<Button>(Resource.Id.activityLoginLoginButton);
			_qrCodeLoginButton = FindViewById<Button>(Resource.Id.activityLoginQrCodeButton);

			_passwordRetrievalRedirectTextView = FindViewById<TextView>(Resource.Id.activityLoginPasswordRedirectTextView);
			_registerRedirectTextView = FindViewById<TextView>(Resource.Id.activityLoginRegisterRedirectTextView);
		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			this.SetBinding(() => ViewModel.Email, () => _emailInputLoginEditText.Text, BindingMode.TwoWay);
			this.SetBinding(() => ViewModel.Password, () => _passwordInputLoginEditText.Text, BindingMode.TwoWay);
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_passwordRetrievalRedirectTextView.SetCommand("Click", ViewModel.PasswordRetrievalRedirectCommand);
			_registerRedirectTextView.SetCommand("Click", ViewModel.RegisterRedirectCommand);
			_loginButton.SetCommand("Click", ViewModel.LoginCommand);
			_qrCodeLoginButton.SetCommand("Click", ViewModel.QrLoginRedirectCommand);
		}
	}
}

