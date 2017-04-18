using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
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
				//Set adapter after payments are loaded
				//_paymentsListView.Adapter = ViewModel.Payments.GetAdapter(GetPaymentsAdapter);
			}
		}
		ObservableCollection<Member> _members = new ObservableCollection<Member>();
		public ObservableCollection<Member> Members
		{
			get
			{
				return _members;
			}
			set
			{
				_members = value;
				SetListViewAdapter();
			}
		}
		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		FloatingActionButton _addPaymentFloatingActionButton;
		ListView _paymentsListView;
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			FindViews();


			SetBindings();

			SetCommands();
		}
		public override void OnResume()
		{
			base.OnResume();
            HandleEvents();

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			return inflater.Inflate(Resource.Layout.FragmentPayments, container, false);
		}

		protected override void FindViews()
		{
			_addPaymentFloatingActionButton = View.FindViewById<FloatingActionButton>(Resource.Id.activityPaymentsAddPaymentFloatingActionButton);
			_paymentsListView = View.FindViewById<ListView>(Resource.Id.fragmentPaymentsPaymentsListView);
		}
		protected override void HandleEvents()
		{
			//Fill Payments
			ViewModel.LoadDataCommand?.Execute(null);
		}
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.Payments, () => Payments, BindingMode.OneWay));
			bindings.Add(this.SetBinding(() => ViewModel.Members, () => Members, BindingMode.OneWay));
		}
		void SetCommands()
		{
			_addPaymentFloatingActionButton.SetCommand("Click", ViewModel.AddPaymentRedirectCommand);
		}
		View GetPaymentsAdapter(int position, Payment payment, View convertView)
		{
			// Not reusing views here
			LayoutInflater inflater = LayoutInflater.From(Application.Context);
			convertView = inflater.Inflate(Resource.Layout.PaymentItemView, null);
			ImageView iconImageView = convertView.FindViewById<ImageView>(Resource.Id.paymentItemViewImageView);
			TextView nameTextView = convertView.FindViewById<TextView>(Resource.Id.paymentsItemViewTitleTextView);
			TextView timeTextView = convertView.FindViewById<TextView>(Resource.Id.paymentItemViewTimeTextView);
			TextView dateTextView = convertView.FindViewById<TextView>(Resource.Id.paymentItemViewDateTextView);
			TextView priceTextView = convertView.FindViewById<TextView>(Resource.Id.paymentItemViewPriceTextView);

			nameTextView.Text = payment.Name;
			priceTextView.Text = String.Format(CultureInfo, "{0:C}",payment.Price);
			timeTextView.Text = payment.CreatedAt.TimeOfDay.ToString("c").Remove(5);
			dateTextView.Text = payment.CreatedAt.ToShortDateString();

			iconImageView.SetColorFilter(Color.Rgb(224, 71, 74));
			convertView.SetCommand("Click", ViewModel.DetailPaymentRedirectCommand, payment);

			return convertView;
		}

		void SetListViewAdapter()
		{
			_paymentsListView.Adapter = ViewModel.Payments.GetAdapter(GetPaymentsAdapter);
		}
	}
}
