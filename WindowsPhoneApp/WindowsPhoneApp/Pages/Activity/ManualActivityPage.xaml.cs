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
using Windows.UI.Popups;
using WindowsPhoneApp.Models;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp
{
    public sealed partial class ManualActivityPage : Page
    {
        public ManualActivityPage()
        {
            this.InitializeComponent();
        }

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			if(UserState.UseOldUnits)
			{
				DistanceBox.PlaceholderText = "in leagues";
			}
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
                AccountId = UserState.CurrentId,
                BeginTime = BeginDate.Date.DateTime.Date.ToUniversalTime() + BeginTime.Time + TimeSpan.FromSeconds(55),
                EndTime = EndDate.Date.DateTime.Date.ToUniversalTime() + EndTime.Time + TimeSpan.FromSeconds(55),
                Description = "<placeholder>",
                Steps = steps,
                Distance = UserState.UseOldUnits ? distance * 3.0 : distance,
                Type = (ActivityType)TypeBox.SelectedIndex
            };

            var result = await Api.Do.SendActivity(activity);
            activity = result.Item1;

            PageDispatch.ViewActivity(Frame, activity);
        }
    }
}
