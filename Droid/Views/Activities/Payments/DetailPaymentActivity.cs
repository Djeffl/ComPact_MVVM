
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using ComPact.Models;
using ComPact.Payments;
using ComPact.WebServices.Data;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Java.IO;
using Java.Net;
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
		ImageView _picturePayment;
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

			//Init
			Init();

			//bindings
			SetBindings();

			//Use Commands
			SetCommands();


		}
		protected override void OnResume()
		{
			base.OnResume();

			ViewModel.LoadDataCommand.Execute(null);


			try
			{
				File image = new File(ViewModel.Payment.Image.Path);
				if (image.Exists())
				{
					Bitmap bitmap = BitmapFactory.DecodeFile(image.AbsolutePath);
					_picturePayment.SetImageBitmap(bitmap);
				}
				else
				{
					//string uri = "file:///Users/jeff/stage/Stage/uploads/payments/payments_0f76c96a-edd3-4b00-b1ca-fa2cc3e8d9c7-1508039419.jpg";// + ViewModel.Payment.Image.Path;// payments_0f76c96a-edd3-4b00-b1ca-fa2cc3e8d9c7-1508039419.jpg;
					//Bitmap bitmap = BitmapFactory.DecodeFile(uri);
					//_picturePayment.SetImageBitmap(bitmap);
					Bitmap imageBitma = null;
					using (var webClient = new WebClient())
					{
string url = "http://192.168.56.1:8080" + ApiCalls.ImageFilePaymentPath +  ViewModel.Payment.Image.Path;
						var imageBytes = webClient.DownloadData(url);
						if (imageBytes != null && imageBytes.Length > 0)
						{
							imageBitma = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
						}
					}
					_picturePayment.SetImageBitmap(imageBitma);
				}
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex);
			}

			_titleTextView.Text = Payment.Name;
			_detailTextView.Text = Payment.Description;
			_priceTextView.Text = String.Format(CultureInfo, "{0:C}", Payment.Price);
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
			_picturePayment = FindViewById<ImageView>(Resource.Id.activityDetailPaymentPictureImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.activityDetailPaymentTitleTextView);
			_priceTextView = FindViewById<TextView>(Resource.Id.activityDetailPaymentPriceTextView);
			_detailTextView = FindViewById<TextView>(Resource.Id.activityDetailPaymentDetailTextView);
			_deletePaymentImageView = FindViewById<ImageView>(Resource.Id.activityDetailPaymentDeletePaymentImageView);
		}

		void Init()
		{
			//Get Payment from previous screen & pass to viewmodel
			Payment payment = Nav.GetAndRemoveParameter<Payment>(Intent);
			ViewModel.SetPaymentCommand.Execute(payment);

			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Detail Payment";
		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.Payment,() => Payment, BindingMode.OneWay));
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
		#endregion
	}
}
