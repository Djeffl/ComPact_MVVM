using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
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
				if (Items.Count != position)
				{
					row = LayoutInflater.From(_context).Inflate(Resource.Layout.ListViewItemOptions, null, false);
				}
				else
				{
					row = LayoutInflater.From(_context).Inflate(Resource.Layout.ListViewItemOtherOption, null, false);
					Button btn = row.FindViewById<Button>(Resource.Id.listViewItemOtherOptionOtherButton);
					btn.Text = "Other";


				}
			}
			if (Items.Count != position-1)
			{
				TextView nameTextView = row.FindViewById<TextView>(Resource.Id.listViewItemOptionsItemName);
				nameTextView.Text = Items[position];
			}
			else
			{
				row = LayoutInflater.From(_context).Inflate(Resource.Layout.ListViewItemOtherOption, null, false);
				Button btn = row.FindViewById<Button>(Resource.Id.listViewItemOtherOptionOtherButton);
				btn.Text = "Other";


			}

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
