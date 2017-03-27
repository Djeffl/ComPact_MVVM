using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using ComPact.ViewModel.Members;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid.Members
{
	[Activity(Label = "MembersActivity")]
	public class MembersActivity : BaseActivity
	{
		//Local variables

		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		ImageView _backImageView;
		ImageView _OptionsImageView;
		TextView _titleTextView;
		FloatingActionButton _addMemberFloatingActionButton;

		//Bind Viewmodel to activity
		MembersViewModel ViewModel
		{
			get
			{
				return App.Locator.MembersViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityMembers);

			//Init elements
			Init();
			_OptionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Members";

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
		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_addMemberFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityMembersAddMemberFloatingActionButton);
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			_addMemberFloatingActionButton.SetCommand("Click", ViewModel.AddMembersRedirectCommand);
		}
		#endregion
	}
}
