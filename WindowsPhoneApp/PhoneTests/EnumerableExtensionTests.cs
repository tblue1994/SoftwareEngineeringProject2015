using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using WindowsPhoneApp;
using Windows.UI.Xaml.Controls;
using Moq;
using Windows.Devices.Sensors;
using System.Threading.Tasks;

namespace PhoneTests
{
	[TestClass]
	public class EnumerableExtensionTests
	{
		[TestMethod]
		public void TestEmpty()
		{
			Assert.AreEqual(new[] { 1, 2, 3 }.Empty(), false);
			Assert.AreEqual(new object[] { }.Empty(), true);
		}

		public async Task<int> DoSomething()
		{
			return 0;
		}

		[TestMethod]
		public void TestSelectAwait()
		{
			CollectionAssert.AreEquivalent(new[] { DoSomething() }.SelectAwait(async a => (await a)).Result, new[] { 0 });
			CollectionAssert.AreEquivalent(new[] {
				DoSomething(),
				DoSomething(),
				DoSomething()}.SelectAwait(async a => (await a)).Result, new[] { 0, 0, 0 });
		}
	}
}
