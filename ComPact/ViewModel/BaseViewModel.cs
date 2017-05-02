using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ComPact.Models;
using GalaSoft.MvvmLight;

namespace ComPact.ViewModel
{
	public class BaseViewModel : ViewModelBase
	{
		public IUserDataService UserDataService;

		public BaseViewModel(IUserDataService userDataService)
		{
			UserDataService = userDataService;
		}

		public async Task<User> GetUser()
		{
			User user;
			try
			{

				user = await UserDataService.GetUser();
			}
			catch (Exception)
			{
				user = null;
			}
			return user;
		}
		public static string FirstCharToUpper(string input)
		{
			string first = input[0].ToString();
			return first.ToUpper() + input.Substring(1);
		}
		public ObservableCollection<object> Convert(IEnumerable original)
		{
			return new ObservableCollection<object>(original.Cast<object>());
		}

		//If you're working with generic IEnumerable<T> you can do it this way:
		public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
		{
			return new ObservableCollection<T>(original);
		}

		//If you're working with non-generic IEnumerable but know the type of elements, you can do it this way:
		public ObservableCollection<T> Convert<T>(IEnumerable original)
		{
			return new ObservableCollection<T>(original.Cast<T>());
		}


	}
}
