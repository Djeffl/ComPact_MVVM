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
using ComPact.Assignments;
using ComPact.Droid.Controls;
using ComPact.Droid.Helpers;
using ComPact.Droid.Models;
using ComPact.Members;
using ComPact.Models;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Java.Util;
using Microsoft.Practices.ServiceLocation;

namespace ComPact.Droid.Assignments
{
	[Activity]
	public class EditAssignmentActivity : BaseActivity
	{

		public NavigationService Nav
		{
			get
			{
				return (NavigationService)ServiceLocator.Current
					.GetInstance<INavigationService>();
			}
		}
		Assignment _assignment;
		public Assignment Assignment
		{
			get
			{
				return _assignment;
			}
			set
			{
				_assignment = value;
			}
		}


		//Local variables
		List<CheckBox> checkboxes = new List<CheckBox>();
		Color _colorFilter;
		Color _resetColorFilter;
		IconList iconList;
		List<RadioButton> radioButtons = new List<RadioButton>();
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
		Assignment _assignments;
		public Assignment Assignments
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
		Member _member;
		public Member Member
		{ 
			get
			{
				return _member;
			}
			set
			{
				_member = value;
			}
		}


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

		//Bind Viewmodel to activity
		EditAssignmentViewModel ViewModel
		{
			get
			{
				return App.Locator.EditAssignmentViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityAddAssignment);

			//_membersListView.ItemSelected += (sender, e) =>
			//{
			//	Console.WriteLine("clicked");
			//	var checkbox = e.View.FindViewById<CheckBox>(Resource.Id.listViewTaskDoneCheckBox);
			//};
			SetIconRecyclerView();

			//Find views elements
			FindViews();

			//Init Elements
			Init();

			//bindings
			SetBindings();

			//Use Commands
			SetCommands();
		}
		/**
		 * Init Views
		 */
		void FindViews()
		{
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_optionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);

			_itemNameEditText = FindViewById<EditText>(Resource.Id.activityAddAssignmentItemNameEditText);
			_descriptionEditText = FindViewById<EditText>(Resource.Id.activityAddAssignmentDescriptionEditText);
			_membersListView = FindViewById<ListView>(Resource.Id.activityAddTaskListView);
			_addTaskFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityTasksAddTaskFloatingActionButton);

			_membersListView.ItemSelected += (sender, e) =>
			{
				Console.WriteLine("clicked");
				var checkbox = e.View.FindViewById<CheckBox>(Resource.Id.listViewTaskDoneCheckBox);
			};

			//Create custom adapter and assign to listview
			//_membersListView.ItemSelected += (sender, e) =>
			//{
			//	//ViewModel.ItemNameCommand?.Execute(e.Position);

			//};

			// ListView Item Click Listener
			//_membersListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
			//{
			//	String selectedFromList = (string)_membersListView.GetItemAtPosition(e.Position);
			//	Toast.MakeText(Application.Context, selectedFromList, ToastLength.Long).Show();
			//};

		}
		/**
		 * Init the views with the wanted data
		 */
		void Init()
		{
			ViewModel.LoadDataCommand?.Execute(null);
			Assignment = Nav.GetAndRemoveParameter<Assignment>(Intent);
			ViewModel.SetAssignmentCommand.Execute(Assignment);
			//Edit header
			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Edit Task";

			_colorFilter = new Color(ContextCompat.GetColor(this, Resource.Color.yellow_accent_color));
			_resetColorFilter = new Color(ContextCompat.GetColor(this, Resource.Color.accent_color));
		}


		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.Members, () => Members));

			bindings.Add(this.SetBinding(() => ViewModel.Assignment.ItemName, () => _itemNameEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Assignment.Description, () => _descriptionEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Assignment.IconName, () => IconName));
			bindings.Add(this.SetBinding(() => ViewModel.Assignment.Member, () => Member, BindingMode.TwoWay));
			//bindings.Add(this.SetBinding(() => ViewModel.Assignment.Member, () => Member, BindingMode.TwoWay));

			//binding = this.SetBinding(() => ViewModel.User, () => items[_membersListView.SelectedItemPosition], BindingMode.TwoWay);
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_addTaskFloatingActionButton.SetCommand("Click", ViewModel.UpdateAssignmentCommand);
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
		}

		void SetIconRecyclerView()
		{
			iconList = new IconList();
			_recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
			RecyclerView.LayoutManager _layoutManager = new GridLayoutManager(this, 4);
			_recyclerView.SetLayoutManager(_layoutManager);

			CustomRecyclerViewAdapter iconAdapter = new CustomRecyclerViewAdapter(iconList);
			iconAdapter.ItemClick += OnIconClick;
			_recyclerView.SetAdapter(iconAdapter);
		}

		void SetMemberListView()
		{
			_membersListView.Adapter = ViewModel.Members.GetAdapter(GetMemberAdapter);
		}

		private View GetMemberAdapter(int position, Member member, View convertView)
		{
			// Not reusing views here
			convertView = LayoutInflater.Inflate(Resource.Layout.ListViewPersonNormal, null);

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
			ViewModel.Assignment.IconName = new IconList()[position].Name;
			// Display a toast that briefly shows the enumeration of the selected photo:
			int photoNum = position + 1;
			//Toast.MakeText(this, "This is photo number " + photoNum, ToastLength.Short).Show();

			for (int iconPlace = 0; iconPlace < iconList.Count; iconPlace++)
			{
				((ImageView)((LinearLayout)_recyclerView.GetChildAt(iconPlace)).GetChildAt(0)).SetColorFilter(_resetColorFilter);
			}
			((ImageView)((LinearLayout)_recyclerView.GetChildAt(position)).GetChildAt(0)).SetColorFilter(_colorFilter);
		}

		#endregion
		protected override void OnStop()
		{
			base.OnStop();
			Finish();
		}
	}
}
