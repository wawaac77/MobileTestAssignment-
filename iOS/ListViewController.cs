using System;
using Core;	
using UIKit;
using System.Linq;
using Foundation;

namespace MobileTest.iOS
{
	public partial class ListViewController : UIViewController
	{
	
		public ListViewController (IntPtr handle) : base (handle)
		{		
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			imageCourse1.Image = UIImage.FromFile("Images/picture1.jpg");
			imageCourse2.Image = UIImage.FromFile("Images/picture2.jpg");
			imageCourse3.Image = UIImage.FromFile("Images/picture3.jpg");

			// Perform any additional setup after loading the view, typically from a nib.


		}

		public override void DidReceiveMemoryWarning ()
		{		
			base.DidReceiveMemoryWarning ();		
			// Release any cached data, images, etc that aren't in use.		
		}
	}
} 
