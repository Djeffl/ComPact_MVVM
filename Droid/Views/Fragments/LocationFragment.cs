
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using ComPact.Locations;
using ComPact.Models;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid.Fragments
{
	public class LocationFragment : BaseFragment
	{
		LocationsViewModel ViewModel
		{
			get
			{
				return App.Locator.LocationsViewModel;
			}
		}
		ObservableCollection<Location> _locations = new ObservableCollection<Location>();
		public ObservableCollection<Location> Locations
		{
			get
			{
				return _locations;
			}
			set
			{
				_locations = value;
				//Set adapter after payments are loaded
				SetListViewAdapter();
			}
		}
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
				bindings.Add(this.SetBinding(() => ViewModel.User.Admin, () => _addLocationFloatingActionButton.Visibility).ConvertSourceToTarget((arg) =>
				{
					return arg ? ViewStates.Visible : ViewStates.Gone;
				}));
			}
		}
		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		FloatingActionButton _addLocationFloatingActionButton;
		ListView _locationsListView;
		//
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			FindViews();

			SetBindings();

			SetCommands();
		}
		public override void OnResume()
		{
			//LocationManager locMgr = GetSystemService(Context.LocationService) as LocationManager;
			base.OnResume();
			HandleEvents();
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.FragmentLocations, container, false);
		}

		protected override void FindViews()
		{
			_addLocationFloatingActionButton = View.FindViewById<FloatingActionButton>(Resource.Id.fragmentLocationsFloatingActionButton);
			_locationsListView = View.FindViewById<ListView>(Resource.Id.fragmentLocationsLocationsListView);
		}
		protected override void HandleEvents()
		{
			//Fill Locations
			ViewModel.LoadDataCommand.Execute(null);
		}
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.User, () => User, BindingMode.OneWay));
			bindings.Add(this.SetBinding(() => ViewModel.Locations, () => Locations));
		}
		void SetCommands()
		{
			_addLocationFloatingActionButton.SetCommand("Click", ViewModel.AddLocationRedirectCommand);
		}

		View GetLocationsAdapter(int position, Location location, View convertView)
		{
			// Not reusing views here
			LayoutInflater inflater = LayoutInflater.From(Application.Context);
			convertView = inflater.Inflate(Resource.Layout.ListViewSimpleListview, null);

			TextView nameTextView = convertView.FindViewById<TextView>(Resource.Id.listViewSimpleListviewTextView);
#if DEBUG
			Console.WriteLine(location);
			Console.WriteLine("members:");
			foreach (Member member in location.Members)
			{
				Console.WriteLine(member);
			}
#endif
			nameTextView.Text = location.Name;


			convertView.SetCommand("Click", ViewModel.DetailLocationRedirectCommand, location);

			return convertView;
		}

		void SetListViewAdapter()
		{
			_locationsListView.Adapter = ViewModel.Locations.GetAdapter(GetLocationsAdapter);
		}
	}
}
