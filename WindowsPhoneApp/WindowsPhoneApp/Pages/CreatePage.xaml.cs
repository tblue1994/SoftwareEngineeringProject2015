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
	public enum Authenticator
	{
		Facebook,
		Twitter
	}

    public sealed partial class CreatePage : Page
    {
        public class AutofillInfo
        {
            public long SocialId;
            public string FullName = "";
            public string PreferredName = "";
            public bool? Gender = null;
            public DateTime Birthdate = DateTime.Now - TimeSpan.FromDays(30);
			public Authenticator Authenticator;
		}

        private AutofillInfo info;

        public CreatePage()
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
            info = (AutofillInfo)e.Parameter;

            Name.Text = info.FullName;
            PreferredName.Text = info.PreferredName;
            Birthdate.Date = info.Birthdate;
            if (info.Gender != null)
            {
                Sex.SelectedIndex = info.Gender.Value ? 1 : 0;
            }

			if(UserState.UseOldUnits)
			{
				Weight.PlaceholderText = "in stones";
			}
        }

        private async void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (new[]{Name.Text, PreferredName.Text, HeightFeet.Text, HeightInches.Text,
                Weight.Text, Zip.Text}.Any(string.IsNullOrEmpty))
            {
                MessageDialog dialog = new MessageDialog("You need to fill in all fields.");
                await dialog.ShowAsync();
                return;
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
                Id = "",
                FullName = Name.Text,
                PreferredName = PreferredName.Text,
                UserName = new string(Name.Text.Where(c => char.IsDigit(c) || char.IsLetter(c)).ToArray()),
                Height = int.Parse(HeightFeet.Text) * 12 + int.Parse(HeightInches.Text),
				Weight = UserState.UseOldUnits ? (int.Parse(Weight.Text) * 14) : int.Parse(Weight.Text),
                Sex = sex,
                Birthdate = Birthdate.Date.DateTime,
                Zip = int.Parse(Zip.Text)
            };

			if(info.Authenticator == Authenticator.Twitter)
			{
				account.TwitterId = info.SocialId;
			}
			else
			{
				account.FacebookId = info.SocialId;
			}

            account = await Api.Do.CreateAccount(account);

            UserState.ActiveAccount = account;
            PageDispatch.GoHome(Frame);
        }

		private void ChangeUnits_Click(object sender, RoutedEventArgs e)
		{
			UserState.UseOldUnits = !UserState.UseOldUnits;
			if (UserState.UseOldUnits)
			{
				ChangeUnits.Label = "Use modern english units";
			}
			else
			{
				ChangeUnits.Label = "Use ye old units";
			}
		}
    }
}
