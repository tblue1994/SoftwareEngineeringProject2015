using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WindowsPhoneApp.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    public sealed partial class EditPage : Page
    {

        public EditPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Account account = UserState.ActiveAccount;
            Name.Text += account.FullName;
            HeightFeet.Text += account.Height / 12;
            HeightInches.Text += account.Height % 12;
            PreferredName.Text = account.PreferredName;
            Birthdate.Date = account.Birthdate.Date;
			Zip.Text += account.Zip;
			if (UserState.UseOldUnits)
			{
				Weight.PlaceholderText = "in stones";
				Weight.Text += (account.Weight / 14).ToString();
			}
			else
			{
				Weight.Text += account.Weight.ToString();
			}
        }

        private async void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (new[]{Name.Text, PreferredName.Text, HeightFeet.Text, HeightInches.Text,
                Weight.Text, Zip.Text}.Any(string.IsNullOrEmpty))
            {
                MessageDialog dialog = new MessageDialog("You need to fill in all fields.");
            }

            bool? sex = null;
            if (Sex.SelectionBoxItem == null)
            {

            }
            else if ((string)Sex.SelectionBoxItem == "Female")
            {
                sex = true;
            }
            else if ((string)Sex.SelectionBoxItem == "Male")
            {
                sex = false;
            }

            var account = new Account()
            {
                Id = UserState.ActiveAccount.Id,
                FullName = Name.Text,
                UserName = new string(Name.Text.Where(c => char.IsDigit(c) || char.IsLetter(c)).ToArray()),
                PreferredName = PreferredName.Text,
                FacebookId = UserState.ActiveAccount.FacebookId,
                TwitterId = UserState.ActiveAccount.TwitterId,
                Height = int.Parse(HeightFeet.Text) * 12 + int.Parse(HeightInches.Text),
                Weight = UserState.UseOldUnits ? int.Parse(Weight.Text)  * 14 : int.Parse(Weight.Text),
                Sex = sex,
                Birthdate = Birthdate.Date.DateTime,
                Zip = int.Parse(Zip.Text)
            };

            account = await Api.Do.EditAccount(account);

            PageDispatch.GoHome(Frame);
        }
    }
}
