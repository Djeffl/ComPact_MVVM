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

namespace ComPact.Droid
{
	[Activity(Label = "AddPaymentActivity")]
	public class AddPaymentActivity : BaseActivity
	{
		//Local variables

		//string _description;
		//public string Description
		//{
		//	get
		//	{
		//		return _description;
		//		//return ViewModel.Payment.Description;
		//	}
		//	set
		//	{
		//		_description = value;
		//		ViewModel.Payment.Description = _description;
		//		//ViewModel.TransferPaymentCommand.Execute(new Payment { Description = _description });
		//	}
		//}


		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		//__________________________Header______________________________
		ImageView _backImageView;
		TextView _titleTextView;
		ImageView _optionsImageView;
		//__________________________Others______________________________
		EditText _whatEditText;
		EditText _priceEditText;
		EditText _detailsEditText;
		FloatingActionButton _createFloatingActionButton;
		//Bind Viewmodel to activity
		AddPaymentViewModel ViewModel
		{
			get
			{
				return App.Locator.AddPaymentViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityAddPayment);



			//Init elements
			FindViews();
			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Add Payment";
			//bindings
			SetBindings();

			//Use Commands
			SetCommands();

			//init
			Init();
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
			_whatEditText = FindViewById<EditText>(Resource.Id.activityAddPaymentWhatEditText);
			_priceEditText = FindViewById<EditText>(Resource.Id.activityAddPaymentPriceEditText);
			_detailsEditText = FindViewById<EditText>(Resource.Id.activityAddPaymentDetailsEditText);
			_createFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityAddPaymentFloatingActionButton);
		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.Payment.Name, () => _whatEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Payment.Price, () => _priceEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Payment.Description, () => _detailsEditText.Text, BindingMode.TwoWay));
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			_createFloatingActionButton.SetCommand("Click", ViewModel.CreatePaymentCommand);
		}
		void Init()
		{
			_priceEditText.Text = null;
		}
		#endregion
	}
}
