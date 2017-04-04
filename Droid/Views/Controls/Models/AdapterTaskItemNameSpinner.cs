using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using ComPact.Models;
using Java.Util;

namespace ComPact.Droid.Models
{
	public class AdapterTaskItemNameSpinner : AdapterBase<string>
	{

		public AdapterTaskItemNameSpinner(Context context, List<string> items) : base(context, items)
		{
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null)
			{
				row = LayoutInflater.From(_context).Inflate(Resource.Layout.ListViewItemOptions, null, false);
			}
			TextView itemNameTextView = row.FindViewById<TextView>(Resource.Id.listViewItemOptionsItemName);
			itemNameTextView.Text = Items[position];

			return row;
		}
	}



	//	: ArrayAdapter<string>
	//{

	//	string[] _items;

	//	public AdapterTaskItemNameSpinner(Context context, string[] items) : base(context, 0, items)
	//	{
	//		_items = items;
	//	}

	//	public override View GetView(int position, View convertView, ViewGroup parent)
	//	{
	//		View row = convertView;

	//		if (row == null)
	//		{
	//			row = LayoutInflater.From(Context).Inflate(Resource.Layout.ListViewItemOptions, null, false);

	//			//if (_items.Length == position)
	//			//{
	//			//	row = LayoutInflater.From(Context).Inflate(Resource.Layout.ListViewItemOptions, null, false);
	//			//}
	//			//else
	//			//{
	//			//	row = LayoutInflater.From(Context).Inflate(Resource.Layout.ListViewItemOtherOption, null, false);

	//			//}
	//		}
	//		TextView nameTextView = row.FindViewById<TextView>(Resource.Id.listViewItemOptionsItemName);
	//		nameTextView.Text = _items[1];

	//		return row;
	//	}

}
