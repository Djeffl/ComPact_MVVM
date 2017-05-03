using System;
using Android.Widget;
using ComPact.Helpers;
using Plugin.CurrentActivity;

namespace ComPact.Droid.Helpers
{
	public class PopUpService: IPopUpService
	{
		ToastLength toastLength;
		public void Show(string message, PopUpLength length)
		{
			if (length == PopUpLength.Long)
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
