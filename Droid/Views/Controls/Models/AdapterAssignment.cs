using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using ComPact.Models;

namespace ComPact.Droid.Models
{
	public class AdapterAssignment : AdapterBase<Assignment>, AdapterView.IOnItemClickListener
	{
		
		public AdapterAssignment(Context context, List<Assignment> items) : base(context, items)
		{
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null)
			{
				row = LayoutInflater.From(_context).Inflate(Resource.Layout.ListViewTask, null, false);
			}
			TextView nameTextView = row.FindViewById<TextView>(Resource.Id.listViewTaskNameTextView);
			nameTextView.Text = Items[position].ItemName;

			//ImageView imageImageView = row.FindViewById<ImageView>(Resource.Id.listViewTaskImageImageView);
			//imageImageView = (ImageView)Items[position].Image;

			//CheckBox doneCheckBox = row.FindViewById<CheckBox>(Resource.Id.listViewTaskDoneCheckBox);
			//if (Items[position].Done == true)
			//{
			//	doneCheckBox.Checked = true;
			//}
			//else
			//{
			//	doneCheckBox.Checked = false;
			//}
			return row;
		}

		public void OnItemClick(AdapterView parent, View view, int position, long id)
		{
			Toast.MakeText(_context, position, ToastLength.Long).Show();
		}
	}
}
