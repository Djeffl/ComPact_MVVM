
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ComPact.Droid.Controls;
using ComPact.Droid.Helpers;
using ComPact.Droid.Models;
using ComPact.Members;
using ComPact.Models;
using GalaSoft.MvvmLight.Helpers;
using Java.Util;

namespace ComPact.Droid.Tasks
{
	[Activity(Label = "AddTaskActivity")]
	public class AddAssignmentActivity : BaseActivity
	{
		//Local variables
		List<CheckBox> checkboxes = new List<CheckBox>();
		Color _colorFilter;
		Color _resetColorFilter;
		IconList iconList;
		List<RadioButton> radioButtons;
		string iconName;
		ObservableCollection<Member> _members;
		public ObservableCollection<Member> Members
		{
			get
			{
				return _members;
			}
			set
			{
				_members = value;
				SetMemberListView();
			}
		}
		ObservableCollection<string> _assignments;
		public ObservableCollection<string> Assignments
		{
			get
			{
				return _assignments;
			}
			set
			{
				_assignments = value;
			}
		}
		public string IconName { get; set; }


		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		ImageView _backImageView;
		TextView _titleTextView;
		ImageView _optionsImageView;

		EditText _itemNameEditText;
		EditText _descriptionEditText;
		ListView _membersListView;
		FloatingActionButton _addTaskFloatingActionButton;
		RecyclerView _recyclerView;
		//Bind Viewmodel to activity
		AddAssignmentViewModel ViewModel
		{
			get
			{
				return App.Locator.AddTaskViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityAddAssignment);



			//Init elements
			Init();
			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "New Task";
			//bindings
			SetBindings();

			//Use Commands
			SetCommands();
		}
		/**
		 * Init Views
		 */
		void Init()
		{
			_colorFilter = new Color(ContextCompat.GetColor(this, Resource.Color.yellow_accent_color));
			_resetColorFilter = new Color(ContextCompat.GetColor(this, Resource.Color.accent_color));
			radioButtons = new List<RadioButton>();

			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_optionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);


			_itemNameEditText = FindViewById<EditText>(Resource.Id.activityAddAssignmentItemNameEditText);
			_descriptionEditText = FindViewById<EditText>(Resource.Id.activityAddAssignmentDescriptionEditText);
			_membersListView = FindViewById<ListView>(Resource.Id.activityAddTaskListView);
			_addTaskFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityTasksAddTaskFloatingActionButton);

			//FILL UP 
			//SET MEMBERS & ASSIGNMENTS ITEMS
			/**
			 * this will execute an async method
			 * when async finished, create+set listadapter
			 */
			ViewModel.GetMembersCommand?.Execute(null);

			_membersListView.ItemSelected += (sender, e) =>
			{
				Console.WriteLine("clicked");
				var checkbox = e.View.FindViewById<CheckBox>(Resource.Id.listViewTaskDoneCheckBox);
			};
			SetIconRecyclerView();

		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.ItemName, () => _itemNameEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Description, () => _descriptionEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Members, () => Members));
			//bindings.Add(this.SetBinding(() => ViewModel.MailMember, () => EmailMember, BindingMode.TwoWay));
			//bindings.Add(this.SetBinding(() => ViewModel.Icons, () => Icons));
			//binding = this.SetBinding(() => ViewModel.User, () => items[_membersListView.SelectedItemPosition], BindingMode.TwoWay);
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			//_addTaskFloatingActionButton.SetCommand("Click", ViewModel.CreateTaskCommand);
			_addTaskFloatingActionButton.Click += (sender, e) =>
			{
				var assignment = new Assignment()
				{
					MemberId = ViewModel.SelectedMember.Id,
					ItemName = _itemNameEditText.Text,
					Description = _descriptionEditText.Text,
					IconName = ViewModel.IconName,
					Done = false
				};
				ViewModel.CreateAssignmentCommand?.Execute(assignment);
				ClearFields();
			};
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			//_membersListView.SetCommand("Click", ViewModel.CreateUserCommand, binding);
			//_itemNameSpinner.SetCommand("OnItemSelectedListener", ViewModel.Test?.Execute(
			//	(TextView)_itemNameSpinner.SelectedView).Text
			//);
		}

		void SetMemberListView()
		{
			_membersListView.Adapter = ViewModel.Members.GetAdapter(GetMemberAdapter); //adapterMember;//new AdapterMember(Application.Context, Members.ToList());
		}
		void SetIconRecyclerView()
		{
			iconList = new IconList();
			_recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
			RecyclerView.LayoutManager _layoutManager = new GridLayoutManager(this, 4);//new LinearLayoutManager(this)
			_recyclerView.SetLayoutManager(_layoutManager);

			CustomRecyclerViewAdapter iconAdapter = new CustomRecyclerViewAdapter(iconList);
			iconAdapter.ItemClick += OnIconClick;
			_recyclerView.SetAdapter(iconAdapter);
		}

		private View GetMemberAdapter(int position, Member member, View convertView)
		{
			// Not reusing views here
			convertView = LayoutInflater.Inflate(Resource.Layout.ListViewPerson, null);

			TextView nameTextView = convertView.FindViewById<TextView>(Resource.Id.listViewPersonNameTextView);
			nameTextView.Text = member.FullName();
			TextView emailTextView = convertView.FindViewById<TextView>(Resource.Id.listViewPersonEmailTextView);
			emailTextView.Text = member.Email;
			var isSelectedRadioButton = convertView.FindViewById<RadioButton>(Resource.Id.listViewPersonRadioButton);
			radioButtons.Add(isSelectedRadioButton);

			isSelectedRadioButton.SetCommand("Click", ViewModel.MemberSelectedCommand, member);
			//TODO CLEAN ME UP
			isSelectedRadioButton.Click += (sender, e) =>
			{
				foreach (RadioButton radioButton in radioButtons)
				{
					radioButton.Checked = false;
				}
				isSelectedRadioButton.Checked = true;
			};
			return convertView;
		}
		void OnIconClick(object sender, int position)
		{
			ViewModel.IconName = new IconList()[position].Name;
			// Display a toast that briefly shows the enumeration of the selected photo:
			int photoNum = position + 1;
			//Toast.MakeText(this, "This is photo number " + photoNum, ToastLength.Short).Show();

			for (int iconPlace = 0; iconPlace < iconList.Count; iconPlace++)
			{
				((ImageView)((LinearLayout)_recyclerView.GetChildAt(iconPlace)).GetChildAt(0)).SetColorFilter(_resetColorFilter);
			}
			((ImageView)((LinearLayout)_recyclerView.GetChildAt(position)).GetChildAt(0)).SetColorFilter(_colorFilter);
		}
		void ClearFields()
		{
			_descriptionEditText.Text = "";
			_itemNameEditText.Text = "";
			foreach (CheckBox checkBox in checkboxes)
			{
				checkBox.Checked = false;
			}
			// Display a toast that briefly shows the enumeration of the selected photo:
			for (int iconPlace = 0; iconPlace < iconList.Count; iconPlace++)
			{
				((ImageView)((LinearLayout)_recyclerView.GetChildAt(iconPlace)).GetChildAt(0)).SetColorFilter(_resetColorFilter);
			}
		}
		//void Test()
		//{
		//	ViewModel.ItemNameCommand?.Execute(
		//		 ((TextView)_itemNameSpinner.SelectedView).Text
		//	);
		//}
		#endregion
	}
}
