
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Droid
{
	[Activity(Label = "RegisterActivity")]
	public class RegisterActivity : BaseActivity
	{
		//Local variables
		//bool conTouched = false;
		//Color red = new Color(250, 54, 50);
		//Color green = new Color(50, 205, 50);

		//Keep track of bindings to avoid premature garbage collection
		private readonly List<Binding> bindings = new List<Binding>();
		//Elements
		private TextView _firstNameTextView;
		private TextView _lastNameTextView;
		private TextView _emailTextView;
		private TextView _passwordTextView;
		private TextView _confirmTextView;
		private Button _registerButton;
		private Toolbar _toolbar;
		private ImageView _backImageView;
		private ImageView _OptionsImageView;

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

			//Set Lay out
			SetContentView(Resource.Layout.ActivityRegister);

			//Init elements
			Init();
			_OptionsImageView.Visibility = ViewStates.Gone;

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
			_firstNameTextView = FindViewById<TextView>(Resource.Id.activityRegisterFirstNameTextView);
			_lastNameTextView = FindViewById<TextView>(Resource.Id.activityRegisterLastNameTextView);
			_emailTextView = FindViewById<TextView>(Resource.Id.activityRegisterEmailTextView);
			_passwordTextView = FindViewById<TextView>(Resource.Id.activityRegisterPasswordTextView);
			_confirmTextView = FindViewById<TextView>(Resource.Id.activityRegisterConfirmPasswordTextView);
			_registerButton = FindViewById<Button>(Resource.Id.activityRegisterRegisterButton);
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_OptionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
		}
		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			this.SetBinding(() => ViewModel.FirstName, () => _firstNameTextView.Text, BindingMode.TwoWay);
			this.SetBinding(() => ViewModel.LastName, () => _lastNameTextView.Text, BindingMode.TwoWay);
			this.SetBinding(() => ViewModel.Email, () => _emailTextView.Text, BindingMode.TwoWay);
			this.SetBinding(() => ViewModel.Password, () => _passwordTextView.Text, BindingMode.TwoWay);
			this.SetBinding(() => ViewModel.ConfirmPassword, () => _confirmTextView.Text, BindingMode.TwoWay);
		}
		/**
		 * Register the commands from the ViewModel to the View
		 * 
		 */
		void SetCommands()
		{
			//_registerButton.SetCommand<Registration>("Click", ViewModel.RegisterUserAsyncCommand, GetRegistrationObject());
			_registerButton.SetCommand("Click", ViewModel.RegisterUserAsyncCommand);
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);


			//ViewModel.RegisterUserAsyncCommand3.Execute(GetRegistrationObject());

		}
		/**
		 * Get the value of the TextViews
		 */
		//Registration GetRegistrationObject()
		//{
		//	return new Registration(_firstNameTextView.Text, _lastNameTextView.Text, _emailTextView.Text, _passwordTextView.Text, _confirmTextView.Text, true);
		//}
	}

}