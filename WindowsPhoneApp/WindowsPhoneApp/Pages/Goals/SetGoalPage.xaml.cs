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
using WindowsPhoneApp.Models;
using Windows.UI.Popups;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SetGoalPage : Page
    {
        public SetGoalPage()
        {
            this.InitializeComponent();
        }
        
        private Account challenged;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            challenged = await Api.Do.GetAccount(e.Parameter as String ?? "");
            if (challenged == null)
            {
                Title.Text = "Set a goal";
            }
            else
            {
                Title.Text = "Challenge " + challenged.PreferredName;
            }
        }

        private async void Submit(object sender, TappedRoutedEventArgs e)
        {

            int number;
            if (MeasureBox.SelectedValue == null ||
                TimelineBox.SelectedValue == null ||
                TypeBox.SelectedValue == null ||
                !int.TryParse(NumberBox.Text, out number) ||
                number <= 0)
            {
                MessageDialog dialog = new MessageDialog("Your count must be a natural number and all fields must be filled in.");
                await dialog.ShowAsync();
                return;
            }

            Target target = new Target
            {
                TargetNumber = (UIType() == TargetType.Distance && UserState.UseOldUnits) ? number/3.0 : number,
                ActivityType = UIActivityType(),
                Type = UIType()
            };

            target = await Api.Do.PostTarget(target);

            Goal goal = new Goal
            {
                TargetId = target.Id,
                AccountId = challenged != null ? challenged.Id : UserState.CurrentId,
                Timeline = UITimeline(),
                BeginDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow + UITimeline().Span(),
                Status = challenged != null ? GoalStatus.Challenged : GoalStatus.Current,
            };

            await Api.Do.PostGoal(goal);
            goal.Target = target;
            PageDispatch.GoHome(Frame);
        }

        private TargetType UIType()
        {
            var value = (string)((ComboBoxItem)MeasureBox.SelectedValue).Tag;
            return EnumValue<TargetType>(value);
        }

        private TargetTimeline UITimeline()
        {
            var value = (string)((ComboBoxItem)TimelineBox.SelectedValue).Tag;
            return EnumValue<TargetTimeline>(value);
        }

        private TargetActivityType UIActivityType()
        {
            var value = (string)((ComboBoxItem)TypeBox.SelectedValue).Tag;
            return EnumValue<TargetActivityType>(value);
        }

        private static T EnumValue<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
