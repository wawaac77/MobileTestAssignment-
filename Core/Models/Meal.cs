using System;
using Xamarin.Media;

namespace Core
{
	public class Meal
	{
		public String Title {get;set;}
		public String Location {get;set;}
		public DateTime CreatedDate {get;set;}
		public byte[] ImageSource {get;set;}

		public Meal ()
		{
			CreatedDate = DateTime.UtcNow;
		}

		public static explicit operator Meal(MediaFile v)
		{
			throw new NotImplementedException();
		}
	}
}

