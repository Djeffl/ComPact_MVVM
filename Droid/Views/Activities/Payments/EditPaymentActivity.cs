
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using ComPact.Models;
using ComPact.Payments;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace ComPact.Droid.Payments
{
	[Activity]
	public class EditPaymentActivity : BaseActivity
	{
		public NavigationService Nav
		{
			get
			{
				return (NavigationService)ServiceLocator.Current
					.GetInstance<INavigationService>();
			}
		}

		Payment _payment;
		public Payment Payment
		{
			get
			{
				return _payment;
			}
			set
			{
				_payment = value;
			}
		}

		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		ImageView _backImageView;
		TextView _titleTextView;
		ImageView _optionsImageView;
		FloatingActionButton _SavePaymentFloatingActionButton;

		//Bind Viewmodel to activity
		EditPaymentViewModel ViewModel
		{
			get
			{
				return App.Locator.EditPaymentViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityAddPayment);

			Payment = Nav.GetAndRemoveParameter<Payment>(Intent);

			//Find views
			FindViews();

			Init();
			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Edit Payment";
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
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_optionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);

			_SavePaymentFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityAddPaymentFloatingActionButton);

		}

		void Init()
		{
		}


		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => Payment, () => Payment));
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			_SavePaymentFloatingActionButton.SetCommand("Click", ViewModel.UpdatePaymentCommand);
		}
		#endregion
	}
}
