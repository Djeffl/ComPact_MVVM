using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using ComPact.ViewModel;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid.Fragments
{
	public class TasksFragment : BaseFragment
	{
		EditText _itemNameEditText;
		EditText _describtionEditText;
		Button _createTaskButton;

		TasksViewModel ViewModel
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
			FindViews();

			HandleEvents();


			//Data & services
			SetBindings();
			SetCommands();
		}

		protected override void FindViews()
		{
			_itemNameEditText = View.FindViewById<EditText>(Resource.Id.FragmentTasksNameItemTextView);
			_describtionEditText = View.FindViewById<EditText>(Resource.Id.FragmentTasksDescribtionTextView);
			_createTaskButton = View.FindViewById<Button>(Resource.Id.FragmentTasksCreateTaskButton);
		}

		void SetBindings()
		{
			this.SetBinding(() => ViewModel.ItemName, () => _itemNameEditText.Text, BindingMode.TwoWay);
			this.SetBinding(() => ViewModel.Descrition, () => _describtionEditText.Text, BindingMode.TwoWay);
		}
		void SetCommands()
		{
			_createTaskButton.SetCommand("Click", ViewModel.CreateTaskAsyncCommand);
			//ViewModel.CreateTaskAsyncCommand
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			return inflater.Inflate(Resource.Layout.FragmentTasks, container, false);
		}
	}
}
