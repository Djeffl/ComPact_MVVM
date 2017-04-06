
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using ComPact.Droid.Helpers;
using ComPact.Droid.Models;
using ComPact.Members;
using ComPact.Models;
using GalaSoft.MvvmLight.Helpers;
using Java.Util;

namespace ComPact.Droid.Tasks
{
	[Activity(Label = "AddTaskActivity", MainLauncher = true)]
	public class AddAssignmentActivity : BaseActivity
	{
		//Local variables
		List<CheckBox> checkboxes = new List<CheckBox>();
		RadioGroup radiog = new RadioGroup(Application.Context);

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
				SetAssignmentsItemsListView();
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
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_optionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);



			_descriptionEditText = FindViewById<EditText>(Resource.Id.activityAddAssignmentItemNameEditText);
			_membersListView = FindViewById<ListView>(Resource.Id.activityAddTaskListView);
			_addTaskFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityTasksAddTaskFloatingActionButton);

			//FILL UP 
			//SET MEMBERS & ASSIGNMENTS ITEMS
			/**
			 * this will execute an async method
			 * when async finished, create+set listadapter
			 */
			ViewModel.GetMembersCommand?.Execute(null);
			Assignments = ViewModel.AssignmentsOptions;

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
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.ItemName, () => _itemNameEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Description, () => _descriptionEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Members, () => Members));
			//bindings.Add(this.SetBinding(() => ViewModel.Icons, () => Icons));
			bindings.Add(this.SetBinding(() => ViewModel.AssignmentsOptions,() => Assignments));

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
				ViewModel.CreateTaskCommand.Execute(null);
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
			//AdapterMember adapterMember = new AdapterMember(this, Members.ToList());
			//adapterMember.GetView(0, LayoutInflater.From(Application.Context).Inflate(Resource.Layout.ListViewPerson, null, false), );

			_membersListView.Adapter = ViewModel.Members.GetAdapter(GetMemberAdapter); //adapterMember;//new AdapterMember(Application.Context, Members.ToList());

		}
		void SetAssignmentsItemsListView()
		{
			//AdapterTaskItemNameSpinner atns = new AdapterTaskItemNameSpinner(Application.Context, Assignments.ToList());
		}

		List<RadioButton> radioButtons = new List<RadioButton>();

		private View GetMemberAdapter(int position, Member members, View convertView)
		{
			// Not reusing views here
			convertView = LayoutInflater.Inflate(Resource.Layout.ListViewPerson, null);

			TextView nameTextView = convertView.FindViewById<TextView>(Resource.Id.listViewPersonNameTextView);
			nameTextView.Text = members.FullName();

			TextView emailTextView = convertView.FindViewById<TextView>(Resource.Id.listViewPersonEmailTextView);
			emailTextView.Text = members.Email;

			RadioButton isSelectedRadioButton = convertView.FindViewById<RadioButton>(Resource.Id.listViewPersonRadioButton);
			//EVENT OP ROW CLICK ZETTEN ALS ROW SELECTED DAN TOON JE IMAGE VIEW WAAR HET ITEM IS GEDISPLAYED

			radioButtons.Add(isSelectedRadioButton);
			isSelectedRadioButton.Click += (sender, e) =>
			{
				foreach (RadioButton radioButton in radioButtons)
				{
					radioButton.Checked = false;
				}
				isSelectedRadioButton.Checked = true;
			};

			//CheckBox checkBox = convertView.FindViewById<CheckBox>(Resource.Id.listViewPersonAddCheckBox);
			//checkboxes.Add(checkBox);
			//TODO how to pass data onClick? can't conver to lambda
			//checkBox.SetCommand("Click", ViewModel.MemberSelectedCommand, convertView);
			//checkBox.Click += (sender, e) =>
			//{
			//	System.Diagnostics.Debug.WriteLine("clicked");
			//	ViewModel.MemberSelectedCommand?.Execute(members);
			//};
			//convertView.Click += (sender, e) =>
			//{
			//	System.Diagnostics.Debug.WriteLine("clicked");
			//};

			return convertView;
		}
		void ClearFields()
		{
			_descriptionEditText.Text = "";
			foreach (CheckBox checkBox in checkboxes)
			{
				checkBox.Checked = false;
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
