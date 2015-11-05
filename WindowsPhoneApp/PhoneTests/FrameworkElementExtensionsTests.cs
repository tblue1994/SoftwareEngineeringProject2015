using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WindowsPhoneApp;

namespace PhoneTests
{
	[TestClass]
	public class FrameworkElementExtensionsTests
	{
		//[TestMethod]
		public void TestExpandWidth()
		{
			Mock<FrameworkElement> parent = new Mock<FrameworkElement>();
			Mock<FrameworkElement> element = new Mock<FrameworkElement>();

			bool performed = false;
			parent.SetupGet(e => e.Width).Returns(70);
			element.SetupSet(e => e.Width).Callback(d => {Assert.AreEqual(d, 7); performed = true;});
			element.SetupGet(e => e.Parent).Returns(parent.Object);

			element.Object.ExpandWidth();
			Assert.AreEqual(performed, true);
		}
	}
}
