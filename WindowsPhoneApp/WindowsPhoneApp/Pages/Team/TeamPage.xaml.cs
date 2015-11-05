using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using WindowsPhoneApp.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsPhoneApp.Common;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TeamPage : Page
    {
        private long teamId;
        public TeamPage()
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
            teamId = (long)e.Parameter;
            Team team = await Api.Do.GetTeam(teamId);

            TeamName.Text = team.Name;
            UserState.ActiveAccount = await Api.Do.GetAccount(UserState.CurrentId);

            WindowsPhoneApp.Feed.FillFeed(Frame, Feed, UserState.ActiveAccount,
                await Api.Do.TeamActivities(teamId),
                await Api.Do.TeamAttainments(teamId),
                await Api.Do.TeamFoods(teamId),
                await Api.Do.TeamStatuses(teamId));

            var accountsInTeam = await Api.Do.ByTeam(teamId);
            var isIn = accountsInTeam.Where(a => a.Id == UserState.CurrentId).Any();
            JoinButton.Visibility = !isIn ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
            LeaveButton.Visibility = isIn ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
            InviteButton.Visibility = isIn ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
            if (isIn)
            {
                GiveAdmin.Visibility = (UserState.ActiveAccount.Memberships.Where(m => m.TeamId == teamId).First().Status == MembershipStatus.Admin)
                    ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
            }

            Edit.Visibility = (await Api.Do.IsAdmin(UserState.CurrentId, team.Id)) ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;

            await FillMembers();
            await FillInvited();
            await FillLeaderboard();
        }

        private async Task FillLeaderboard()
        {
            string name = (string)((ComboBoxItem)LeaderMetric.SelectedValue).Content;
            List<Tuple<Account, double>> accounts = new List<Tuple<Account, double>>();
            if (name == "Distance")
            {
                accounts = await Api.Do.LeaderboardDistance(teamId);
            }
            else if (name == "Attainments")
            {
                accounts = await Api.Do.LeaderboardAttainments(teamId);
            }

            Leaders.Items.Clear();
            foreach (var account in accounts)
            {
                Leaders.Items.Add(CreateAccount(account));
            }
        }

        private Grid CreateAccount(Tuple<Account, double> account)
        {
            Grid grid = new Grid();
            grid.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            Image image = new Image()
            {
                Width = 50,
                Height = 50,
                Source = new BitmapImage(new Uri("http://graph.facebook.com/" + account.Item1.FacebookId + "/picture"))
            };
            Grid.SetColumn(image, 0);

            TextBlock number = new TextBlock
            {
                Text = account.Item2.ToString(),
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                FontSize = 25
            };
            Grid.SetColumn(number, 1);

            TextBlock text = new TextBlock
            {
                Text = account.Item1.FullName,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right,
                FontSize = 30
            };
            Grid.SetColumn(text, 3);

            grid.Children.Add(image);
            grid.Children.Add(text);
            grid.Children.Add(number);
            return grid;
        }

        private async Task FillInvited()
        {
            List<Account> invited = await Api.Do.AccountsInvitedTo(teamId);
            if (!invited.Any())
            {
                InviteLabel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            Invited.Items.Clear();
            foreach (var account in invited)
            {
                Button button = new Button() { Content = account.FullName, Name = account.Id, FontSize = 30 };
                button.Tapped += button_Tapped;
                Invited.Items.Add(button);
            }
        }

        private async Task FillMembers()
        {
            List<Account> accounts = await Api.Do.ByTeam(teamId);
            Members.Items.Clear();
            foreach (var account in accounts)
            {
                Button button = new Button() { Content = account.FullName, Name = account.Id, FontSize = 30 };
                button.Tapped += button_Tapped;
                Members.Items.Add(button);
            }
        }

        void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PageDispatch.ViewUser(Frame, ((Button)sender).Name);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            PageDispatch.EditTeam(Frame, teamId);
        }

        private void AppBarButton_Click_Join(object sender, RoutedEventArgs e)
        {
            Api.Do.JoinTeam(UserState.CurrentId, teamId);
            JoinButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            LeaveButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            FillMembers();
            FillInvited();
            FillLeaderboard();
        }

        private async void AppBarButton_Click_Leave(object sender, RoutedEventArgs e)
        {
            var membership = (UserState.ActiveAccount.Memberships.Where(m => m.TeamId == teamId)).FirstOrDefault();
            if (membership != null &&
                membership.Status == MembershipStatus.Admin &&
                (await Api.Do.GetTeam(teamId)).Memberships.Where(m => m.Status == MembershipStatus.Member || m.Status == MembershipStatus.Admin ||
                m.Status == MembershipStatus.Invited).Count() > 1)
            {
                await new MessageDialog("You can't leave the group until you make someone else admin.").ShowAsync();
                return;
            }

            Api.Do.LeaveTeam(UserState.CurrentId, teamId);
            JoinButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            LeaveButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            PageDispatch.GoHome(Frame);
        }

        private void ClickInvite(object sender, RoutedEventArgs e)
        {
            PageDispatch.Invite(Frame, teamId);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            PageDispatch.EditTeam(Frame, teamId);
        }

        private void GiveAdmin_Click(object sender, RoutedEventArgs e)
        {
            PageDispatch.GiveAdmin(Frame, teamId);
        }

        private void HomeTapped(object sender, RoutedEventArgs e)
        {
            PageDispatch.GoHome(Frame);
        }

        private async void MetricChanged(object sender, SelectionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () => await FillLeaderboard());
        }
    }
}
