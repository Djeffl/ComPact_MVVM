using System;
using System.Globalization;
using GalaSoft.MvvmLight.Views;

namespace ComPact.Droid
{
	public class BaseActivity : ActivityBase
	{
		public CultureInfo CultureInfo { get; set; }


		public BaseActivity()
		{
			CultureInfo = CultureInfo.GetCultureInfo("nl-BE");
		}
	}
}
