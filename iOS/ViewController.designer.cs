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
    [Register ("ListViewController")]
    partial class ListViewController
    {
        [Outlet]
        UIKit.UIButton Button { get; set; }


        [Outlet]
        UIKit.UIImageView previewImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageCourse1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageCourse2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageCourse3 { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imageCourse1 != null) {
                imageCourse1.Dispose ();
                imageCourse1 = null;
            }

            if (imageCourse2 != null) {
                imageCourse2.Dispose ();
                imageCourse2 = null;
            }

            if (imageCourse3 != null) {
                imageCourse3.Dispose ();
                imageCourse3 = null;
            }
        }
    }
}