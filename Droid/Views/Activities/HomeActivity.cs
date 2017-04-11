
using System;
using System.Collections.Generic;

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using ComPact.Droid.Fragments;
using ComPact.Droid.Helpers;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid.Activities
{
	[Activity(Label = "HomeActivity")]
	public class HomeActivity : BaseActivity
	{
		//Local variables
		MenuDialogService menu;
		Color _colorFilter;
		Color _resetColor = new Color(255, 255, 255);
		FragmentManager _fragmentManager;
		//Elements
		ImageView _backImageView;
		ImageView _optionsImageView;
			//Botom nav
		ImageView _tasksImageView;
		ImageView _locationsImageView;
		ImageView _messagesImageView;
		ImageView _paymentsImageView;

		Fragment _tasksFragment;
		Fragment _messagesFragment;
		Fragment _locationsFragment;
		Fragment _paymentsFragment;

		TextView _titleTextView;

		private View _helpView;
		private View _settingsView;
		//Keep track of bindings to avoid premature garbage collection
		private readonly List<Binding> bindings = new List<Binding>();
		//Bind Viewmodel to activity
		HomeViewModel ViewModel
		{
			get
			{
				return App.Locator.HomeViewModel;
			}
		}
		AssignmentsViewModel TasksViewModel
		{
			get
			{
				return App.Locator.TasksViewModel;
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityHome);
			//Init elements
			Init();
			_backImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Tasks";
			//bindings
			SetBindings();

			//Use Commands
			SetCommands();
			//Start Fragment
			_tasksImageView.SetColorFilter(_colorFilter);
			ChangeFragment(null, null, typeof(AssignmentsFragment), _tasksFragment);
			//-----
			//Ask Bruno
			menu = new MenuDialogService();
			_optionsImageView.Click += PopupMenu;
		}

		void PopupMenu(Object sender, EventArgs e)
		{
			var popup = new PopupMenu(this, _optionsImageView);
			popup.Inflate(Resource.Menu.header);


			//popup. SetCommand("MenuItemClick", ViewModel.HelpRedirectCommand);
			popup.MenuItemClick += (s1, arg1) =>
			{
				if (arg1.Item.ItemId == Resource.Id.menu_help)
				{
					ViewModel.HelpRedirectCommand.Execute(null);
				}
				else if(arg1.Item.ItemId == Resource.Id.menu_settings)
				{
					ViewModel.SettingsRedirectCommand.Execute(null);
				}
			};

			popup.Show();
		}

		public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
		{
			base.OnCreateContextMenu(menu, v, menuInfo);
		}

		/**
		 * Init Views
		 */
		void Init()
		{
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_optionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
			_helpView = FindViewById<View>(Resource.Id.menu_help);
			_settingsView = FindViewById<View>(Resource.Id.menu_settings);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);
			//botom nav
			_tasksImageView = FindViewById<ImageView>(Resource.Id.bottomNavigationTabAssignmentImageView);
			_locationsImageView = FindViewById<ImageView>(Resource.Id.bottomNavigationTabLocationImageView);
			_messagesImageView = FindViewById<ImageView>(Resource.Id.bottomNavigationTabMessageImageView);
			_paymentsImageView = FindViewById<ImageView>(Resource.Id.bottomNavigationTabPaymentImageView);

			_colorFilter = new Color(ContextCompat.GetColor(this, Resource.Color.yellow_accent_color));

			_tasksImageView.Click += (sender, e) =>
			{
				ResetColors();
				_tasksImageView.SetColorFilter(_colorFilter);
				ChangeFragment(sender, e, typeof(AssignmentsFragment), _tasksFragment);
				_titleTextView.Text = "Tasks";
			};
			_locationsImageView.Click += new EventHandler((sender, e) => 
			{
				ResetColors();
				_locationsImageView.SetColorFilter(_colorFilter);
				ChangeFragment(sender, e, typeof(LocationFragment) , _locationsFragment); 
				_titleTextView.Text = "Locations";

			});
			_messagesImageView.Click += new EventHandler((sender, e) =>
			{
				ResetColors();
				_messagesImageView.SetColorFilter(_colorFilter);
				ChangeFragment(sender, e, typeof(MessagesFragment), _messagesFragment);
				_titleTextView.Text = "Messages";
			});
			_paymentsImageView.Click += new EventHandler((sender, e) =>
			{
				ResetColors();
				_paymentsImageView.SetColorFilter(_colorFilter);
				ChangeFragment(sender, e, typeof(PaymentFragments), _paymentsFragment);
				_titleTextView.Text = "Payments";
			});
		}
		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			//_helpView.SetCommand("Click", ViewModel.HelpRedirectCommand);
			//_settingsView.SetCommand("Click", ViewModel.SettingsRedirectCommand);
		}
		void ChangeFragment(Object sender, EventArgs e, Type fragmentType ,Fragment frag)
		{
			var fragment = GetFragment(fragmentType, frag);
			if (_fragmentManager == null)
			{
				_fragmentManager = FragmentManager;
			}
			FragmentTransaction transaction = _fragmentManager.BeginTransaction();
			transaction.Replace(Resource.Id.fragmentContainer, fragment);
			transaction.AddToBackStack(null);
			transaction.Commit();


		}

		Fragment GetFragment(Type type, Fragment fragment)
		{
			//if (fragment != null)
			//{
			//	return fragment;
			//}
			//else
			//{
			if (type == typeof(AssignmentsFragment))
				{
					fragment = new AssignmentsFragment();
					return fragment;
				}
				else if (type == typeof(MessagesFragment))
				{
					fragment =  new MessagesFragment();
					return fragment;
				}
				else if (type == typeof(LocationFragment))
				{
					fragment = new LocationFragment();
					return fragment;
				}
				else if (type == typeof(PaymentFragments))
				{
					fragment = new PaymentFragments();
					return fragment;

				}
			//}
			throw new Exception("Something is wrong with your fragments!");
		}
		void ResetColors()
		{
			_paymentsImageView.SetColorFilter(_resetColor);
			_tasksImageView.SetColorFilter(_resetColor);
			_locationsImageView.SetColorFilter(_resetColor);
			_messagesImageView.SetColorFilter(_resetColor);
		}
	}
}
