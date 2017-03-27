
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;

namespace ComPact.Droid.Tasks
{
	[Activity(Label = "AddTaskActivity")]
	public class AddTaskActivity : BaseActivity
	{
		//Local variables

		//Keep track of bindings to avoid premature garbage collection
		private readonly List<Binding> bindings = new List<Binding>();
		//Elements

		//Bind Viewmodel to activity
		//ViewModel ViewModel
		//{
		//	get
		//	{
		//		return App.Locator.ViewModel;
		//	}
		//}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityAddTask);

			//Init elements
			Init();

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

		}
		#endregion
	}
}
