
using Android.Views;
using Android.OS;
using Android.Support.V7.App;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Views;
using Android.Support.V7.Widget;
using Android.Widget;
using Android.Views;
using Android;
using Android.App;

namespace ComPact.Droid
{
	[Activity(Label = "ComPact", MainLauncher = true, Icon = "@mipmap/icon")]
	public class LoginActivity : AppCompatActivityBase
	{
		// Keep track of bindings to avoid premature garbage collection
		private readonly List<Binding> bindings = new List<Binding>();

		private TextView _registerRedirectTextView;
		private TextView _passwordRetrievalRedirectTextView;

		private LoginViewModel ViewModel
		{
			get
			{
				return App.Locator.LoginViewModel;
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.ActivityLogin);
			var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			toolbar.SetTitleTextColor(Resources.GetColor(Resource.Color.white_color));
			SetSupportActionBar(toolbar);

			//Declare elements
			//_nextButton = FindViewById<Button>(Resource.Id.ActivityMainButtonNext);
			//_welcomeTextView = FindViewById<TextView>(Resource.Id.ActivityMainTextViewWelcome);
			//Bind element from activity to a viewmodel
			//bindings.Add(this.SetBinding(() => ViewModel.WelcomeText, () => _welcomeTextView.Text));
			//bindings.Add(this.SetBinding(() => ViewModel.NextText, () => _nextButton.Text));

			_passwordRetrievalRedirectTextView = FindViewById<TextView>(Resource.Id.activityLoginPasswordRedirectTextView);
			_registerRedirectTextView = FindViewById<TextView>(Resource.Id.activityLoginRegisterRedirectTextView);

			_passwordRetrievalRedirectTextView.SetCommand("Click", ViewModel.PasswordRetrievalRedirectCommand);
			_registerRedirectTextView.SetCommand("Click", ViewModel.RegisterRedirectCommand);
		}
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.header,  menu);
			return base.OnCreateOptionsMenu(menu);
		}
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
			return base.OnOptionsItemSelected(item);
		}

		//public const string Page2Key = "Page2";
		//public const string Page3Key = "Page3";

		//private static bool _initialized;

		//protected override void OnCreate(Bundle bundle)
		//{
		//	base.OnCreate(bundle);
		//	SetContentView(Resource.Layout.Main);

		//	if (!_initialized)
		//	{
		//		_initialized = true;
		//		ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

		//		var nav = new NavigationService();
		//		nav.Configure(Page2Key, typeof(Page2Activity));
		//		nav.Configure(Page3Key, typeof(Page3Activity));

		//		SimpleIoc.Default.Register<INavigationService>(() => nav);
		//	}
		//	var button = FindViewById<Button>(Resource.Id.MyButton);
		//	button.Click += (s, e) =>
		//	{
		//		var nav = ServiceLocator.Current.GetInstance<INavigationService>();
		//		nav.NavigateTo(Page2Key);
		//	};
		//}
	}
}

