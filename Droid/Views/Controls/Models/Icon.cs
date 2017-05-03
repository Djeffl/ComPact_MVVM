using System;
using System.Collections.Generic;

namespace ComPact.Droid.Models
{
	// Photo: contains image resource ID and caption:
	public class Icon
	{
		// Photo ID for this photo:
		public string Name { get; set; }
		public int IconId { get; set; }

	}
	public class IconList
	{
		static List<Icon> BuiltInIcons = new List<Icon>{
			new Icon(){ Name = "Pets", IconId = Resource.Drawable.ic_pets_white_24dp },
			new Icon(){ Name = "Alarm", IconId = Resource.Drawable.ic_alarm_white_24dp },
			//new Icon(){ Name = "Check", IconId = Resource.Drawable.ic_check_white_24dp },
			new Icon(){ Name = "Power", IconId = Resource.Drawable.ic_power_white_24dp },
			new Icon(){ Name = "Store", IconId = Resource.Drawable.ic_store_white_24dp },
			new Icon(){ Name = "School", IconId = Resource.Drawable.ic_school_white_24dp },
			new Icon(){ Name = "TrashCan", IconId = Resource.Drawable.ic_delete_white_24dp },
			new Icon(){ Name = "Folder", IconId = Resource.Drawable.ic_folder_white_24dp },
			//new Icon(){ Name = "Payment", IconId = Resource.Drawable.ic_payment_white_24dp },
			new Icon(){ Name = "Message", IconId = Resource.Drawable.ic_message_white_24dp },
			new Icon(){ Name = "Healing", IconId = Resource.Drawable.ic_healing_white_24dp },
			new Icon(){ Name = "PanTool", IconId = Resource.Drawable.ic_pan_tool_white_24dp },
			//new Icon(){ Name = "More", IconId = Resource.Drawable.ic_more_vert_white_24dp },
			new Icon(){ Name = "Pencil", IconId = Resource.Drawable.ic_mode_edit_white_24dp },
			new Icon(){ Name = "Taxi", IconId = Resource.Drawable.ic_local_taxi_white_24dp },
			new Icon(){ Name = "Assignment", IconId = Resource.Drawable.ic_assignment_white_24dp },
			new Icon(){ Name = "Lens", IconId = Resource.Drawable.ic_color_lens_white_24dp },
			//new Icon(){ Name = "BackArrow", IconId = Resource.Drawable.ic_arrow_back_white_24dp },
			new Icon(){ Name = "CameraRoll", IconId = Resource.Drawable.ic_camera_roll_white_24dp },
			new Icon(){ Name = "Priority", IconId = Resource.Drawable.ic_priority_high_white_24dp },
			new Icon(){ Name = "ChildFriendly", IconId = Resource.Drawable.ic_child_friendly_white_24dp },
			new Icon(){ Name = "RestaurantMenu", IconId = Resource.Drawable.ic_restaurant_menu_white_24dp },
			new Icon(){ Name = "Printer", IconId = Resource.Drawable.ic_local_printshop_white_24dp },
			new Icon(){ Name = "Car", IconId = Resource.Drawable.ic_time_to_leave_white_24dp },
		};

		private List<Icon> icons;

		public IconList()
		{
			icons = BuiltInIcons;
		}

		public Icon this[int i]
		{
			get { return icons[i]; }
		}
		public Icon FindByName(string name)
		{
			foreach (Icon icon in BuiltInIcons)
			{
				if (icon.Name == name)
				{
					return icon;
				}
			}
			return BuiltInIcons[1];
		}
		public int Count
		{
			get { return icons.Count; }
		}
	}
}
