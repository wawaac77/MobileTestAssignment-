using System.Threading.Tasks;
using Core;

namespace domain
{
	class UpdateItem : Task<Meal>
	{
		Meal mealSelectedImage;

		public UpdateItem(Meal mealSelectedImage)
		{
			this.mealSelectedImage = mealSelectedImage;
		}
	}
}