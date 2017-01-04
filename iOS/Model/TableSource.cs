using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace MobileTest.iOS
{
	public class TableSource : UITableViewSource
	{
		private List<UIImage> TableImages = new List<UIImage>();
		String cellIdentifier = "ImagesCell";
		static SavedImage savedImage = new SavedImage();


		public TableSource(List<UIImage> images)
		{
			TableImages = images;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			TableImages = savedImage.GetImage();
			return TableImages.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier) as UITableViewCell;

			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Default,cellIdentifier);
			}

			cell.ImageView.Image = TableImages[indexPath.Row];

			return cell;
		}
	}
}
