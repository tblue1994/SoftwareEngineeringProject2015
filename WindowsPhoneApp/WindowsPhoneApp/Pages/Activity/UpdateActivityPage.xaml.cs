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
    public sealed partial class UpdateActivityPage : Page
    {
        private long id;
        public UpdateActivityPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Activity original = e.Parameter as Activity;
            id = original.Id;

            BeginDate.Date = original.BeginTime - original.BeginTime.TimeOfDay;
            BeginTime.Time = original.BeginTime.TimeOfDay;
            DistanceBox.Text = original.Distance.ToString();
            StepsBox.Text = original.Steps.ToString();
            TypeBox.SelectedIndex = (int)original.Type;
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            int steps = 0;
            double distance = 0;
            if (!int.TryParse(StepsBox.Text, out steps) ||
                !double.TryParse(DistanceBox.Text, out distance))
            {
                await new MessageDialog("You need to fill in an approximate number of steps and distance.").ShowAsync();
                return;
            }

            Activity activity = new Activity
            {
                Id = id,
                AccountId = UserState.CurrentId,
                BeginTime = BeginDate.Date.DateTime + BeginTime.Time,
                EndTime = EndDate.Date.DateTime + EndTime.Time,
                Description = "<placeholder>",
                Steps = steps,
                Distance = UserState.UseOldUnits ? distance * 3.0 : distance,
                Type = (ActivityType)TypeBox.SelectedIndex
            };

            var result = await Api.Do.UpdateActivity(activity);
            activity = result;

            PageDispatch.ViewActivity(Frame, activity);
        }
    }
}
