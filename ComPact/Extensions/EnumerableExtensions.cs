using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ComPact.Extensions
{
	public static class EnumerableExtensions
	{
		//If you're working with non-generic IEnumerable you can do it this way:
		public static ObservableCollection<object> Convert(this IEnumerable original)
		{
			return new ObservableCollection<object>(original.Cast<object>());
		}

		//If you're working with generic IEnumerable<T> you can do it this way:
		public static ObservableCollection<T> Convert<T>(this IEnumerable<T> original)
		{
			return new ObservableCollection<T>(original);
		}

		//If you're working with non-generic IEnumerable but know the type of elements, you can do it this way:
		public static ObservableCollection<T> Convert<T>(this IEnumerable original)
		{
			return new ObservableCollection<T>(original.Cast<T>());
		}
	}
}
