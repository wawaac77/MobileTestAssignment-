using System;
using System.Threading.Tasks;
using Foundation;
using MonoTouch.Dialog;
using UIKit;
using Xamarin.Media;
using Core;
using CoreGraphics;
using AssetsLibrary;
using Photos;

namespace MobileTest.iOS
{
	public partial class UploadViewController : UIViewController
	{
		public UIImage selectedImage;
		private NSData nsSelectedImage = new NSData();
		Meal mealSelectedImage; // domain defined image type

		private UIImagePickerController imagePicker = new UIImagePickerController();
		private UIAlertView alertView;
		public UIActivityIndicatorView activitySpinner = new UIActivityIndicatorView();

		public static SavedImage savedImage = new SavedImage();


		public UploadViewController (IntPtr handle) : base (handle)
		{		
		}


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			CancelButton.Clicked += closeHandler;
			DoneButton.Clicked += doneHandler;
			NewImageButton.TouchUpInside += newIamgeHandler;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		void closeHandler (object sender, EventArgs args)
		{
			if (sender == CancelButton) {
				DismissViewController (true, null);
			}
				
		}

	    void doneHandler (object sender, EventArgs args)
		{
			if (sender == DoneButton)
			{
				savedImage.AddToList(selectedImage);
				DismissViewController(true, null);
			}

		}

		private void newIamgeHandler(object sender, EventArgs args)
		{
			// Create a new Alert Controller
			UIAlertController actionSheetAlert = UIAlertController.Create("New Image", "Select an image from below", UIAlertControllerStyle.ActionSheet);

			// Add Actions
			actionSheetAlert.AddAction(UIAlertAction.Create("Take Photo", UIAlertActionStyle.Default, (action) => TakePictureFunction()));

			actionSheetAlert.AddAction(UIAlertAction.Create("Choose From Library", UIAlertActionStyle.Default, (action) => ChooseFromLibraryFunction()));

			actionSheetAlert.AddAction(UIAlertAction.Create("Use Last Photo Taken", UIAlertActionStyle.Default, (action) => LastTakenPhotoFunction()));

			actionSheetAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, (action) => Console.WriteLine("Cancel button pressed.")));

			// Required for iPad - You must specify a source for the Action Sheet since it is
			// displayed as a popover
			UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
			if (presentationPopover != null)
			{
				presentationPopover.SourceView = this.View;
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
			}

			// Display the alert
			this.PresentViewController(actionSheetAlert, true, null);
		}

		private void TakePictureFunction ()
		{

			if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera)) 
			{
				alertView = new UIAlertView ("SnagR Upload Photos APP" , "Sorry, you cannot take pictures with your device",
				                             new UIAlertViewDelegate(), "OK");
				alertView.Show();
				return;
			}


			imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
			imagePicker.CameraDevice = UIImagePickerControllerCameraDevice.Rear;
			imagePicker.CameraFlashMode = UIImagePickerControllerCameraFlashMode.Auto;

			imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;

			this.PresentModalViewController(imagePicker, true);
		}



		private void ChooseFromLibraryFunction()
		{
			if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.PhotoLibrary))
			{
				alertView = new UIAlertView("SnagR Upload Photos APP", "Sorry, you cannot select pictures with your device",
											 new UIAlertViewDelegate(), "OK");
				alertView.Show();
				return;
			}

			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

			imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;

			this.PresentModalViewController(imagePicker, true);
		}

		private void LastTakenPhotoFunction()
		{
			if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.PhotoLibrary))
			{
				alertView = new UIAlertView("SnagR Upload Photos APP", "Sorry, you cannot select pictures with your device",
				                            new UIAlertViewDelegate(), "OK");
				alertView.Show();
				return;
			}

			//imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			//ALAssetsLibrary library = new ALAssetsLibrary();
			PHFetchOptions fetchOptions = new PHFetchOptions();
			fetchOptions.SortDescriptors = new NSSortDescriptor[] {new NSSortDescriptor("creationDate", false)};
			PHFetchResult fetchResult = PHAsset.FetchAssets(PHAssetMediaType.Image,fetchOptions);
			PHAsset lastAsset = (Photos.PHAsset)fetchResult.LastObject;
			PHImageRequestOptions option = new PHImageRequestOptions();
			option.ResizeMode = PHImageRequestOptionsResizeMode.Exact;
			PHImageManager.DefaultManager.RequestImageData(lastAsset, option,(data, dataUti, orientation, info) => nsSelectedImage = data);

			//PHImageManager.DefaultManager.RequestImageForAsset(lastAsset, new CGSize(100, 100), PHImageContentMode.AspectFill, options:option,(UIImage result, NSDictionary info) => {selectedImage = result});
			selectedImage = UIImage.LoadFromData(nsSelectedImage);
			TestImagePlace.Image = selectedImage;
			//imagePicker.AssetsLibrary

			UploadProcess();	
		}


		protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			var originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
			TestImagePlace.Image = originalImage; // display
			selectedImage = originalImage;

			this.imagePicker.DismissModalViewController(true); // where is Animated

			//selectedImage.SaveToPhotosAlbum(null);

			UploadProcess();

		}

		private async void UploadProcess()
		{
			activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			activitySpinner.Frame = new CGRect(
				UIScreen.MainScreen.Bounds.Width / 2 - (activitySpinner.Frame.Width / 2),
				UIScreen.MainScreen.Bounds.Height / 2 - activitySpinner.Frame.Height - 20,
				activitySpinner.Frame.Width,
				activitySpinner.Frame.Height);
			activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
			View.Add(activitySpinner);
			activitySpinner.StartAnimating();
			textBox.Text = "Uploading the selected image... \n";

			//Uploading through Domain
			Domain domainTask = new Domain();
			mealSelectedImage = toTypeMeal(selectedImage); // change image type to a domain accpeted type
			var tempResult = await domainTask.UpdateItem(mealSelectedImage);
			if (tempResult != null)
			{
				activitySpinner.StopAnimating();
				textBox.Text = "Uploaded! \n";
			}
		}

		private void Handle_Canceled (object sender, EventArgs e) 
		{
			//Console.WriteLine("picker cancelled");
			this.imagePicker.DismissModalViewController (true);
		}

		private Meal toTypeMeal (UIImage _selectedImage) 
		{
			Meal _mealSelectedImage = new Meal ();
			_mealSelectedImage.Title = "selected";
			return _mealSelectedImage;
		}
	}

}
