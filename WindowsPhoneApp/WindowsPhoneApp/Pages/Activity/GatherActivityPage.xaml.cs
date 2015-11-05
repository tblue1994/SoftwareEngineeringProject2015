using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using WindowsPhoneApp.Models;
using Windows.Devices.Geolocation;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Display;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsPhoneApp.Models;
using WindowsPhoneApp.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GatherActivityPage : Page
    {
        private bool recording = false;
        private Pedometer pedometer;
        private DisplayRequest request;
        private Geolocator geolocator;
        private Path path;
        private DateTime begin;
        private DateTime current;
        private Timer timer;

        public GatherActivityPage()
        {
            this.InitializeComponent();
            pedometer = new Pedometer(Accelerometer.GetDefault());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            geolocator = new Geolocator();
            path = new Path();
            geolocator.ReportInterval = 500;
            geolocator.DesiredAccuracy = PositionAccuracy.High;
            geolocator.StatusChanged += geolocator_StatusChanged;

            request = new DisplayRequest();
            request.RequestActive();
        }

        private async void geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            if (args.Status == PositionStatus.Ready)
            {
                var position = await geolocator.GetGeopositionAsync();
                path.Update(position.Coordinate.Latitude, position.Coordinate.Longitude);
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    GPSSpinner.IsActive = false;
                    GPSSpinnerLabel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    RecordButton.IsEnabled = true;
                });
            }
        }

        private async void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            var pos = await geolocator.GetGeopositionAsync();
            path.Update(
                pos.Coordinate.Latitude,
                pos.Coordinate.Longitude);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
				if(UserState.UseOldUnits)
				{
					DistanceDisplay.Text = "Distance: " + path.Distance + " miles";
				}
				else
				{
					DistanceDisplay.Text = "Distance: " + (path.Distance/3.0) + " leagues";
				}

                Map.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Map.Center = new Geopoint(path.Last());

                MapPolyline poly = new MapPolyline();
                poly.Path = new Geopath(path.Coordinates);
                poly.StrokeColor = Color.FromArgb(255, 120, 220, 140);
                poly.StrokeThickness = 5.0;
                poly.Visible = true;

                Map.MapElements.Clear();
                Map.MapElements.Add(poly);
            });
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            request.RequestRelease();
        }

        private async void RecordButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (recording)
            {
                DateTime end = DateTime.UtcNow;

                Activity activity = new Activity
                {
                    AccountId = UserState.CurrentId,
                    BeginTime = begin,
                    EndTime = end,
                    Steps = pedometer.Steps,
                    Distance = path.Distance
                };

                GPSSpinner.IsActive = true;
                ((Button)sender).IsEnabled = false;
                geolocator.PositionChanged -= geolocator_PositionChanged;
                pedometer.Stepped -= pedometer_Stepped;
                activity.Type = await Api.Do.Predict(activity);

                activity = (await Api.Do.SendActivity(activity)).Item1;

                path.ActivityId = activity.Id;

                path = await Api.Do.PostPath(path);

                PageDispatch.ViewActivity(Frame, activity);
            }
            else
            {
                begin = DateTime.UtcNow;
                current = DateTime.UtcNow;

                timer = new Timer((o) =>
                {
                    current = current.AddSeconds(1);
                    Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => DurationDisplay.Text = "Duration: " + (current - begin).ToString("hh':'mm':'ss"));
                }, null, 1000, 1000);

                ((Button)sender).Content = "Stop Activity";

                pedometer.Start();
                begin = DateTime.UtcNow;

                pedometer.Stepped += pedometer_Stepped;
                recording = true;

                geolocator.PositionChanged += geolocator_PositionChanged;
            }
        }

        void pedometer_Stepped(object sender, int e)
        {
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                StepCounter.Text = "Steps: " + e;
            });
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            Map.ZoomLevel = 17;
        }
    }
}
