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
		ObservableCollection<Assignment> _assignments;
		public ObservableCollection<Assignment> Assignments
		{
			get
			{
				return _assignments;
			}
			set
			{
				_assignments = value;
				SetMemberListView();

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

			//items = new List<Assignment>();
			//items.Add(new Assignment() { ItemName = "item 1" });
			//items.Add(new Assignment() { ItemName = "item 2" });
			//items.Add(new Assignment() { ItemName = "item 3" });

			//var adapter = new AdapterAssignment(Application.Context, items);
			// Assign adapter to ListView
			//_tasksListView.Adapter = adapter;

		

			// ListView Item Click Listener
			_tasksListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
			{
				String selectedFromList = (string)_tasksListView.GetItemAtPosition(e.Position);
				Toast.MakeText(Application.Context, selectedFromList, ToastLength.Long).Show();
				ViewModel.AssignmentsPostionCommand?.Execute(e.Position);
			};
		}
	

		public override void OnResume()
		{
			base.OnResume();
			ViewModel.GetAssignmentsCommand?.Execute(null);
		}



		protected void Init()
		{
			//_itemNameEditText = View.FindViewById<EditText>(Resource.Id.FragmentTasksNameItemTextView);
			//_describtionEditText = View.FindViewById<EditText>(Resource.Id.FragmentTasksDescribtionTextView);
			//_createTaskButton = View.FindViewById<Button>(Resource.Id.FragmentTasksCreateTaskButton);
			_addTaskFloatingActionButton = View.FindViewById<FloatingActionButton>(Resource.Id.activityTasksAddTaskFloatingActionButton);
			_tasksListView = View.FindViewById<ListView>(Resource.Id.activityTasksTasksListView);

			//FILL UP 
			//SET MEMBERS & ASSIGNMENTS ITEMS
			/**
			 * this will execute an async method
			 * when async finished, create+set listadapter
			 */
			ViewModel.GetAssignmentsCommand?.Execute(null);
			ViewModel.GetUserCommand?.Execute(null);
		}

		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.Assignments, () => Assignments));
			bindings.Add(this.SetBinding(() => ViewModel.User, () => User));
			//this.SetBindings(() => ViewModel.Done, () => _tasksListView, BindingMode.TwoWay);
			//this.SetBinding(() => ViewModel.ItemName, () => _itemNameEditText.Text, BindingMode.TwoWay);
			//Binding itemPosition = this.SetBinding(() => ViewModel., () => _describtionEditText.Text, BindingMode.TwoWay);
		}
		void SetCommands()
		{
			//_createTaskButton.SetCommand("Click", ViewModel.CreateTaskAsyncCommand);
			//ViewModel.CreateTaskAsyncCommand
			_addTaskFloatingActionButton.SetCommand("Click", ViewModel.AddAssignmentRedirectCommand);
			//_tasksListView.SetCommand("Click", ViewModel.AssignmentDetailRedirectCommand); // Bind nog met item, list 


		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			return inflater.Inflate(Resource.Layout.FragmentTasks, container, false);
		}


		private View GetAssignmentsAdapter(int position, Assignment assignment, View convertView)
		{
			// Not reusing views here
			LayoutInflater inflater = LayoutInflater.From(Application.Context);
			convertView = inflater.Inflate(Resource.Layout.ListViewAssignment, null);
			ImageView iconImageView = convertView.FindViewById<ImageView>(Resource.Id.listViewTaskImageImageView);
			if (ViewModel.User.Admin)
			{
				iconImageView.SetImageResource(Resource.Drawable.Profile_placeholderImage);
				//iconImageView.SetImageBitmap();
			}
			else
			{
				iconImageView.SetImageResource(_iconList.FindByName(assignment.IconName).IconId);
			}
			//iconImageView.SetImageResource(_iconList.FindByName(assignment.IconName).IconId);
			TextView nameTextView = convertView.FindViewById<TextView>(Resource.Id.listViewTaskNameTextView);
			nameTextView.Text = assignment.ItemName;

			//TextView emailTextView = convertView.FindViewById<TextView>(Resource.Id.listViewTaskDoneCheckBox);
			//emailTextView.Text = members.Email;

			CheckBox checkBox = convertView.FindViewById<CheckBox>(Resource.Id.listViewTaskDoneCheckBox);
			checkBox.Click += (sender, e) =>
			{

				//TODO Verwijder
				/**
				 * Command met binding
				 * * Promp "are you sure"
				 * Remove data VM
				 * API call Task -> done
				 */
				//new AlertDialog.Builder()
				//			   .SetTitle("Finished")
				//               .SetMessage("Is this task finished?")
	   //                        .SetNegativeButton(Android.Resource.String.No, (sender1, e1) =>{})
	   //                        .SetPositiveButton(Android.Resource.String.Yes, (senderAlert, args) =>
				//				{
									ViewModel.AssignmentDoneCommand?.Execute(assignment);
									
								//}).Show();

				// your remaining code
			};
			//if (ViewModel.User.Admin)
			//{
				//convertView.Click += (sender, e) =>
				//{
				//	ViewModel.DetailAssignmentRedirectCommand.Execute(assignment);
				//	System.Diagnostics.Debug.WriteLine("clicked");
				//};
			//}
			//else
			//{

				if (assignment.Description != null && assignment.Description != "")
				{
					convertView.Click += (sender, e) =>
					{
						ViewModel.DetailAssignmentRedirectCommand.Execute(assignment);
						System.Diagnostics.Debug.WriteLine("clicked");
					};
				}
				else
				{
					if (ViewModel.User.Admin)
					{
						convertView.Click += (sender, e) =>
						{
							ViewModel.DetailAssignmentRedirectCommand.Execute(assignment);
							System.Diagnostics.Debug.WriteLine("clicked");
						};
					}
			
					convertView.SetBackgroundColor(new Android.Graphics.Color(238, 238, 238));
				}
			//}



			return convertView;
		}
		void SetMemberListView()
		{
			_tasksListView.Adapter = ViewModel.Assignments.GetAdapter(GetAssignmentsAdapter);

		}



		//          // ListView Item Click Listener
		//          listView.setOnItemClickListener(new OnItemClickListener()
		//{

		//	@Override

		//		  public void onItemClick(AdapterView<?> parent, View view,
		//			 int position, long id)
		//{

		//	// ListView Clicked item index
		//	int itemPosition = position;

		//	// ListView Clicked item value
		//	String itemValue = (String)listView.getItemAtPosition(position);

		//	// Show Alert 
		//	Toast.makeText(getApplicationContext(),
		//	  "Position :" + itemPosition + "  ListItem : " + itemValue, Toast.LENGTH_LONG)
		//	  .show();

	}
}