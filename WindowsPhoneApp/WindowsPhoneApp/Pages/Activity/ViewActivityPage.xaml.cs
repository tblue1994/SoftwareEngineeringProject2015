using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WindowsPhoneApp.Models;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
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
    public sealed partial class ViewActivityPage : Page
    {
        private Activity activity;

        public ViewActivityPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.activity = (Activity)e.Parameter;
            StepCounter.Text = "Steps: " + activity.Steps;
            DurationDisplay.Text = "Duration: " + (activity.EndTime - activity.BeginTime).ToString("hh':'mm':'ss");
            TypeDisplay.Text = "Type: " + Enum.GetName(typeof(ActivityType), activity.Type);
			DistanceDisplay.Text = "Distance: " + (UserState.UseOldUnits ? Math.Round(activity.Distance / 3.0, 2) + " leagues" : activity.Distance + " miles");

            Path path = await Api.Do.GetPath(activity.Id);
            if (path != null)
            {
                Map.Center = new Geopoint(path[0]);
                Map.ZoomLevel = 15;

                MapPolyline poly = new MapPolyline();
                poly.Path = new Geopath(path.Coordinates);
                poly.StrokeColor = Color.FromArgb(255, 120, 220, 140);
                poly.StrokeThickness = 5.0;
                poly.Visible = true;

                Map.MapElements.Add(poly);
            }
        }

        private void HomeButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PageDispatch.GoHome(Frame);
        }

        private void EditButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PageDispatch.EditActivity(Frame, activity);
        }
    }
}
