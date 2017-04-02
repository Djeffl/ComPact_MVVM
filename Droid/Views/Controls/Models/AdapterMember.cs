using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace ComPact.Droid.Models
{
	public class AdapterMember: AdapterBase<User>
	{
		public AdapterMember(Context context, List<User> items) : base(context, items)
		{
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null)
			{
				row = LayoutInflater.From(_context).Inflate(Resource.Layout.ListViewPerson, null, false);
			}
			TextView nameTextView = row.FindViewById<TextView>(Resource.Id.listViewPersonNameTextView);
			nameTextView.Text = Items[position].FullName();

			TextView emailTextView = row.FindViewById<TextView>(Resource.Id.listViewPersonEmailTextView);
			emailTextView.Text = Items[position].Email;

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
