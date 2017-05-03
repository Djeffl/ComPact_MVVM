using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using ComPact.Locations;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid.Locations
{
	[Activity]
	public class AddLocationActivity: BaseActivity
	{
		//Local variables
		//Color _yellowColor;
		//Color _blueColor;
		bool _isVisibleElementLocation;
		public bool IsVisibleElementLocation
		{
			get
			{
				return _isVisibleElementLocation;
			}
			set
			{
				_isVisibleElementLocation = value;
				if (_isVisibleElementLocation)
				{
					_showImageView.SetImageResource(Resource.Drawable.ic_keyboard_arrow_up_white_24dp);
				}
				else
				{
					_showImageView.SetImageResource(Resource.Drawable.ic_keyboard_arrow_down_white_24dp);
				}

			}
		}
		string _city;
		public string City
		{
			get
			{
				return _city;
			}
			set
			{
				_city = value;
				_fullAdressTextView.Text = String.Format("{0} - {1}", _city, Street);
			}
		}
		string _street;
		public string Street
		{
			get
			{
				return _street;
			}
			set
			{
				_street = value;
				_fullAdressTextView.Text = String.Format("{0} - {1}", City, _street);
			}
		}

		ObservableCollection<Member> _members = new ObservableCollection<Member>();
		public ObservableCollection<Member> Members
		{
			get
			{
				return _members;
			}
			set
			{
				_members = value;
				//Set listView
				SetMemberListView();
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
		TextInputLayout _nameTextInputLayout;
		EditText _nameEditText;
		TextView _fullAdressTextView;
		ImageView _showImageView;
		TextInputLayout _cityTextInputLayout;
		TextInputLayout _streetTextInputLayout;
		EditText _cityEditText;
		EditText _streetEditText;
		EditText _radiusEditText;
		ListView _membersListView;
		FloatingActionButton _createFloatingActionButton;
		//Bind Viewmodel to activity
		AddLocationViewModel ViewModel
		{
			get
			{
				return App.Locator.AddLocationViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityAddLocation);

			//Init elements
			FindViews();
			//init
			Init();
			//bindings
			SetBindings();
			//Use Commands
			SetCommands();
			//Set Hint visible priceEditText
			_radiusEditText.Text = null;
		}
		protected override void OnDestroy()
		{
			base.OnDestroy();
			_cityEditText.TextChanged += null;
			_streetEditText.TextChanged += null;
		}
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
			_nameTextInputLayout = FindViewById<TextInputLayout>(Resource.Id.activityAddLocationNameTextInputLayout);
			_cityEditText = FindViewById<EditText>(Resource.Id.activityAddLocationCityEditText);
			_streetEditText = FindViewById<EditText>(Resource.Id.activityAddLocationStreetEditText);
			_radiusEditText = FindViewById<EditText>(Resource.Id.activityAddLocationRadiusEditText);
			_nameEditText = FindViewById<EditText>(Resource.Id.activityAddLocationNameEditText);
			_fullAdressTextView = FindViewById<TextView>(Resource.Id.activityAddLocationFullAdressTextView);
			_showImageView = FindViewById<ImageView>(Resource.Id.activityAddLocationShowImageView);
			_cityTextInputLayout = FindViewById<TextInputLayout>(Resource.Id.activityAddLocationCityTextInputLayout);
			_streetTextInputLayout = FindViewById<TextInputLayout>(Resource.Id.activityAddLocationStreetTextInputLayout);
			_createFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityAddLocationFloatingActionButton);
			_membersListView = FindViewById<ListView>(Resource.Id.activityAddLocationMembersListView);
		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.IsVisibleElementLocation, () => _cityTextInputLayout.Visibility).ConvertSourceToTarget((arg) =>
				{
					return arg ? ViewStates.Gone : ViewStates.Visible;
				}));
			bindings.Add(this.SetBinding(() => ViewModel.IsVisibleElementLocation, () => _streetTextInputLayout.Visibility).ConvertSourceToTarget((arg) =>
				{
					return arg ? ViewStates.Gone : ViewStates.Visible;
				}));
			bindings.Add(this.SetBinding(() => ViewModel.IsVisibleElementLocation, () => IsVisibleElementLocation));
			bindings.Add(this.SetBinding(() => ViewModel.Location.Name, () => _nameEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Location.Street, () => _streetEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Location.City, () => _cityEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Location.Radius, () => _radiusEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Members, () => Members, BindingMode.OneWay));
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			_showImageView.SetCommand("Click", ViewModel.ChangeVisibilityCommand);
			_createFloatingActionButton.SetCommand("Click", ViewModel.CreateLocationCommand);
		}
		void Init()
		{
			ViewModel.LoadDataCommand.Execute(null);
			//_yellowColor = new Color(ContextCompat.GetColor(this, Resource.Color.yellow_accent_color));
			//_blueColor = new Color(ContextCompat.GetColor(this, Resource.Color.blue_main_color));

			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Add Location";

			_cityEditText.TextChanged += _cityEditText_TextChanged;
			_streetEditText.TextChanged += _streetEditText_TextChanged;
		}
		#endregion
		void _cityEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
		{
			City = e.Text.ToString();
		}

		void _streetEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
		{
			Street = e.Text.ToString();
		}
		void SetMemberListView()
		{
			_membersListView.Adapter = ViewModel.Members.GetAdapter(GetMemberAdapter);
		}
		View GetMemberAdapter(int position, Member member, View convertView)
		{
			// Not reusing views here
			convertView = LayoutInflater.Inflate(Resource.Layout.ListViewPersonCheckBox, null);

			TextView nameTextView = convertView.FindViewById<TextView>(Resource.Id.listViewPersonCheckBoxNameTextView);
			nameTextView.Text = member.FullName();
			TextView emailTextView = convertView.FindViewById<TextView>(Resource.Id.listViewPersonCheckBoxEmailTextView);
			emailTextView.Text = member.Email;
			CheckBox memberCheckbox = convertView.FindViewById<CheckBox>(Resource.Id.listViewPersonCheckBoxCheckBox);
			memberCheckbox.SetCommand("Click", ViewModel.AddMemberCommand, member);

			return convertView;
		}
	}
}
