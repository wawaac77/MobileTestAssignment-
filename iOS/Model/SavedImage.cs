using System;
using System.Collections.Generic;
using UIKit;

namespace MobileTest.iOS
{
	public class SavedImage
	{
		public SavedImage()
		{
		}

		static List<UIImage> savedImage1 = new List<UIImage>();

		public void AddToList (UIImage image)
		{
			savedImage1.Add(image);
		}

		public List<UIImage> GetImage ()
		{
			if (savedImage1.Count == 0) {
				return null;
			}
			return savedImage1;
		}
	}
}
