
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Droid
{
	[Activity(Label = "RegisterActivity")]
	public class RegisterActivity : AppCompatActivityBase
	{
		//Keep track of bindings to avoid premature garbage collection
		private readonly List<Binding> bindings = new List<Binding>();
		//Elements
		private TextView _firstNameTextView;
		private TextView _lastNameTextView;
		private TextView _emailTextView;
		private TextView _passwordTextView;
		private TextView _confirmTextView;
		private Button _registerButton;

		//Bind Viewmodel to activity
		private RegisterViewModel ViewModel
		{
			get
			{
				return App.Locator.RegisterViewModel;
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.ActivityRegister);

			//Init elements
			_firstNameTextView = FindViewById<TextView>(Resource.Id.activityRegisterFirstNameTextView);
			_lastNameTextView = FindViewById<TextView>(Resource.Id.activityRegisterLastNameTextView);
			_emailTextView = FindViewById<TextView>(Resource.Id.activityRegisterEmailTextView);
			_passwordTextView = FindViewById<TextView>(Resource.Id.activityRegisterEmailTextView);
			_confirmTextView = FindViewById<TextView>(Resource.Id.activityRegisterConfirmPasswordTextView);
			_registerButton = FindViewById<Button>(Resource.Id.activityRegisterRegisterButton);

			//Init new Registration Object
			var _newRegistration = new Registration(_firstNameTextView.Text, _lastNameTextView.Text, _emailTextView.Text, _passwordTextView.Text, _confirmTextView.Text, true);

			//bindings
			//Binding firstNameBinding = this.SetBinding(() => ViewModel.FirstName, () => _firstNameTextView, BindingMode.TwoWay);
			//Binding lastNameBinding = this.SetBinding(() => ViewModel.LastName, () => _lastNameTextView, BindingMode.TwoWay);
			//Binding emailBinding = this.SetBinding(() => ViewModel.Email, () => _emailTextView, BindingMode.TwoWay);
			//Binding passwordBinding = this.SetBinding(() => ViewModel.Password, () => _passwordTextView, BindingMode.TwoWay);
			//Binding confirmBinding = this.SetBinding(() => ViewModel.ConfirmPassword, () => _confirmTextView, BindingMode.TwoWay);

			//Binding registrationBinding = this.SetBinding(() => ViewModel.RegisterInfo, () => _newRegistration);
			//Binding registrationData = this.SetBinding(() => ViewModel.RegisterInfo, () => _newRegistration);


			//Use Commands
			//_registerButton.SetCommand("Click", ViewModel.RegisterUserAsyncCommand);
		}
		private void RegisterElements()
		{

		}
	}

}