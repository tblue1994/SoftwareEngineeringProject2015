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
    public sealed partial class GiveAdminPage : Page
    {
        private long teamId;

        public GiveAdminPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            teamId = (long)e.Parameter;
            await GetMembers();
        }

        private async Task GetMembers()
        {
            List<Account> accounts = await Api.Do.ByTeam(teamId);

            foreach (var account in accounts)
            {
                Button button = new Button() { Content = account.FullName, Tag = account.Id, FontSize = 30 };
                button.Tapped += button_Tapped;
                Members.Items.Add(button);
            }
        }

        private async void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Api.Do.GiveAdmin(UserState.CurrentId, (string)((Button)sender).Tag, teamId);
            UserState.ActiveAccount = await Api.Do.GetAccount(UserState.CurrentId);
            PageDispatch.ViewTeam(Frame, teamId);
        }
    }
}
