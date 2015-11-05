using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WindowsPhoneApp.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsPhoneApp.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateTeamPage : Page
    {

        public CreateTeamPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length == 0)
            {
                MessageDialog dialog = new MessageDialog("You need to fill in a name.");
                await dialog.ShowAsync();
                return;
            }

            Team team = new Team()
            {
                Deleted = false,
                Name = Name.Text
            };

            var result = await Api.Do.CreateTeam(team, UserState.CurrentId);

            PageDispatch.ViewTeam(Frame, result.Id);
        }
    }
}
