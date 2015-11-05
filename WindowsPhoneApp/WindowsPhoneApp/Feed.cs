using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using WindowsPhoneApp.Common;
using WindowsPhoneApp.Models;

namespace WindowsPhoneApp
{
    public class Feed
    {
        private static Frame frame;

        public static async Task FillFeed(Frame frame, StackPanel view, Account account, params IEnumerable<IFeedable>[] feeds)
        {
            Feed.frame = frame;
            List<IFeedable> sorted = new List<IFeedable>();
            foreach (var item in feeds.SelectMany(x => x))
            {
                sorted.Add(item);
            }
            sorted.Sort(new IFeedableComparer());

            for (int i = 0; i < sorted.Count; ++i )
            {
                var item = sorted[i];
                if (item is Attainment)
                {
                    FillAttainment(view, item as Attainment);
                }
                else if (item is Activity)
                {
                    FillActivity(view, account, ((Activity)item));
                }
                else if (item is Food)
                {
                    FillFood(view, account, ((Food)item));
                }
                else if (item is Status)
                {
                    await FillStatus(view, account, ((Status)item));
                }
                else if (item is Goal)
                {
                    FillGoal(view, ((Goal)item));
                }
                else if (item is Report)
                {
                    FillReport(view, ((Report)item));
                }
                else if (item is Membership)
                {
                    await FillMembership(view, ((Membership)item));
                }
            }
        }

