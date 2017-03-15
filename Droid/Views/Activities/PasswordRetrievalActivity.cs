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
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Droid
{
	[Activity(Label = "PasswordRetrievalActivity")]
	public class PasswordRetrievalActivity : BaseActivity
	{
		//Keep track of bindings to avoid premature garbage collection
		private readonly List<Binding> bindings = new List<Binding>();
		//Elements
		//private TextView _detailTextView;

		//Bind Viewmodel to activity
		private PasswordRetrievalViewModel ViewModel
		{
			get
			{
				return App.Locator.PasswordRetrievalViewModel;
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.ActivityPasswordRetrieval);


		}
	}
}
