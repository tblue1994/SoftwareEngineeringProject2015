using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace WindowsPhoneApp
{
    public static class FrameworkElementExtensions
    {
        public static void ExpandWidth(this FrameworkElement element)
        {
            var parent = element.Parent as FrameworkElement;
            if(parent == null)
            {
                throw new NotImplementedException();
            }
            element.Width = parent.ActualWidth;
        }

		public static void ExpandWidth(this FrameworkElement element, FrameworkElement parent)
		{
			if (parent == null)
			{
				throw new NotImplementedException();
			}
			element.Width = parent.ActualWidth;
		}
    }
}
