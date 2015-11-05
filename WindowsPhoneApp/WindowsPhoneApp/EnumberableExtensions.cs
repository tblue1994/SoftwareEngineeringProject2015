using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhoneApp
{
    public static class EnumberableExtensions
    {
        public static async Task<List<Y>> SelectAwait<T, Y>(this IEnumerable<T> self, Func<T, Task<Y>> f)
        {
            List<Y> output = new List<Y>();
            foreach (var item in self)
            {
                output.Add(await f(item));
            }
            return output;
        }

		public static bool Empty<T>(this IEnumerable<T> self)
		{
			return !self.Any();
		}
    }
}
