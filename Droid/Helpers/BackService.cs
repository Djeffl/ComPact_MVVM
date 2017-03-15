using System;
using ComPact.Helpers;
using Plugin.CurrentActivity;

namespace ComPact.Droid.Helpers
{
	public class BackService: IBackService
	{
		public void GoBack()
		{
			CrossCurrentActivity.Current.Activity.OnBackPressed();
		}
	}
}
