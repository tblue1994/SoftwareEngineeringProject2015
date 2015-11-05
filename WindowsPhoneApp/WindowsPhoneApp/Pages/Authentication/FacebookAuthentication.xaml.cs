using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Facebook;
using Facebook.Client;
using WindowsPhoneApp.Common;
using WindowsPhoneApp.Models;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp.Authentication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FacebookAuthentication : Page
    {
        public FacebookAuthentication()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadingNameSpinner.IsActive = true;
            await RetriveUserInfo((AccessTokenData)e.Parameter);
        }

        private async System.Threading.Tasks.Task RetriveUserInfo(AccessTokenData accessToken)
        {
            var client = new Facebook.FacebookClient(accessToken.AccessToken);

			dynamic result = null;
			bool failed = false;
			try
			{
				result = await client.GetTaskAsync("me?fields=id,birthday,first_name,last_name,middle_name,gender");
			}
			catch(Exception e)
			{
				failed = true;
			}
			if(failed)
			{
				MessageDialog dialog = new MessageDialog("Facebook is not responding to our authentication request. Sorry.");
				await dialog.ShowAsync();

				throw new Exception("Windows phone does not provide an exit function, so we have to crash the app.");
			}
            string fullName = string.Join(" ", new[] { result.first_name, result.middle_name, result.last_name });
            string preferredName = result.last_name;
            bool? gender = null;
            if (result.gender == "female")
            {
                gender = true;
            }
            else if (result.gender == "male")
            {
                gender = false;
            }
            DateTime birthdate = DateTime.UtcNow - TimeSpan.FromDays(365 * 30);
            if (result.birthday != null)
            {
                birthdate = DateTime.Parse(result.birthday);
            }

            var currentUser = new Facebook.Client.GraphUser(result);

            long facebookId = long.Parse(result.id);

            UserState.ActiveAccount = await Api.Do.AccountFacebook(facebookId);
            if (UserState.ActiveAccount == null)
            {
                Frame.Navigate(typeof(CreatePage), new CreatePage.AutofillInfo
                    {
                        SocialId = facebookId,
						Authenticator = Authenticator.Facebook,
                        Birthdate = birthdate,
                        FullName = fullName,
                        Gender = gender,
                        PreferredName = preferredName
                    });
            }
            else
            {
                Frame.Navigate(typeof(MainPage), UserState.CurrentId);
            }
        }
    }
}
