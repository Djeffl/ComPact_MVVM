using System;
using Android.Widget;
using ComPact.Helpers;
using Plugin.CurrentActivity;

namespace ComPact.Droid.Helpers
{
	public class PopUpService: IPopUpService
	{
		ToastLength toastLength;
		public void Show(string message, string length)
		{
			if (length == "long")
			{
				toastLength = ToastLength.Long;
			}
			else {
				toastLength = ToastLength.Short;
			}
			Toast.MakeText(CrossCurrentActivity.Current.Activity, message, toastLength).Show();
		}
	}
}
