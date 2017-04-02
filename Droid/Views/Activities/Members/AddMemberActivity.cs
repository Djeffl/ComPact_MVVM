using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
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
		EditText _firstNameEditText;
		EditText _lastNameEditText;
		EditText _emailEditText;
		EditText _passwordEditText;
		EditText _confirmEditText;
		Button _registerButton;

		ImageView _backImageView;
		TextView _titleTextView;
		ImageView _optionsImageView;
		//Bind Viewmodel to activity
		//AddMembersActivity ViewModel
		//{
		//	get
		//	{
		//		return App.Locator.AddMembersViewModel;
		//	}
		//}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityAddMember);

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

		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{

		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{

		}
		#endregion
	}
}
