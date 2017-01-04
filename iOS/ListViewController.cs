using System;
using Core;
using UIKit;
using System.Linq;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;

namespace MobileTest.iOS
{
	public partial class ListViewController : UIViewController
	{
		private List<UIImage> uploadedImages;
		//UITableView TableView = new UITableView();
		static SavedImage savedImage = new SavedImage();

		public ListViewController (IntPtr handle) : base (handle)
		{		
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//this.imageCourse2.Image = UIImage.FromFile("Images/picture2.jpg");
			//this.imageCourse3.Image = UIImage.FromFile("Images/picture3.jpg");
		
			// Perform any additional setup after loading the view, typically from a nib.

		}

		public override void ViewDidAppear (bool animated) 
		{
			base.ViewDidAppear(animated);

			//TableView = new UITableView(View.Bounds);
			uploadedImages = savedImage.GetImage();
			//TableView.Bounds = View.Bounds;
			TableView.Source = new TableSource(uploadedImages);
			//Add(TableView);
		}


		public override void DidReceiveMemoryWarning ()
		{		
			base.DidReceiveMemoryWarning ();		
			// Release any cached data, images, etc that aren't in use.		
		}
	}
} 
