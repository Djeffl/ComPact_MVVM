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
	[Activity]
	public class RegisterActivity : BaseActivity
	{
		//Local variables
		//bool conTouched = false;
		//Color red = new Color(250, 54, 50);
		//Color green = new Color(50, 205, 50);

		//Keep track of bindings to avoid premature garbage collection
		private readonly List<Binding> bindings = new List<Binding>();
		//Elements
		private EditText _firstNameEditText;
		private EditText _lastNameEditText;
		private EditText _emailEditText;
		private EditText _passwordEditText;
		private EditText _confirmEditText;
		private Button _registerButton;

		private ImageView _backImageView;
		private TextView _titleTextView;
		private ImageView _optionsImageView;

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
			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Register";
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
			_firstNameEditText = FindViewById<EditText>(Resource.Id.activityRegisterFirstNameTextView);
			_lastNameEditText = FindViewById<EditText>(Resource.Id.activityRegisterLastNameTextView);
			_emailEditText = FindViewById<EditText>(Resource.Id.activityRegisterEmailTextView);
			_passwordEditText = FindViewById<EditText>(Resource.Id.activityRegisterPasswordTextView);
			_confirmEditText = FindViewById<EditText>(Resource.Id.activityRegisterConfirmPasswordTextView);
			_registerButton = FindViewById<Button>(Resource.Id.activityRegisterRegisterButton);
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_optionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);
			Typeface face = Typeface.CreateFromAsset(this.Assets,"RobotoTTF/Roboto-Light.ttf");
			_confirmEditText.Typeface = face;
			_passwordEditText.Typeface = face;
			_emailEditText.Typeface = face;
			_lastNameEditText.Typeface = face;
			_firstNameEditText.Typeface = face;
		}
		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			this.SetBinding(() => ViewModel.Registration.FirstName, () => _firstNameEditText.Text, BindingMode.TwoWay);
			this.SetBinding(() => ViewModel.Registration.LastName, () => _lastNameEditText.Text, BindingMode.TwoWay);
			this.SetBinding(() => ViewModel.Registration.Email, () => _emailEditText.Text, BindingMode.TwoWay);
			this.SetBinding(() => ViewModel.Registration.Password, () => _passwordEditText.Text, BindingMode.TwoWay);
			this.SetBinding(() => ViewModel.Registration.ConfirmPassword, () => _confirmEditText.Text, BindingMode.TwoWay);
		}
		/**
		 * Register the commands from the ViewModel to the View
		 * 
		 */
		void SetCommands()
		{
			_registerButton.SetCommand("Click", ViewModel.RegisterUserCommand);
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);

		}
	}

}