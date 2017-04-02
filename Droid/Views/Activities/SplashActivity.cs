using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid.Activities
{
	[Activity(Label = "ComPact", Icon = "@mipmap/icon")]
	public class SplashActivity: BaseActivity
	{
		//Local variables

		//Keep track of bindings to avoid premature garbage collection
		private readonly List<Binding> bindings = new List<Binding>();
		//Elements

		//Bind Viewmodel to activity
		SplashViewModel ViewModel
		{
			get
			{
				return App.Locator.SplashViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			//OnStart(this);
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivitySplash);

			//View x;

			//x.Post(() => { });

			//Init elements
			Init();

			//bindings
			SetBindings();

			//Use Commands
			SetCommands();
		}

		protected override void OnResume()
		{
			base.OnResume();
			ViewModel.LoginCommand.Execute(null);
		}
		/**
		 * Init Views
		 */
		void Init()
		{

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
		}



		#endregion
	}
}
