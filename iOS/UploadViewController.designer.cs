// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MobileTest.iOS
{
    [Register ("UploadViewController")]
    partial class UploadViewController
    {
        [Outlet]
        UIKit.UIBarButtonItem CancelButton { get; set; }


        [Outlet]
        UIKit.UIButton NewImageButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem DoneButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView TestImagePlace { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView textBox { get; set; }

        [Action ("Cancel:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Cancel (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (CancelButton != null) {
                CancelButton.Dispose ();
                CancelButton = null;
            }

            if (DoneButton != null) {
                DoneButton.Dispose ();
                DoneButton = null;
            }

            if (NewImageButton != null) {
                NewImageButton.Dispose ();
                NewImageButton = null;
            }

            if (TestImagePlace != null) {
                TestImagePlace.Dispose ();
                TestImagePlace = null;
            }

            if (textBox != null) {
                textBox.Dispose ();
                textBox = null;
            }
        }
    }
}