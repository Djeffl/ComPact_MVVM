using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Helpers;
using Android.Views;
using ComPact.Droid.Models;
using System.Collections.Generic;
using ComPact.Models;
using System.Collections.ObjectModel;
using Android.Graphics;

namespace ComPact.Droid.Fragments
{
	public class AssignmentsFragment : BaseFragment
	{
		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		FloatingActionButton _addTaskFloatingActionButton;
		ListView _tasksListView;
		IconList _iconList = new IconList();
		//data
		ObservableCollection<ComPact.Models.Assignment> _assignments;
		public ObservableCollection<ComPact.Models.Assignment> Assignments
		{
			get
			{
				return _assignments;
			}
			set
			{
				_assignments = value;
				SetAssignmentsListView();

			}
		}
		private List<int> resources = new List<int>();

		User _user;
		public User User
		{
			get
			{
				return _user;
			}
			set
			{
				_user = value;
				bindings.Add(this.SetBinding(() => ViewModel.User.Admin, () => _addTaskFloatingActionButton.Visibility).ConvertSourceToTarget((arg) => {
					return arg ? ViewStates.Visible : ViewStates.Gone;
				}));
			}
		}


		AssignmentsViewModel ViewModel
		{
			get
			{
				return App.Locator.TasksViewModel;
			}
		}
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			Init();

			HandleEvents();

			//Data & services
			SetBindings();
			SetCommands();

			// ListView Item Click Listener
			//_tasksListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
			//{
			//	String selectedFromList = (string)_tasksListView.GetItemAtPosition(e.Position);
			//	Toast.MakeText(Application.Context, selectedFromList, ToastLength.Long).Show();
			//	ViewModel.AssignmentsPostionCommand?.Execute(e.Position);
			//};
		}
	

		public override void OnResume()
		{
			base.OnResume();
			ViewModel.LoadDataCommand?.Execute(null);
		}



		protected void Init()
		{
			_addTaskFloatingActionButton = View.FindViewById<FloatingActionButton>(Resource.Id.activityTasksAddTaskFloatingActionButton);
			_tasksListView = View.FindViewById<ListView>(Resource.Id.activityTasksTasksListView);
		}

		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.Assignments, () => Assignments));
			bindings.Add(this.SetBinding(() => ViewModel.User, () => User));
		}
		void SetCommands()
		{
			_addTaskFloatingActionButton.SetCommand("Click", ViewModel.AddAssignmentRedirectCommand);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			return inflater.Inflate(Resource.Layout.FragmentTasks, container, false);
		}

		void SetAssignmentsListView()
		{
			_tasksListView.Adapter = ViewModel.Assignments.GetAdapter(GetAssignmentsAdapter);
		}

		private View GetAssignmentsAdapter(int position, ComPact.Models.Assignment assignment, View convertView)
		{
			// Not reusing views
			LayoutInflater inflater = LayoutInflater.From(Application.Context);
			convertView = inflater.Inflate(Resource.Layout.ListViewAssignment, null);

			ImageView iconImageView = convertView.FindViewById<ImageView>(Resource.Id.listViewTaskImageImageView);
			TextView nameTextView = convertView.FindViewById<TextView>(Resource.Id.listViewTaskNameTextView);
			CheckBox checkBox = convertView.FindViewById<CheckBox>(Resource.Id.listViewTaskDoneCheckBox);

			//Icon Image
			if (ViewModel.User.Admin)
			{
				iconImageView.SetImageResource(Resource.Drawable.Profile_placeholderImage);
			}
			else
			{
				iconImageView.SetImageResource(_iconList.FindByName(assignment.IconName).IconId);
			}

			//Name Assignment
			nameTextView.Text = assignment.ItemName;

			//TextView emailTextView = convertView.FindViewById<TextView>(Resource.Id.listViewTaskDoneCheckBox);
			//emailTextView.Text = members.Email;

			//CheckBox Assignment Done
			checkBox.SetCommand("Click", ViewModel.AssignmentDoneCommand, assignment);

			if (assignment.Description != null && assignment.Description != "")
			{
				convertView.SetCommand("Click", ViewModel.DetailAssignmentRedirectCommand, assignment);
			}
			else
			{
				if (ViewModel.User.Admin)
				{
					convertView.SetCommand("Click", ViewModel.DetailAssignmentRedirectCommand, assignment);
				}
				convertView.SetBackgroundColor(new Color(238, 238, 238));
			}

			return convertView;
		}
	}
}