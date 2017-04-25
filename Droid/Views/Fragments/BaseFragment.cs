using System;
using System.Globalization;
using Android.App;
//using Android.Support.V4.App;
using Android.Widget;

namespace ComPact.Droid.Fragments
{
	public class BaseFragment : Fragment
	{
		public CultureInfo CultureInfo {get;set;}

		void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{

		}

		public BaseFragment()
		{
			//Delcare needed services here
			CultureInfo = CultureInfo.GetCultureInfo("nl-BE");
		}
		protected virtual void HandleEvents()
		{

		}
		protected virtual void FindViews()
		{

		}
	}
}
