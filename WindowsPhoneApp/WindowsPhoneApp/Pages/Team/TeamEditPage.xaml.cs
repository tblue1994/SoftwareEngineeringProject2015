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
    public sealed partial class TeamEditPage : Page
    {
        private long id;

        public TeamEditPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            id = (long)e.Parameter;

            Team team = await Api.Do.GetTeam(id);
            Name.Text += team.Name;
        }

        private async void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length == 0)
            {
                MessageDialog dialog = new MessageDialog("You need to fill in a name.");
            }

            Team team = new Team()
            {
                Id = id,
                Deleted = false,
                Name = Name.Text
            };

            await Api.Do.EditTeam(team);

            PageDispatch.ViewTeam(Frame, id);
        }
    }
}
