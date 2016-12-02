using System;
using System.Threading.Tasks;
using Foundation;
using Media.Plugin;
using MonoTouch.Dialog;
using UIKit;
using Xamarin.Media;
using Core;

namespace MobileTest.iOS
{
	public partial class UploadViewController : UIViewController
	{
		private MediaPicker mediaPicker = new MediaPicker();
		private UIImagePickerController mediaPickerFromLibrary = new UIImagePickerController();
		private TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
		private DisposingMediaViewController dialogController;

		private UIAlertView alertView;
		private Xamarin.Media.MediaPickerController mediaPickerController;

		public UploadViewController (IntPtr handle) : base (handle)
		{		
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			CancelButton.Clicked += closeHandler;
			NewImageButton.TouchUpInside += newIamgeHandler;

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

		void newIamgeHandler (object sender, EventArgs args)
		{
			// Create a new Alert Controller
			UIAlertController actionSheetAlert = UIAlertController.Create("New Image", "Select an image from below", UIAlertControllerStyle.ActionSheet);

			// Add Actions
			actionSheetAlert.AddAction(UIAlertAction.Create("Take Photo",UIAlertActionStyle.Default, (action) => TakePictureFunction()));

			actionSheetAlert.AddAction(UIAlertAction.Create("Choose From Library",UIAlertActionStyle.Default, (action) => ChooseFromLibraryFunction()));

			actionSheetAlert.AddAction(UIAlertAction.Create("Use Last Photo Taken",UIAlertActionStyle.Default, (action) => Console.WriteLine ("Item Three pressed.")));

			actionSheetAlert.AddAction(UIAlertAction.Create("Cancel",UIAlertActionStyle.Cancel, (action) => Console.WriteLine ("Cancel button pressed.")));

			// Required for iPad - You must specify a source for the Action Sheet since it is
			// displayed as a popover
			UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
			if (presentationPopover!=null) {
				presentationPopover.SourceView = this.View;
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
			}

			// Display the alert
			this.PresentViewController(actionSheetAlert,true,null);

		}

		private void TakePictureFunction ()
		{

			if (!mediaPicker.IsCameraAvailable) 
			{
				alertView = new UIAlertView ("MobileTest" , "Sorry, you cannot take pictures with your device",
				                             new UIAlertViewDelegate(), "OK");
				alertView.Show();

				return;
			}


			mediaPickerController = mediaPicker.GetTakePhotoUI(new StoreCameraMediaOptions
			{
				Name = "TakePicture.jpg",
				Directory = "Images"
			});

			dialogController.PresentViewController (mediaPickerController, true, null);

			mediaPickerController.GetResultAsync().ContinueWith (t => 
			{
				dialogController.DismissViewController (true, () =>
				{
					if (t.IsCanceled || t.IsFaulted) 
						return;

					MediaFile media = t.Result;
					ShowTakenPicture (media);
				});
			}, uiScheduler);

			//await UploadThroughDomain(media); 
			//I don't know how to use "UploadThroughDomain"
		}


		private void ChooseFromLibraryFunction()
		{
			
			if (!CrossMedia.Current.IsPickPhotoSupported)
			{
				alertView = new UIAlertView("SnagR Upload Photos APP", "Sorry, you cannot select pictures with your device",
											 new UIAlertViewDelegate(), "OK");
				alertView.Show();

				return;
			}


			mediaPickerFromLibrary.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			mediaPickerFromLibrary.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

			mediaPickerFromLibrary.FinishedPickingMedia += Handle_FinishedPickingMedia;
			mediaPickerFromLibrary.Canceled += Handle_Canceled;

			NavigationController.PresentModalViewController (mediaPickerFromLibrary, true);			
			/*

			var mediaOptions = new Media.Plugin.Abstractions.StoreCameraMediaOptions
			{
				Directory = "Images",
				Name = $"{DateTime.UtcNow}.jpg"
			};

			var photo = CrossMedia.Current.PickPhotoAsync();

			return;
			*/

		}

		protected void Handle_FinishedPickingMedia (object sender, UIImagePickerMediaPickedEventArgs e)
		{
			bool isImage = false;
			switch (e.Info[UIImagePickerController.MediaType].ToString()) {
				case "public.image":
					Console.WriteLine("Image selected");
					isImage = true;
				break;
					
				case "public.video":
					Console.WriteLine("Video selected");
				break;
			}

			Console.Write("Reference URL: [" + UIImagePickerController.ReferenceUrl + "]");

			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
			if (referenceURL != null) 
				Console.WriteLine(referenceURL.ToString());

			if (isImage) {
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if (originalImage != null) {
					Console.WriteLine ("got the original image");
					TestImagePlace.Image = originalImage; // display
				}

				UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
				if (editedImage != null)
				{
					Console.WriteLine("got the edited image");
					TestImagePlace.Image = editedImage;
				}

				NSDictionary imageMetadata = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;
				if (imageMetadata != null)
				{
					Console.WriteLine("got image metadata");
				}
			} else {
				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
				if (mediaURL != null)
				{
					Console.WriteLine(mediaURL.ToString());
				}
			}

			mediaPickerFromLibrary.DismissModalViewController (true); // where is Animated
		}

		protected void Handle_Canceled (object sender, EventArgs e) 
		{
			Console.WriteLine("picker cancelled");
			mediaPickerFromLibrary.DismissModalViewController (true);
		}


		private void ShowTakenPicture (MediaFile media) 
		{
			dialogController.Media = media;
			//ListViewController.imageCourse1.Image = UIImage.FromFile (media.Path);
			// I don't know how to show the photo in the ListViewController.
		}


		//Upload photo through api in domain, with loading information displayed
		protected PhotoPickerIOSLoadingOverlay loadPop = null;
		protected Domain domainUpdate = null;

		private async Task<Meal> UploadThroughDomain (MediaFile media)
		{

			var bounds = UIScreen.MainScreen.Bounds;
			loadPop = new PhotoPickerIOSLoadingOverlay(bounds);

			View.Add(loadPop); //display the loading information
			await domainUpdate.UpdateItem((Core.Meal)media); // Upload through api in domain
			loadPop.Hide(); //cancel the loading reminder

			return (Core.Meal)media;
		}

	}



	class DisposingMediaViewController : DialogViewController
	{
		public DisposingMediaViewController (RootElement root) : base (root)
		{
		}

		public MediaFile Media
		{
			get;
			set;
		}

		public override void ViewDidAppear (bool animated)
		{
			if (Media != null) {
				Media.Dispose();
				Media = null;
			}

			base.ViewDidAppear (animated);
		}
	}
}


