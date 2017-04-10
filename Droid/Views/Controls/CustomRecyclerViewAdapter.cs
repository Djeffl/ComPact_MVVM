using System;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ComPact.Droid.Models;

namespace ComPact.Droid.Controls
{
	public class CustomRecyclerViewAdapter : RecyclerView.Adapter //<CustomRecyclerViewAdapter.ViewHolder>
	{
		// Event handler for item clicks:
		public event EventHandler<int> ItemClick;

		// Return the number of photos available in the photo album:
		public override int ItemCount
		{
			get { return IconList.Count; }
		}

		// Underlying data set (a icon list) Findable in Controls/Models/Icon.cs
		public IconList IconList { get; private set; }


		// Load the adapter with the data set (photo album) at construction time:
		public CustomRecyclerViewAdapter(IconList iconList)
		{
			IconList = iconList;

		}

		// Create a new photo CardView (invoked by the layout manager): 
		public override RecyclerView.ViewHolder	OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Drawable.Recyclerview_item, parent, false);

			// Create a ViewHolder to find and hold these view references, and 
			// register OnClick with the view holder:
			IconListViewHolder vh = new IconListViewHolder(itemView, OnClick);
			return vh;
		}

		// Fill in the contents of the photo card (invoked by the layout manager):
		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			IconListViewHolder vh = holder as IconListViewHolder;
			// Set the ImageView and TextView in this ViewHolder's CardView 
			// from this position in the photo album:
			vh.Image.SetImageResource(IconList[position].IconId);
		}



		// Raise an event when the item-click takes place:
		void OnClick(int position)
		{
			if (ItemClick != null)
				ItemClick(this, position);
		}
	}
	public class IconListViewHolder : RecyclerView.ViewHolder
	{
		public ImageView Image { get; private set; }

		// Get references to the views defined in the layout.
		public IconListViewHolder(View itemView, Action<int> listener)
			: base(itemView)
		{
			// Locate and cache view references:
			Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);

			// Detect user clicks on the item view and report which item
			// was clicked (by position) to the listener:
			itemView.Click += (sender, e) => listener(base.Position);
		}
	}
}
