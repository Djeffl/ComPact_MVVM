using System;
using Android.App;
using Android.Locations;
using Android.OS;
using Android.Views;

namespace ComPact.Droid.Fragments
{
	public class MessagesFragment: BaseFragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}


		//public override void OnAttach(Android.Content.Context context)
		//{
//			base.OnAttach(Context);
//			Activity activity;

//			if (context.GetType() == typeof(Activity)){
//				activity = (Activity)context;
//			}
//			try
//			{
//				delegate = (ITaskListManager)activity;
//			}
//			catch (Java.Lang.ClassCastException ignore)
//			{
//				throw new Java.Lang.IllegalStateException("Activity " + activity + " must implement ITaskListManager");
//			}
//		}

//		@Override
//public void onDetach()
//		{
//			delegate = sDummyDelegate;
//			super.onDetach();
//		}

//		public override void OnActivityCreated(Bundle savedInstanceState)
//		{
//			base.OnActivityCreated(savedInstanceState);
//			FindViews();

//			HandleEvents();

//			//Data & services
//		}

		//public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		//{
		//	// Use this to return your custom view for this Fragment
		//	// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

		//	return inflater.Inflate(Resource.Layout.FragmentMessages, container, false);
		//}
	}
}
