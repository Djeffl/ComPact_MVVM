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
using ComPact.Assignments;
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
	[Activity(Label = "EditAssignmentActivity")]
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

		Spinner _itemNameSpinner;
		EditText _descriptionEditText;
		ListView _membersListView;
		FloatingActionButton _addTaskFloatingActionButton;
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

			Assignment = Nav.GetAndRemoveParameter<Assignment>(Intent);

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

			_descriptionEditText = FindViewById<EditText>(Resource.Id.activityAddAssignmentDescriptionEditText);
			_membersListView = FindViewById<ListView>(Resource.Id.activityAddTaskListView);
			_addTaskFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityTasksAddTaskFloatingActionButton);

			_itemNameSpinner.SetSelection(2);
			ViewModel.Description = Assignment.Description;
			//_membersListView.
			//FILL UP 
			//SET MEMBERS & ASSIGNMENTS ITEMS
			/**
			 * this will execute an async method
			 * when async finished, create+set listadapter
			 */
			ViewModel.GetMembersCommand?.Execute(null);
			Assignments = ViewModel.AssignmentsOptions;

			_itemNameSpinner.ItemSelected += (sender, e) =>
			{
				//Send position back to ViewModel
				ViewModel.AssignmentsOptionsCommand?.Execute(e.Position);
			};
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
			bindings.Add(this.SetBinding(() => ViewModel.Members, () => Members));
			bindings.Add(this.SetBinding(() => ViewModel.AssignmentsOptions, () => Assignments));
			bindings.Add(this.SetBinding(() => ViewModel.Description, () => _descriptionEditText.Text, BindingMode.TwoWay));

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
				ViewModel.UpdateAssignmentCommand.Execute(null);
				ClearFields();
			};
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			//_addTaskFloatingActionButton.SetCommand("Click", ViewModel.UpdateAssignmentCommand);
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
			_itemNameSpinner.Adapter = new AdapterTaskItemNameSpinner(Application.Context, Assignments.ToList());
		}
		private View GetMemberAdapter(int position, Member members, View convertView)
		{
			// Not reusing views here
			convertView = LayoutInflater.Inflate(Resource.Layout.ListViewPerson, null);

			TextView nameTextView = convertView.FindViewById<TextView>(Resource.Id.listViewPersonNameTextView);
			nameTextView.Text = members.FullName();

			TextView emailTextView = convertView.FindViewById<TextView>(Resource.Id.listViewPersonEmailTextView);
			emailTextView.Text = members.Email;

			CheckBox checkBox = convertView.FindViewById<CheckBox>(Resource.Id.listViewPersonAddCheckBox);
			checkboxes.Add(checkBox);
			//TODO how to pass data onClick? can't conver to lambda
			//checkBox.SetCommand("Click", ViewModel.MemberSelectedCommand, convertView);
			checkBox.Click += (sender, e) =>
			{
				System.Diagnostics.Debug.WriteLine("clicked");
				ViewModel.MemberSelectedCommand?.Execute(members);
			};
			//convertView.Click += (sender, e) =>
			//{
			//	System.Diagnostics.Debug.WriteLine("clicked");
			//};

			return convertView;
		}
		void ClearFields()
		{
			_itemNameSpinner.SetSelection(0);
			_descriptionEditText.Text = "";
			foreach (CheckBox checkBox in checkboxes)
			{
				checkBox.Checked = false;
			}
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
