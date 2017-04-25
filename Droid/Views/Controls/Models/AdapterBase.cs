using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace ComPact.Droid.Models
{
	public abstract class AdapterBase<T>: BaseAdapter<T>
	{
		public List<T> Items;
		protected Context _context;

		public AdapterBase(Context context, List<T> items)
		{
			Items = items;
			_context = context;

		}

		public override T this[int position]
		{
			get
			{
				return Items[position];
			}
		}

		public override int Count
		{
			get
			{
				return Items.Count;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public abstract override View GetView(int position, View convertView, ViewGroup parent);
	}
}
