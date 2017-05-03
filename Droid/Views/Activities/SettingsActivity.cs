
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using ZXing;

namespace ComPact.Droid.Activities
{
	[Activity(Label = "SettingsActivity")]
	public class SettingsActivity : BaseActivity
	{
		//Local variables

		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		ImageView _backImageView;
		ImageView _OptionsImageView;
		TextView _titleTextView;
		ImageView _qrCodeViewImageView;
		Button _logOutButton;
		Button _membersRedirectButton;

		//Bind Viewmodel to activity
		SettingsViewModel ViewModel
		{
			get
			{
				return App.Locator.SettingsViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			//Set Lay out
			SetContentView(Resource.Layout.ActivitySettings);

			//Init elements
			Init();
			_OptionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Settings";
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
			_qrCodeViewImageView = FindViewById<ImageView>(Resource.Id.qrCodeViewImageView);
			_qrCodeViewImageView.SetImageBitmap(GetQRCode());
			_logOutButton = FindViewById<Button>(Resource.Id.activitySettingsLogOutButton);
			_membersRedirectButton = FindViewById<Button>(Resource.Id.activitySettingsMembersRedirectButton);
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
			_logOutButton.SetCommand("Click", ViewModel.LogOutCommand);
			_membersRedirectButton.SetCommand("Click", ViewModel.MembersRedirectCommand);
		}
		#endregion
		Bitmap GetQRCode()
		{
			var writer = new BarcodeWriter
			{
				Format = BarcodeFormat.QR_CODE,
				Options = new ZXing.Common.EncodingOptions
				{
					Height = 200,
					Width = 200
				}
			};
			string text = "jeffliekens7@hotmail.com...test";
			return writer.Write(text);
		}
	}
}
