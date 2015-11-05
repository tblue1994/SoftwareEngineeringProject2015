using WindowsPhoneApp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class FriendsWithBadgePage : Page
	{
		public FriendsWithBadgePage()
		{
			this.InitializeComponent();
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			foreach (var friend in await Api.Do.FriendsWithBadge(UserState.CurrentId, (long)e.Parameter))
			{
				var button = new Button
					{
						Name = friend.Id,
						Content = new TextBlock
						{
							Text = friend.FullName,
							FontSize = 25
						}
					};
				button.Tapped += button_Tapped;
				Friends.Items.Add(button);
			}

		}

		void button_Tapped(object sender, TappedRoutedEventArgs e)
		{
			PageDispatch.ViewUser(Frame, ((Button)sender).Name);
		}
	}
}
