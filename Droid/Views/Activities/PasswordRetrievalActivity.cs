using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Droid
{
	[Activity(Label = "PasswordRetrievalActivity")]
	public class PasswordRetrievalActivity : BaseActivity
	{
		//Local variables

		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		ImageView _backImageView;
		ImageView _OptionsImageView;
		TextView _titleTextView;
		EditText _emailEditText;
		Button _confirmButton;


		//Bind Viewmodel to activity
		PasswordRetrievalViewModel ViewModel
		{
			get
			{
				return App.Locator.PasswordRetrievalViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			//Set Lay out
			SetContentView(Resource.Layout.ActivityPasswordRetrieval);
			//Init elements
			Init();
			_OptionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Reset password";
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
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_OptionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);
			_emailEditText = FindViewById<EditText>(Resource.Id.ActivityPasswordRetrievalEmailEditText);
			_confirmButton = FindViewById<Button>(Resource.Id.ActivityPasswordRetrievalConfirmButton);

		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			this.SetBinding(() => ViewModel.Email, () => _emailEditText.Text, BindingMode.TwoWay);
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			_confirmButton.SetCommand("Click", ViewModel.PasswordResetCommand);
		}
		#endregion
	}
}
