using System;
using System.Threading.Tasks;

using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Plugin.CurrentActivity;

namespace ComPact.Droid
{
	public class DialogService : IDialogService
	{
		//public Context Context { get; set; }
		public DialogService()
		{
		}

		public void ShowMessage(string message)
		{
			new Handler(Looper.MainLooper).Post(() =>
			{
				new AlertDialog.Builder(CrossCurrentActivity.Current.Activity)
					.SetTitle("Error")
					.SetMessage(message)
					.SetPositiveButton(Android.Resource.String.Yes, (senderAlert, args) =>
					{

					}).Show();
			});
		}
	}
}
