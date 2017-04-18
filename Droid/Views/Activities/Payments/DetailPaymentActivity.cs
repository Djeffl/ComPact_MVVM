
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
	public class DetailPaymentActivity : BaseActivity
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
		//__________________________Header______________________________
		ImageView _backImageView;
		TextView _titleTextView;
		ImageView _optionsImageView;
		//__________________________Others______________________________

		FloatingActionButton _editFloatingActionButton;
		TextView _detailTextView;
		TextView _paymentTitleTextView;
		TextView _priceTextView;
		ImageView _deletePaymentImageView;
		//Bind Viewmodel to activity
		DetailPaymentViewModel ViewModel
		{
			get
			{
				return App.Locator.DetailPaymentViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityDetailPayment);

			//Init elements
			FindViews();
			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Detail Payment";
			//init
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
			//__________________________Header______________________________
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);
			_optionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
			//__________________________Others______________________________
			_editFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityDetailPaymentEditPaymentFloatingActionButton);
			_titleTextView = FindViewById<TextView>(Resource.Id.activityDetailPaymentTitleTextView);
			_priceTextView = FindViewById<TextView>(Resource.Id.activityDetailPaymentPriceTextView);
			_detailTextView = FindViewById<TextView>(Resource.Id.activityDetailPaymentDetailTextView);
			_deletePaymentImageView = FindViewById<ImageView>(Resource.Id.activityDetailPaymentDeletePaymentImageView);
		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => Payment,() => ViewModel.Payment, BindingMode.TwoWay));
			//bindings.Add(this.SetBinding(() => ViewModel.Payment, () => Payment, BindingMode.TwoWay));
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			_deletePaymentImageView.SetCommand("Click", ViewModel.DeletePaymentCommand);
			_editFloatingActionButton.SetCommand("Click", ViewModel.EditPaymentRedirectCommand);
		}
		void Init()
		{
			Payment = Nav.GetAndRemoveParameter<Payment>(Intent);
			_titleTextView.Text = Payment.Name;
			_detailTextView.Text = Payment.Description;
			_priceTextView.Text = String.Format(CultureInfo, "{0:C}",Payment.Price);
		}
		#endregion
	}
}
