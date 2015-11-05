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
using System.Net.Http;
using WindowsPhoneApp.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.Phone.UI.Input;
using Windows.UI;
using Website.Models;

namespace WindowsPhoneApp
{
    public sealed partial class MainPage : Page
    {
        private string id;

        public MainPage()
        {
            this.InitializeComponent();

            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			if(e.Parameter == null)
			{
				id = UserState.CurrentId;
			}
			else
			{
				id = (string)e.Parameter;
			}

			if (UserState.UseOldUnits)
			{
				ChangeUnits.Label = "Use modern english units";
			}
			else
			{
				ChangeUnits.Label = "Use ye old units";
			}

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    LogOut.Visibility = id == UserState.CurrentId ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
                    Edit.Visibility = id == UserState.CurrentId ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
                    Home.Visibility = id != UserState.CurrentId ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
                });

            Account account = await Api.Do.GetAccount(id);
            if (id == UserState.CurrentId)
            {
                UserState.ActiveAccount = account;
            }
            Name.Text = account.FullName;
            Profile.Source = new BitmapImage(new Uri("http://graph.facebook.com/" + account.FacebookId + "/picture"));

            CentralPivot.Title = id == UserState.CurrentId ? "You" : account.FullName;

            id = account.Id;

            var goals = await Api.Do.GetGoals(account.Id);
            WindowsPhoneApp.Feed.FillFeed(Frame, Feed, account,
                account.Activities,
                account.Attainments,
                account.FoodEaten,
                await Api.Do.GetStatuses(id),
                await Api.Do.GetReports(id),
                await Api.Do.GetMemberships(id),
                goals.Where(g => g.Status == GoalStatus.Completed));
            FillTeams(account);
            FillBadges(account);
            FillGoals(goals);

