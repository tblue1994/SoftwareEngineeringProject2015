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
using System.Threading.Tasks;
using WindowsPhoneApp.Models;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InvitePage : Page
    {
        private long teamId;

        public InvitePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            teamId = (long)e.Parameter;
        }

        private async void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                await DoSearch();
            }
        }

        private async Task DoSearch()
		{
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                Searched.Items.Clear();
                List<Account> accounts = await Api.Do.SearchAccounts(SearchBar.Text.Split());

                foreach (var account in accounts)
                {
                    Button button = new Button() { Content = account.FullName, Tag = account.Id, FontSize = 30 };
                    button.Tapped += button_Tapped;
                    Searched.Items.Add(button);
                }
            });
        }

        private async void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Api.Do.Invite(UserState.CurrentId, (string)((Button)sender).Tag, teamId);
            PageDispatch.ViewTeam(Frame, teamId);
        }

		private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
		{
			if(((TextBox)sender).Text.Length >= 2)
			{
				await DoSearch();
			}
		}
    }
}
