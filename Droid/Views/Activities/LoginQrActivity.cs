
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
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid
{
	[Activity(Label = "LoginQrActivity")]
	public class LoginQrActivity: BaseActivity
	{
		//Local variables

		//Keep track of bindings to avoid premature garbage collection
		private readonly List<Binding> bindings = new List<Binding>();
		//Elements
		private ImageView _backImageView;
		private TextView _titleTextView;
		private ImageView _optionsImageView;
		string _emailstr;
		string _passStr;
		//Bind Viewmodel to activity
		LoginQrViewModel ViewModel
		{
			get
			{
				return App.Locator.LoginQrViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityLoginQr);

			//Init elements
			Init();
			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Scanner";
			//bindings
			SetBindings();

			//Use Commands
			SetCommands();
			scanner();
		}
		/**
		 * Init Views
		 */
		void Init()
		{
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);
			_optionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			//this.SetBinding(() => ViewModel.Email, () => _emailstr, BindingMode.TwoWay);
			//this.SetBinding(() => ViewModel.Password, () => _password.Text, BindingMode.TwoWay);

		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
		}


		async void scanner()
		{
			try
			{
				string[] separators = { "..." };
				var scanner = new ZXing.Mobile.MobileBarcodeScanner(this);
				var result = await scanner.Scan();

				//Console.WriteLine(result.Text);
				string value = result.Text;
				//0 email  1 password
				string[] emailAndPassword = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
				_emailstr = emailAndPassword[0];
				_passStr = emailAndPassword[1];
				ViewModel.Email = _emailstr;
				ViewModel.Password = _passStr;
				Toast.MakeText(this, ViewModel.Email + " " + ViewModel.Password, ToastLength.Long).Show();
			}
			catch (Exception ex)

			{
				Toast.MakeText(this, "Scanner cancelled!", ToastLength.Long).Show();
			}
			finally
			{
				Console.WriteLine("okey we going to start");
				await ViewModel.LoginUserAsync();
			}
		}
		async void scan()
		{
			try
			{
				ZXing.Mobile.MobileBarcodeScanner scanner = new ZXing.Mobile.MobileBarcodeScanner(this);
				scanner.FlashButtonText = "Flash";
				scanner.TopText = "";
				scanner.BottomText = "";
				var result = await scanner.Scan();

				Toast.MakeText(this, result.Text, ToastLength.Long).Show();

			}
			catch (Exception ex)

			{
				Toast.MakeText(this, "Scanner cancelled!", ToastLength.Long).Show();
			}
			finally
			{
			}
		}
		#endregion
	}
}