        private static async Task FillMembership(StackPanel view, Membership membership)
        {
            Grid message = new Grid();
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Pixel) });
            message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			message.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			message.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Pixel) });

            Image image = new Image
            {
                Width = 50,
                Height = 50,
                Source = new BitmapImage(new Uri("ms-appx:///Assets/cow.png", UriKind.Absolute))
            };

			Grid.SetRow(image, 0);
            Grid.SetColumn(image, 0);

            Team team = await Api.Do.GetTeam(membership.TeamId);
            string descriptor = "";
            if (membership.Status == MembershipStatus.Invited)
            {
                descriptor += "Invited to " + team.Name;
            }
            else if (membership.Status == MembershipStatus.Admin)
            {
                descriptor += "Created " + team.Name;
            }
            else if (membership.Status == MembershipStatus.Member)
            {
                descriptor += "Joined " + team.Name;
            }
            else if (membership.Status == MembershipStatus.Left)
            {
                descriptor += "Left " + team.Name;
            }
            TextBlock text = new TextBlock
            {
                FontSize = 20,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                Text = descriptor
            };
			Grid.SetColumn(text, 2);
			Grid.SetRow(text, 0);

            message.Children.Add(image);
            message.Children.Add(text);

            view.Children.Add(message);
        }

        private static void FillReport(StackPanel view, Report report)
        {
            Grid message = new Grid();
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Pixel) });
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			message.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			message.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Pixel) });

            Image image = new Image
            {
				Width = 50,
				Height =50,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx:///Assets/clipboard.png", UriKind.Absolute))
            };

			Grid.SetRow(image, 0);
            Grid.SetColumn(image, 0);

            StackPanel rightpanel = new StackPanel();
			Grid.SetColumn(rightpanel, 2);
			Grid.SetRow(rightpanel, 0);
            TextBlock text = new TextBlock
            {
                FontSize = 15,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                Text = "Distance: " + Math.Round(UserState.UseOldUnits ? report.Distance / 3.0 : report.Distance, 1) + 
                    (UserState.UseOldUnits ? " leagues" : " miles")
            };
            rightpanel.Children.Add(text);
            text = new TextBlock
            {
                FontSize = 15,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                Text = "Duration: " + Math.Round(report.Distance / 60.0, 2) + " hours"
            };
            rightpanel.Children.Add(text);
            text = new TextBlock
            {
                FontSize = 15,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                Text = "Steps: " + report.Steps
            };
            rightpanel.Children.Add(text);

            message.Children.Add(image);
            message.Children.Add(rightpanel);

            view.Children.Add(message);
        }

        private static void FillGoal(StackPanel view, Goal goal)
        {
            Grid message = new Grid();
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Pixel) });
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			message.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			message.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Pixel) });

            Image image = new Image
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/ruler.png", UriKind.Absolute)),
				Width = 50,
				Height = 50
            };

			Grid.SetRow(image, 0);
            Grid.SetColumn(image, 0);

            var target = goal.Target;
            var unit = target.Type.AsUnit();

            double value = target.TargetNumber;
            if (target.Type == TargetType.Distance && UserState.UseOldUnits)
            {
                value /= 3.0;
                unit = "league";
            }

            TextBlock text = new TextBlock
            {
                FontSize = 20,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                Text = target.ActivityType.AsVerb() + " " + Math.Round(value, 2) + " " + unit + " " + goal.Timeline.AsPostfix()
			};

			Grid.SetRow(image, 0);
            Grid.SetColumn(text, 2);

            message.Children.Add(image);
            message.Children.Add(text);

            view.Children.Add(message);
        }

        private static void FillAttainment(StackPanel view, Attainment attainment)
        {
            Grid message = new Grid();
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Pixel) });
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			message.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			message.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Pixel) });

            Image image = new Image
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/brightBell.png", UriKind.Absolute)),
				Width = 50,
				Height = 50
            };

			Grid.SetRow(image, 0);
            Grid.SetColumn(image, 0);

            TextBlock text = new TextBlock
            {
                Text = "Got a badge",
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                FontSize = 20
            };

			Grid.SetRow(image, 0);
            Grid.SetColumn(text, 2);

            message.Tapped += message_Tapped;
            message.Tag = attainment.BadgeId;

            message.Children.Add(image);
            message.Children.Add(text);

            view.Children.Add(message);
        }

        static void message_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PageDispatch.FriendsWithBadge(frame, (long)((Grid)sender).Tag);
        }

        private static async Task FillStatus(StackPanel view, Account account, Models.Status status)
        {
            Grid message = new Grid();
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Pixel) });
            message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			message.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			message.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Pixel) });

            Image image = new Image
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/smilie.png", UriKind.Absolute)),
				Width = 50,
				Height = 50,
            };

			Grid.SetColumn(image, 0);
			Grid.SetRow(image, 0);

            TextBlock text = new TextBlock
            {
                Text = "Was " + await Api.Do.MoodDescription(status.MoodId),
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                FontSize = 20
            };
			Grid.SetColumn(text, 2);
			Grid.SetRow(text, 0);

            message.Children.Add(image);
            message.Children.Add(text);

            view.Children.Add(message);
        }


        private static void FillFood(StackPanel view, Account account, Food food)
        {
            Grid message = new Grid();
            message.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto
            });
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Pixel) });
            message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			message.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			message.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Pixel) });

            Image image = new Image
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/food.png", UriKind.Absolute)),
				Width = 50,
				Height = 50
            };

            Grid.SetColumn(image, 0);
			Grid.SetRow(image, 0);

            TextBlock text = new TextBlock
            {
                Text = "Ate " + food.Amount + " " + food.Measurement.InSentence(food.Amount != 1) + " of " + food.FoodName,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                FontSize = 20
            };
			Grid.SetColumn(text, 2);
			Grid.SetRow(image, 0);

            message.Children.Add(image);
            message.Children.Add(text);

            view.Children.Add(message);
        }

        private static void FillActivity(StackPanel view, Account account, Activity activity)
        {
            Grid message = new Grid();
            message.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
			message.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			message.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			message.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Pixel) });

            Image image = new Image
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/running.png", UriKind.Absolute)),
				Width = 50,
				Height = 50
            };

			Grid.SetRow(image, 0);
            Grid.SetColumn(image, 0);

            TextBlock text = new TextBlock
            {
                Text = activity.Describe(account.PreferredName),
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 20
            };

			Grid.SetRow(text, 0);
            Grid.SetColumn(text, 2);

            message.Children.Add(image);
            message.Children.Add(text);

            message.Name = activity.Id.ToString() + " activity";
            message.Tapped += ActivityTapped;

            view.Children.Add(message);
        }


        private async static void ActivityTapped(object sender, TappedRoutedEventArgs e)
        {
            PageDispatch.ViewActivity(frame, await Api.Do.GetActivity(long.Parse(((Grid)sender).Name.Split()[0])));
        }
    }
}
