
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
using ZXing.Mobile;

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
			Scan();
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


		async void Scan()
		{
			try
			{
				string[] separators = { "..." };
				var scanner = new MobileBarcodeScanner(this);
				var result = await scanner.Scan();

				//Console.WriteLine(result.Text);
				string value = result.Text;
				//0 email  1 password
				string[] emailAndPassword = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
				string _emailstr = emailAndPassword[0];
				string _passStr = emailAndPassword[1];

				ViewModel.ScanningFinishedCommand?.Execute(new User
				{
					Email = _emailstr,
					Password = _passStr
					
				});

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
		#endregion
	}
}
