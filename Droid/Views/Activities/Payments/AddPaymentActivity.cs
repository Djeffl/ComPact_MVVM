using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using ComPact.Models;
using ComPact.Payments;
using GalaSoft.MvvmLight.Helpers;
using Java.IO;
using Java.Text;

namespace ComPact.Droid
{
	[Activity]
	public class AddPaymentActivity : BaseActivity
	{
		//Local variables

		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		//__________________________Header______________________________
		ImageView _backImageView;
		TextView _titleTextView;
		ImageView _optionsImageView;
		//__________________________Others______________________________
		ImageView _addPictureImageView;
		EditText _whatEditText;
		EditText _priceEditText;
		EditText _detailsEditText;
		FloatingActionButton _createFloatingActionButton;
		//Bind Viewmodel to activity
		AddPaymentViewModel ViewModel
		{
			get
			{
				return App.Locator.AddPaymentViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityAddPayment);

			//Init elements
			FindViews();
			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Add Payment";
			//bindings
			SetBindings();

			//Use Commands
			SetCommands();

			//init
			Init();
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
			_addPictureImageView = FindViewById<ImageView>(Resource.Id.activityAddPaymentAddPicture);
			_whatEditText = FindViewById<EditText>(Resource.Id.activityAddPaymentWhatEditText);
			_priceEditText = FindViewById<EditText>(Resource.Id.activityAddPaymentPriceEditText);
			_detailsEditText = FindViewById<EditText>(Resource.Id.activityAddPaymentDetailsEditText);
			_createFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityAddPaymentFloatingActionButton);
		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.Payment.Name, () => _whatEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Payment.Price, () => _priceEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Payment.Description, () => _detailsEditText.Text, BindingMode.TwoWay));
		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			_createFloatingActionButton.SetCommand("Click", ViewModel.CreatePaymentCommand);
		}
		void Init()
		{
			_priceEditText.Text = null;

			_addPictureImageView.Click += BtnCamera_click;


		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			Bitmap bitmap = (Bitmap)data.Extras.Get("data");
			_addPictureImageView.SetImageBitmap(bitmap);
		}

		private void BtnCamera_click(object sender, EventArgs e)
		{
			Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);
			if (takePictureIntent.ResolveActivity(PackageManager) != null)
			{
				StartActivityForResult(takePictureIntent, 1);
			}
		}
		string mCurrentPhotoPath;

		private File CreateImageFile()
		{
			// Create an image file name
			string timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").Format(new Java.Util.Date());
			string imageFileName = "JPEG_" + timeStamp + "_";
			File storageDir = GetExternalFilesDir(Android.OS.Environment.DirectoryPictures);
			File image = File.CreateTempFile(
				imageFileName,  /* prefix */
				".jpg",         /* suffix */
				storageDir      /* directory */
			);

			// Save a file: path for use with ACTION_VIEW intents
			mCurrentPhotoPath = image.AbsolutePath;
			return image;
		}
		void TakePictureIntent()
		{
			Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);
			// Ensure that there's a camera activity to handle the intent
			if (takePictureIntent.ResolveActivity(PackageManager) != null)
			{
				// Create the File where the photo should go
				File photoFile = null;
				try
				{
					photoFile = CreateImageFile();
				}
				catch (IOException ex)
				{

				}
				// Continue only if the File was successfully created
				if (photoFile != null)
				{
					var photoURI = FileProvider.GetUriForFile(this, "com.example.android.fileprovider", photoFile);
					takePictureIntent.PutExtra(MediaStore.ExtraOutput, photoURI);

					StartActivityForResult(takePictureIntent, 1);
				}
			}
		}

		#endregion
	}
	
}
