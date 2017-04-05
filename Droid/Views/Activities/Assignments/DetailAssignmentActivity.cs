using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using ComPact.Assignments;
using ComPact.Models;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace ComPact.Droid
{
	[Activity(Label = "DetailAssignmentActivity")]
	public class DetailAssignmentActivity : BaseActivity
	{

		public NavigationService Nav
		{
			get
			{
				return (NavigationService)ServiceLocator.Current
					.GetInstance<INavigationService>();
			}
		}

		//Local variables

		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
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
				if (!_user.Admin)
				{
					_editTaskFloatingActionButton.Visibility = ViewStates.Gone;
				}
				else
				{
					_editTaskFloatingActionButton.Visibility = ViewStates.Visible;

				}
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
		ImageView _backImageView;
		TextView _titleTextView;
		ImageView _optionsImageView;

		Spinner _itemNameSpinner;
		EditText _descriptionEditText;
		ListView _membersListView;
		FloatingActionButton _editTaskFloatingActionButton;
		//Bind Viewmodel to activity
		DetailAssignmentViewModel ViewModel
		{
			get
			{
				return App.Locator.DetailAssignmentViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityDetailAssignment);


			Assignment = Nav.GetAndRemoveParameter<Assignment>(Intent);
			ViewModel.Assignment = Assignment;
			//Init elements
			Init();
			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Task";
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
			_editTaskFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityDetailAssignmentEditTaskFloatingActionButton);


			//FILL UP 
			//SET MEMBERS & ASSIGNMENTS ITEMS
			/**
			 * this will execute an async method
			 * when async finished, create+set listadapter
			 */
			ViewModel.GetUserCommand?.Execute(null);

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
			bindings.Add(this.SetBinding(() => ViewModel.Assignment, () => Assignment, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Description, () => _descriptionEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.User, () => User));

			//binding = this.SetBinding(() => ViewModel.User, () => items[_membersListView.SelectedItemPosition], BindingMode.TwoWay);
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_editTaskFloatingActionButton.SetCommand("Click", ViewModel.EditRedirectCommand);
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			//_membersListView.SetCommand("Click", ViewModel.CreateUserCommand, binding);
			//_itemNameSpinner.SetCommand("OnItemSelectedListener", ViewModel.Test?.Execute(
			//	(TextView)_itemNameSpinner.SelectedView).Text
			//);

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
