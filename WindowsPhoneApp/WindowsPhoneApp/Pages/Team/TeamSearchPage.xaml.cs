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
    public sealed partial class TeamSearchPage : Page
    {
        public TeamSearchPage()
        {
            this.InitializeComponent();
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
			Searched.Items.Clear();
            List<Team> teams = await Api.Do.SearchTeams(SearchBar.Text.Split());

            foreach (var team in teams)
            {
                Button button = new Button() { Content = team.Name, Tag = team.Id, FontSize = 30 };
                button.Tapped += button_Tapped;
                Searched.Items.Add(button);
            }
        }

        private async void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PageDispatch.ViewTeam(Frame, (long)((Button)sender).Tag);
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