            Report report = await Api.Do.MiniReport(id);
			double distance = UserState.UseOldUnits ? report.Distance / 3.0 : report.Distance;
            Distance.Text = Math.Round(distance, 1).ToString() + (UserState.UseOldUnits ? " leagues" : " miles");
            Duration.Text = Math.Round(report.Duration / 60.0, 2).ToString() + " hours";
            Steps.Text = report.Steps.ToString();
			DistanceBar.Value = distance / 10.0 * 100.0;
			DurationBar.Value = report.Duration / 120.0 * 100.0;
			StepsBar.Value = report.Steps / 10000.0 * 100.0;
        }

        private void FillGoals(IEnumerable<Goal> goals)
        {
            var current = goals.Where(g => g.Status == GoalStatus.Current);
            foreach (var goal in current)
			{
				var view = CreateGoal(goal, Goals);
				Goals.Children.Add(view);
            }

            var challenges = goals.Where(g => g.Status == GoalStatus.Challenged);
            foreach (var goal in challenges)
			{
				var view = CreateGoal(goal, Challenges);
				Challenges.Children.Add(view);
            }
			if(challenges.Empty())
			{
				Challenges.Children.Add(new TextBlock
					{
						Text = "You haven't been challenged.",
						FontSize = 20
					});
			}

            var completed = goals.Where(g => g.Status == GoalStatus.Completed);
            foreach (var goal in completed)
            {
                var view = CreateGoal(goal, Completed);
				Completed.Children.Add(view);
            }
			if(completed.Empty())
			{
				Completed.Children.Add(new TextBlock
					{
						Text = "You haven't completed a goal.",
						FontSize = 20
					});
			}
        }

        private Grid CreateGoal(Goal goal, StackPanel view)
        {
			Grid grid = new Grid();
            grid.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            Image image = new Image()
            {
                Width = 50,
                Height = 50,
                Source = new BitmapImage(new Uri("ms-appx:///Assets/ruler.png", UriKind.Absolute))
            };
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, 0);

            var target = goal.Target;
            var unit = target.Type.AsUnit();

            double value = target.TargetNumber;
            if(target.Type == TargetType.Distance && UserState.UseOldUnits)
            {
                value /= 3.0;
                unit = "league";
            }

            TextBlock text = new TextBlock
            {
                FontSize = 20,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                Text = target.ActivityType.AsVerb() + " " + Math.Round(value, 1) + " " + unit + " " + goal.Timeline.AsPostfix()
            };
            Grid.SetRow(text, 0);
            Grid.SetColumn(text, 1);
            Grid.SetColumnSpan(text, 2);

            Button deleteButton = new Button
            {
                Content = "X",
                Width = 5,
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center
            };
			deleteButton.Tapped += async (sender, e) =>
			{
				view.Children.Remove(grid);
				await Api.Do.DeleteGoal(goal.Id);
			};
            Grid.SetRow(deleteButton, 1);
            Grid.SetColumn(deleteButton, 2);

            if(goal.Status != GoalStatus.Completed)
            {
                ProgressBar bar = new ProgressBar
                {
                    Background = new SolidColorBrush(Color.FromArgb(255, 100, 90, 90)),
                    Value = goal.Progress / goal.Target.TargetNumber * 100.0f,
                    HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch
                };
                Grid.SetRow(bar, 1);
                Grid.SetColumn(bar, 0);
                Grid.SetColumnSpan(bar, 2);
                grid.Children.Add(bar);
            }

            grid.Children.Add(image);
            grid.Children.Add(text);
            grid.Children.Add(deleteButton);
            return grid;
        }

        private async Task FillBadges(Account account)
        {
            List<Badge> earnedBadges = (await Api.Do.EarnedBadges(account.Id));
            foreach (var badge in earnedBadges)
            {
                AddBadge(badge, EarnedBadges, true);
            }
            List<Badge> unearnedBadges = (await Api.Do.UnearnedBadges(account.Id));
            foreach (var badge in unearnedBadges)
            {
                AddBadge(badge, UnearnedBadges, false);
            }
        }

        private void AddBadge(Badge badge, StackPanel list, bool achieved)
        {
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
			grid.RowDefinitions.Add(new RowDefinition());
			grid.RowDefinitions.Add(new RowDefinition());
            Image image = new Image()
            {
                Width = 50,
                Height = 50,
				Source = new BitmapImage(new Uri(achieved ? "ms-appx:///Assets/brightBell.png" : "ms-appx:///Assets/dullBell.png", UriKind.Absolute))
            };
            Grid.SetColumn(image, 0);
            Grid.SetRow(image, 0);
			Grid.SetRowSpan(image, 2);

            TextBlock text = new TextBlock
            {
                Text = badge.Title,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 25
            };
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 0);

			TextBlock description = new TextBlock
			{
				Text = badge.Description,
				TextWrapping = TextWrapping.Wrap,
				FontSize = 15
			};
			Grid.SetColumn(description, 1);
			Grid.SetRow(description, 1);

            grid.Children.Add(image);
			grid.Children.Add(text);
			grid.Children.Add(description);

            list.Children.Add(grid);
        }


        private async Task FillTeams(Account account)
        {
            List<Team> teams = (await Api.Do.TeamsByAccount(account.Id));
            foreach (var team in teams)
            {
                Button button = new Button() { Content = team.Name, Name = team.Id.ToString(), FontSize = 30 };
                button.Tapped += TeamTapped;
                Teams.Items.Add(button);
                button.Width = TeamsGrid.Width;
            }

            List<Team> invitedTo = (await Api.Do.TeamsInvitedTo(account.Id));
            if (!invitedTo.Any())
            {
                InvitationsLabel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            foreach (var team in invitedTo)
            {
                Button button = new Button() { Content = team.Name, Name = team.Id.ToString(), FontSize = 30 };
                button.Tapped += TeamTapped;
                Invitations.Items.Add(button);
                button.Width = TeamsGrid.Width;
            }
        }

        private void TeamTapped(object sender, TappedRoutedEventArgs e)
        {
            long teamId = long.Parse(((Button)sender).Name);
            PageDispatch.ViewTeam(Frame, teamId);
        }

        private void EditTapped(object sender, RoutedEventArgs e)
        {
            PageDispatch.EditUser(Frame);
        }

        private void LogoutTapped(object sender, RoutedEventArgs e)
        {
            PageDispatch.Logout(Frame);
        }

        private void HomeTapped(object sender, RoutedEventArgs e)
        {
            PageDispatch.GoHome(Frame);
        }

        private void FindTapped(object sender, RoutedEventArgs e)
        {
            PageDispatch.SearchTeams(Frame);
        }

        private void CreateTeamTapped(object sender, RoutedEventArgs e)
        {
            PageDispatch.CreateTeam(Frame);
        }

        private void StatusTapped(object sender, RoutedEventArgs e)
        {
            PageDispatch.ShareStatus(Frame);
        }

        private void SetGoalTapped(object sender, RoutedEventArgs e)
        {
            PageDispatch.SetGoal(Frame);
        }

        private void StartActivityTapped(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GatherActivityPage));
        }

        /*private void Pivot_PivotItemLoaded(Pivot sender, PivotItemEventArgs args)
        {
            if ((string)args.Item.Header == "Profile")
            {
                LogOut.Visibility = id == UserState.CurrentId ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
                Edit.Visibility = id == UserState.CurrentId ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
                Home.Visibility = id == UserState.CurrentId ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;

                Find.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Create.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                LogOut.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Edit.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Home.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                Find.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Create.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }*/

        private async void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog("Are you sure you want to delete your account? This cannot be undone.");
            dialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;
            dialog.Commands.Clear();
            dialog.Commands.Add(new UICommand("Delete", DoDeleteAccount));
            dialog.Commands.Add(new UICommand("Cancel"));
            dialog.CancelCommandIndex = 1;
            await dialog.ShowAsync();
        }

        private void DoDeleteAccount(IUICommand command)
        {
            Api.Do.DeleteAccount(UserState.CurrentId);
            LogoutTapped(null, null);
        }

        private void EatFood_Click(object sender, RoutedEventArgs e)
        {
            PageDispatch.RecordFood(Frame);
        }

        private void ManualActivity_Click(object sender, RoutedEventArgs e)
        {
            PageDispatch.RecordManualActibity(Frame);
        }

        private void ChallengeClick(object sender, RoutedEventArgs e)
        {
            PageDispatch.SearchChallenge(Frame);
        }

		private void CentralPivot_PivotItemLoading(Pivot sender, PivotItemEventArgs args)
		{
			/*
			<AppBarButton Name="Create" Label="Create team" Visibility="Collapsed" Click="CreateTeamTapped"/>
			<AppBarButton Name="Status" Icon="Emoji2" Label="Share mood" Visibility="Visible" Click="StatusTapped"/>
			<AppBarButton Name="EatFood" Label="Track food" Visibility="Visible" Click="EatFood_Click"/>
			<AppBarButton Name="SetGoal" Label="Set a goal" Visibility="Collapsed" Click="SetGoalTapped"/>
			<AppBarButton Name="Challenge" Label="Challenge" Visibility="Collapsed" Click="ChallengeClick"/>
			<AppBarButton Name="ManualActivity" Label="Manually enter activity" Visibility="Visible" Click="ManualActivity_Click"/>
			<AppBarButton Name="DeleteAccount" Label*/
			if ((string)args.Item.Header == "Feed")
			{
				Create.Visibility = Visibility.Collapsed;
				Status.Visibility = Visibility.Visible;
				EatFood.Visibility = Visibility.Visible;
				SetGoal.Visibility = Visibility.Collapsed;
				Challenge.Visibility = Visibility.Collapsed;
				ManualActivity.Visibility = Visibility.Visible;
			}
			else if((string)args.Item.Header == "Herds")
			{
				Create.Visibility = Visibility.Visible;
				Status.Visibility = Visibility.Collapsed;
				EatFood.Visibility = Visibility.Collapsed;
				SetGoal.Visibility = Visibility.Collapsed;
				Challenge.Visibility = Visibility.Collapsed;
				ManualActivity.Visibility = Visibility.Collapsed;
			}
			else if ((string)args.Item.Header == "Goals")
			{
				Create.Visibility = Visibility.Collapsed;
				Status.Visibility = Visibility.Collapsed;
				EatFood.Visibility = Visibility.Collapsed;
				SetGoal.Visibility = Visibility.Visible;
				Challenge.Visibility = Visibility.Visible;
				ManualActivity.Visibility = Visibility.Collapsed;
			}
			else if ((string)args.Item.Header == "Badges")
			{
				Create.Visibility = Visibility.Collapsed;
				Status.Visibility = Visibility.Collapsed;
				EatFood.Visibility = Visibility.Collapsed;
				SetGoal.Visibility = Visibility.Collapsed;
				Challenge.Visibility = Visibility.Collapsed;
				ManualActivity.Visibility = Visibility.Collapsed;
			}
		}

		private void ChangeUnits_Click(object sender, RoutedEventArgs e)
		{
			UserState.UseOldUnits = !UserState.UseOldUnits;
			if(UserState.UseOldUnits)
			{
				ChangeUnits.Label = "Use modern english units";
			}
			else
			{
				ChangeUnits.Label = "Use ye old units";
			}
            PageDispatch.GoHome(Frame);
		}
    }
}
