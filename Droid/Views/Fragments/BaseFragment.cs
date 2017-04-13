using System;
using System.Globalization;
using Android.App;
//using Android.Support.V4.App;
using Android.Widget;

namespace ComPact.Droid.Fragments
{
	public class BaseFragment: Fragment
	{
		public CultureInfo CultureInfo = CultureInfo.GetCultureInfo("nl-BE");

		void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{

		}

		public BaseFragment()
		{
			//Delcare needed services here
		}
		protected virtual void HandleEvents()
		{

		}
		protected virtual void FindViews()
		{

		}
	}
}
