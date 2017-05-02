using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace ComPact.Droid.Locations
{
	[Activity]
	public class DetailLocationActivity : BaseActivity
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
		Location _location;
		public Location Location
		{
			get
			{
				return _location;
			}
			set
			{
				_location = value;
				//SET LIST
                FillView();
			}
		}

		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		//__________________________Header______________________________
		ImageView _backImageView;
		TextView _titleTextView;
		ImageView _optionsImageView;
		//__________________________Others______________________________
		ImageView _deleteLocationImageView;
		TextView _nameTextView;
		TextView _fullAdressTextView;
		TextView _radiusTextView;
		ListView _membersListView;
		FloatingActionButton _EditRedirectFloatingActionButton;
		//Bind Viewmodel to activity
		DetailLocationViewModel ViewModel
		{
			get
			{
				return App.Locator.DetailLocationViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityDetailLocation);

			//Init elements
			FindViews();
			//init
			Init();
			//bindings
			SetBindings();
			//Use Commands
			SetCommands();
		}
		protected override void OnResume()
		{
			base.OnResume();

			ViewModel.LoadDataCommand.Execute(null);
		}
		//protected override void OnDestroy()
		//{
		//	base.OnDestroy();
		//}
		/**
		 * Init Views
		 */
		void FindViews()
		{
			//__________________________Header______________________________
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);
			_optionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
			//__________________________Others______________________________
			_deleteLocationImageView = FindViewById<ImageView>(Resource.Id.activityDetailLocationDeleteLocationImageView);
			_nameTextView = FindViewById<TextView>(Resource.Id.activityDetailLocationNameTextView);
			_radiusTextView = FindViewById<TextView>(Resource.Id.activityDetailLocationRadiusTextView);
			_fullAdressTextView = FindViewById<TextView>(Resource.Id.activityDetailLocationFullAdressTextView);
			_membersListView = FindViewById<ListView>(Resource.Id.activityDetailLocationMembersListView);
			_EditRedirectFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityDetailLocationFloatingActionButton);

		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.Location, () => Location));
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			_deleteLocationImageView.SetCommand("Click", ViewModel.DeleteLocationCommand);
			_EditRedirectFloatingActionButton.SetCommand("Click", ViewModel.EditLocationRedirectCommand);
		}
		void Init()
		{
			Location location = Nav.GetAndRemoveParameter<Location>(base.Intent);
			ViewModel.SetLocationCommand.Execute(location);
			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Location";
		}
		void FillView()
		{
			_nameTextView.Text = Location.Name;
			_radiusTextView.Text = Location.Radius.ToString();
			_fullAdressTextView.Text = String.Format("{0} - {1}", Location.Street, Location.City);
			SetMemberListView();
		}
		#endregion

		void SetMemberListView()
		{
			_membersListView.Adapter = ViewModel.Location.Members.GetAdapter(GetMemberAdapter);
		}
		private View GetMemberAdapter(int position, Member member, View convertView)
		{
			// Not reusing views here
			convertView = LayoutInflater.Inflate(Resource.Layout.ListViewPersonNormal, null);

			TextView nameTextView = convertView.FindViewById<TextView>(Resource.Id.listViewPersonNormalNameTextView);
			nameTextView.Text = member.FullName();
			TextView emailTextView = convertView.FindViewById<TextView>(Resource.Id.listViewPersonNormalEmailTextView);
			emailTextView.Text = member.Email;

			return convertView;
		}
	}
}
