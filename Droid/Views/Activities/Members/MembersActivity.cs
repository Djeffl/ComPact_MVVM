using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using ComPact.ViewModel.Members;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid.Members
{
	[Activity(Label = "MembersActivity")]
	public class MembersActivity : BaseActivity
	{
		//Local variables

		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		ImageView _backImageView;
		ImageView _OptionsImageView;
		TextView _titleTextView;

		ListView _memberListView;
		FloatingActionButton _addMemberFloatingActionButton;

		//Bind Viewmodel to activity
		MembersViewModel ViewModel
		{
			get
			{
				return App.Locator.MembersViewModel;
			}
		}
		//Parameters
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
				SetListViewAdapter();
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityMembers);

			//Init elements
			FindViews();
			Init();
			_OptionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Members";

			//bindings
			SetBindings();

			//Use Commands
			SetCommands();
		}
		protected override void OnResume()
		{
			base.OnResume();
			ViewModel.LoadDataCommand?.Execute(null);
		}
		/**
		 * Init Views
		 */
		void FindViews()
		{
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_OptionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);

			_memberListView = FindViewById<ListView>(Resource.Id.activityMembersMemberListView);
			_addMemberFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityMembersAddMemberFloatingActionButton);
		}

		void Init()
		{
			
		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.Members,() => Members, BindingMode.OneWay));
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			_addMemberFloatingActionButton.SetCommand("Click", ViewModel.AddMembersRedirectCommand);
		}
		void SetListViewAdapter()
		{
			_memberListView.Adapter = ViewModel.Members.GetAdapter(GetMembersAdapter);
		}
		View GetMembersAdapter(int position, Member member, View convertView)
		{
			// Not reusing views here
			LayoutInflater inflater = LayoutInflater.From(Application.Context);
			convertView = inflater.Inflate(Resource.Layout.ListViewSimpleListview, null);
			TextView textView = convertView.FindViewById<TextView>(Resource.Id.listViewSimpleListviewTextView);

			textView.Text = member.FullName();

			return convertView;
		}
		#endregion
	}
}
