using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid.Models
{
	public class AdapterMember : AdapterBase<Member>
	{


		public AdapterMember(Context context, List<Member> items) : base(context, items)
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

			row.Click += (sender, e) => {
				System.Diagnostics.Debug.WriteLine("");
			};




			//ImageView imageImageView = row.FindViewById<ImageView>(Resource.Id.listViewTaskImageImageView);
			//imageImageView = (ImageView)Items[position].Image;

			//CheckBox doneCheckBox = row.FindViewById<CheckBox>(Resource.Id.listViewTaskDoneCheckBox);
			//if (Items[position].Tasks[position].Done == false || false)
			//{
			//	doneCheckBox.Checked = false;
			//}
			//else
			//{
			//	doneCheckBox.Checked = true;
			//}
			return row;
		}


	}
}
