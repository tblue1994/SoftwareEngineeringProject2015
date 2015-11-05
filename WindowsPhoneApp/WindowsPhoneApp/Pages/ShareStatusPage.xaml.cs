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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShareStatusPage : Page
    {
        private List<Mood> moods;

        public ShareStatusPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            moods = await Api.Do.GetMoods();

            foreach (var mood in moods)
            {
                MoodOptions.Items.Add(new ComboBoxItem
                {
                    Content = new TextBlock { Text = mood.Description }
                });
            }

            Progress.IsActive = false;

            MoodOptions.IsEnabled = true;
            SubmitButton.IsEnabled = true;
        }

        private async void SubmitButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            var item = ((TextBlock)MoodOptions.SelectionBoxItem).Text;

            var mood = moods.First(m => m.Description == item);

            await Api.Do.CreateStatus(new Status
                {
                    MoodId = mood.Id,
                    Date = DateTime.UtcNow,
                    AccountId = UserState.CurrentId
                });

            PageDispatch.GoHome(Frame);
        }
    }
}
