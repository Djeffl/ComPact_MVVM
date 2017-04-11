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
using ComPact.Droid.Models;
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
		IconList _iconList = new IconList();
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
				bindings.Add(this.SetBinding(() => ViewModel.Member, () => Member));
				_user = value;
				bindings.Add(this.SetBinding(() => ViewModel.User.Admin, () => _editTaskFloatingActionButton.Visibility).ConvertSourceToTarget((arg) =>
				{
					return arg ? ViewStates.Visible : ViewStates.Gone;
				}));
				bindings.Add(this.SetBinding(() => ViewModel.User.Admin, () => _memberLinearLayout.Visibility).ConvertSourceToTarget((arg) =>
				{
					return arg ? ViewStates.Visible : ViewStates.Gone;
				}));
				bindings.Add(this.SetBinding(() => ViewModel.User.Admin, () => _memberTextView.Visibility).ConvertSourceToTarget((arg) =>
				{
					return arg ? ViewStates.Visible : ViewStates.Gone;
				}));
			}
		}
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
				if (_member != null)
				{
					_PersonNameTextView.Text = Member.FullName();
					_PersonEmailTextView.Text = Member.Email;
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

		ImageView _iconImageView;
		TextView _itemNameTextView;
		ImageView _deleteImageView;
		TextView _descriptionTextView;


		TextView _memberTextView;
		LinearLayout _memberLinearLayout;
		TextView _PersonNameTextView;
		TextView _PersonEmailTextView;
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
			ViewModel.GetUserCommand.Execute(null);
			ViewModel.GetMemberCommand.Execute(Assignment.MemberId);
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

			_iconImageView = FindViewById<ImageView>(Resource.Id.activityDetailAssignmentIconImageView);
			_itemNameTextView = FindViewById<TextView>(Resource.Id.activityDetailAssignmentItemNameTitleTextView);
			_descriptionTextView = FindViewById<TextView>(Resource.Id.activityDetailAssignmentDescriptionTextView);

			_memberTextView = FindViewById<TextView>(Resource.Id.activityDetailAssignmentMemberTextView);
			_memberLinearLayout = FindViewById<LinearLayout>(Resource.Id.activityDetailAssignmentMemberLinearLayout);
			_PersonNameTextView = FindViewById<TextView>(Resource.Id.activityDetailAssignmentPersonNameTextView);
			_PersonEmailTextView = FindViewById<TextView>(Resource.Id.activityDetailAssignmentPersonEmailTextView);
			_editTaskFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityDetailAssignmentEditTaskFloatingActionButton);
			_deleteImageView = FindViewById<ImageView>(Resource.Id.activityDetailAssignmentDeleteImageView);

			//FILL UP 
			_itemNameTextView.Text = Assignment.ItemName;
			_descriptionTextView.Text = Assignment.Description != null ? Assignment.Description : "No description was given.";
			_iconImageView.SetImageResource(_iconList.FindByName(Assignment.IconName).IconId);



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
			_deleteImageView.SetCommand("Click", ViewModel?.DeleteAssignmentCommand);
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
