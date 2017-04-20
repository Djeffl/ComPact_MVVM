using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using ComPact.Members;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid.Members
{
	[Activity(Label = "TemplateActivity")]
	public class AddMembersActivity : BaseActivity
	{
		//Local variables

		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();

		//Elements
		ImageView _backImageView;
		TextView _titleTextView;
		ImageView _optionsImageView;

		EditText _firstNameEditText;
		EditText _lastNameEditText;
		EditText _emailEditText;
		EditText _passwordEditText;
		EditText _confirmEditText;
		Button _registerButton;


		//Bind Viewmodel to activity
		AddMembersViewModel ViewModel
		{
			get
			{
				return App.Locator.AddMembersViewModel;
			}
		}

		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityAddMember);

			//Init elements
			FindViews();
			Init();

			//bindings
			SetBindings();

			//Use Commands
			SetCommands();
		}
		/**
		 * Init Views
		 */
		void FindViews()
		{
			//Header
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);
			_optionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
			//Rest
			_firstNameEditText = FindViewById<EditText>(Resource.Id.activityAddMemberFirstNameEditText);
			_lastNameEditText = FindViewById<EditText>(Resource.Id.activityAddMemberLastNameEditText);
			_emailEditText = FindViewById<EditText>(Resource.Id.activityAddMemberEmailEditText);
			_passwordEditText = FindViewById<EditText>(Resource.Id.activityAddMemberPasswordEditText);
			_confirmEditText = FindViewById<EditText>(Resource.Id.activityAddMemberConfirmPasswordEditText);

			_registerButton = FindViewById<Button>(Resource.Id.activityAddMemberRegisterButton);
		}

		void Init()
		{
			
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
		 */
		void SetCommands()
		{
			_registerButton.SetCommand("Click", ViewModel.CreateMemberCommand);
		}
		#endregion
	}
}
