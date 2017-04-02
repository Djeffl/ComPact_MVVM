
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	[Activity(Label = "AddTaskActivity")]
	public class AddTaskActivity : BaseActivity
	{
		//Local variables
		List<User> items;
		Binding binding;
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
		AddTaskViewModel ViewModel
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
			SetContentView(Resource.Layout.ActivityAddTask);

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

			_itemNameSpinner = FindViewById<Spinner>(Resource.Id.activityAddWhatItemSpinner);
			_descriptionEditText = FindViewById<EditText>(Resource.Id.activityAddDescriptionEditText);
			_membersListView = FindViewById<ListView>(Resource.Id.activityAddTaskListView);
			_addTaskFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityTasksAddTaskFloatingActionButton);

			//FILL UP 
			var taskOptions = new List<string>() { "Choose an option...", "Take out trash", "Groceries", "Feed pet", "other" };


			items = new List<User>();
			User user1 = new User
			{
				Email = "jeffliekens7@hotmail.com",
				FirstName = "Jeff",
				LastName = "Liekens",
				Tasks = new List<Task>()
			};
			User user2 = new User
			{
				Email = "liekensjeff@gmail.com",
				FirstName = "Nathan",
				LastName = "Liekens",
				Tasks = new List<Task>()
			};
			User user3 = new User
			{
				Email = "jeffliekens17@hotmail.com",
				FirstName = "Thomas",
				LastName = "Liekens",
				Tasks = new List<Task>()
			};

			items.Add(user1);
			items.Add(user2);
			items.Add(user3);

			//Create custom adapter and assign to spinner
			var adapter = new AdapterTaskItemNameSpinner(Application.Context, taskOptions);
			_itemNameSpinner.Adapter = adapter;
			_itemNameSpinner.ItemSelected += (sender, e) => 
			{
				ViewModel.ItemNameCommand?.Execute(e.Position);
			};


			//Create custom adapter and assign to listview
			var listAdapter = new AdapterMember(Application.Context, items);
			_membersListView.Adapter = listAdapter;
			_membersListView.ItemSelected += (sender, e) =>
			{
				ViewModel.ItemNameCommand?.Execute(e.Position);
			};


			// ListView Item Click Listener
			_membersListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
			{
				String selectedFromList = (string)_membersListView.GetItemAtPosition(e.Position);
				Toast.MakeText(Application.Context, selectedFromList, ToastLength.Long).Show();
			};

		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			//this.SetBinding(() => ViewModel.Name, () => ((TextView)_itemNameSpinner.SelectedView).Text, BindingMode.TwoWay);
			//this.SetBinding(() => ViewModel.Description, () => _descriptionEditText.Text, BindingMode.TwoWay);
			//this.SetBinding(() => _itemNameSpinner.SelectedView).WhenSourceChanges(Test);
			//ViewModel.ItemName

			//binding = this.SetBinding(() => ViewModel.User, () => items[_membersListView.SelectedItemPosition], BindingMode.TwoWay);
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_addTaskFloatingActionButton.SetCommand("Click", ViewModel.CreateTaskCommand);
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			//_membersListView.SetCommand("Click", ViewModel.CreateUserCommand, binding);
			//_itemNameSpinner.SetCommand("OnItemSelectedListener", ViewModel.Test?.Execute(
			//	(TextView)_itemNameSpinner.SelectedView).Text
			//);

		}
		void Test()
		{
			ViewModel.ItemNameCommand?.Execute(
				 ((TextView)_itemNameSpinner.SelectedView).Text
			);
		}
		#endregion
	}
}
