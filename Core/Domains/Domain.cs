using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;

namespace Core
{
	public class Domain
	{
		public Domain ()
		{
		}


		public async Task<Meal> UpdateItem(Meal item)
		{
            //Simulate the random response time from server
            Random rnd = new Random();
            var timeout = rnd.Next(3000,10000);
            System.Diagnostics.Debug.WriteLine(timeout);

            await Task.Delay(timeout);
            
            return item;
		}

	}
}

