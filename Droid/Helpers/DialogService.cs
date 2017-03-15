using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Plugin.CurrentActivity;

namespace ComPact.Droid
{
	public class DialogService : IDialogService
	{
		//public Context Context { get; set; }
		public DialogService()
		{
		}

		//public async Task DisplayAlertAsync(string title, string message, string acceptButton, string cancelButton, Action afterHideCallback)
		//{
		//	// AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(_context)
		//	//	.SetTitle(title)
		//	//	.SetMessage(message)
		//	//	.SetPositiveButton(Android.Resource.String.Yes, (senderAlert, args) =>
		//	//	{

		//	//});
		//	if(afterHideCallback != null)

		//	{
		//		afterHideCallback();
		//	}
		//}

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
