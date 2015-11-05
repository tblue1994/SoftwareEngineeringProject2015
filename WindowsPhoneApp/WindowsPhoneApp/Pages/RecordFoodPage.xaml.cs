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

namespace WindowsPhoneApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecordFoodPage : Page
    {
        public RecordFoodPage()
        {
            this.InitializeComponent();
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            double amount = 0;
            if (string.IsNullOrEmpty(NameInput.Text) ||
                string.IsNullOrEmpty(Number.Text) ||
                !double.TryParse(Number.Text, out amount))
            {
                MessageDialog dialog = new MessageDialog("You need to fill in all fields.");
                await dialog.ShowAsync();
                return;
            }

            Food food = new Food
            {
                AccountId = UserState.CurrentId,
                Amount = amount,
                Date = DateTime.UtcNow,
                FoodName = NameInput.Text,
                Measurement = (Measurement)Enum.Parse(typeof(Measurement), (string)((ComboBoxItem)Units.SelectedValue).Tag)
            };
            await Api.Do.PostFood(food);
            PageDispatch.GoHome(Frame);
        }
    }
}
