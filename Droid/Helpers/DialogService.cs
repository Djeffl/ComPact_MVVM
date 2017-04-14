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

		public Task<bool> ShowMessage(string message, string title)
		{
			var tcs = new TaskCompletionSource<bool>();
			new Handler(Looper.MainLooper).Post(() =>
			{
				new AlertDialog.Builder(CrossCurrentActivity.Current.Activity)
							   .SetTitle(title).SetMessage(message)
							   .SetNegativeButton(Android.Resource.String.No, (sender, e) => { tcs.SetResult(false); })
							   .SetPositiveButton(Android.Resource.String.Yes, (senderAlert, args) =>
								{
									tcs.SetResult(true);
								}).Show();
			});
			return tcs.Task;
		}
		public Task<bool> ShowMessage(string message, string title, string negativeTextButton, string positiveTextButton)
		{
			var tcs = new TaskCompletionSource<bool>();
			new Handler(Looper.MainLooper).Post(() =>
			{
				new AlertDialog.Builder(CrossCurrentActivity.Current.Activity)
							   .SetTitle(title).SetMessage(message)
				               .SetNegativeButton(negativeTextButton, (sender, e) => { tcs.SetResult(false); })
				               .SetPositiveButton(positiveTextButton, (senderAlert, args) =>
								{
									tcs.SetResult(true);
								}).Show();
			});
			return tcs.Task;
		}

	}
}
