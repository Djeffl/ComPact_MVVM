using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using ComPact.Models;

namespace ComPact.Droid.Models
{
	public class AdapterTask : AdapterBase<Task>
	{
		public AdapterTask(Context context, List<Task> items) : base(context, items)
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
	}
}
