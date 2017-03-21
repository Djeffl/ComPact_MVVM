
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

namespace ComPact.Droid.Activities
{
	[Activity(Label = "HelpActivity")]
	public class HelpActivity : BaseActivity
	{
		//Local variables

		//Keep track of bindings to avoid premature garbage collection
		private readonly List<Binding> bindings = new List<Binding>();
		//Elements
		private ImageView _backImageView;
		private ImageView _OptionsImageView;
		private TextView _titleTextView;

		//Bind Viewmodel to activity
		HelpViewModel ViewModel
		{
			get
			{
				return App.Locator.HelpViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			//Set Lay out
			SetContentView(Resource.Layout.ActivityHelp);
			//Init elements
			Init();
			_OptionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Help";
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

		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
		}
		#endregion
	}
}
