using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using ComPact.Models;
using ComPact.Payments;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid.Fragments
{
	public class PaymentFragments : BaseFragment
	{
		PaymentsViewModel ViewModel
		{
			get
			{
				return App.Locator.PaymentsViewModel;
			}
		}
		ObservableCollection<Payment> _payments = new ObservableCollection<Payment>();
		public ObservableCollection<Payment> Payments
		{
			get
			{
				return _payments;
			}
			set
			{
				_payments = value;
				//FILL list
				//_adapter = new PaymentsAdapter(this);
				//_recyclerView.SetAdapter(_adapter);
			}
		}

		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		FloatingActionButton _addPaymentFloatingActionButton;
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			FindViews();

			HandleEvents();

			//Data & services
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			return inflater.Inflate(Resource.Layout.FragmentPayments, container, false);
		}

		protected override void FindViews()
		{
			_addPaymentFloatingActionButton = View.FindViewById<FloatingActionButton>(Resource.Id.activityPaymentsAddPaymentFloatingActionButton);
			//_recyclerView = View.FindViewById<RecyclerView>(Resource.Id.fragmentPaymentsRecyclerView);
		}
		protected override void HandleEvents()
		{
			//Fill Payments
			ViewModel.LoadDataCommand?.Execute(null);
		}
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.Payments, () => Payments, BindingMode.OneWay));
		}
		void SetCommands()
		{
			_addPaymentFloatingActionButton.SetCommand("Click", ViewModel.AddPaymentRedirectCommand);
		}
	}
}
