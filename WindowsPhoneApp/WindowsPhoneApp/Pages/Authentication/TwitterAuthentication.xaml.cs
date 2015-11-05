using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsPhoneApp.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp.Authentication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TwitterAuthentication : Page
    {
        public TwitterAuthentication()
        {
            this.InitializeComponent();
        }

		private static HttpClient client = new HttpClient();
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			client.BaseAddress = new Uri("https://api.twitter.com");
			var request = new HttpRequestMessage(HttpMethod.Post, "/oauth/request_token");
			request.Headers.Add("Authorization", TwitterAuthenticator.GenerateAuthString("POST", "https://api.twitter.com/oauth/request_token",
				new SortedDictionary<string, string>(), "http://quarr.us"));

			var response = await client.SendAsync(request);
			if (!response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				throw new NotImplementedException();
			}
			var responseText = await response.Content.ReadAsStringAsync();

            Web.Navigate(new Uri("https://api.twitter.com/oauth/authorize?" + responseText.Split('\0').First().Split('&').First()));
            Web.NavigationStarting += Web_NavigationStarting;
        }

        private async void Web_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
		{
			var split = args.Uri.OriginalString.Split('?');
            if (split.Length > 1 && split[0] == "http://quarr.us/")
            {
				var userId = await TwitterAuthenticator.GetUserId(split[1].Split('&')[0].Split('=')[1], split[1].Split('&')[1].Split('=')[1]);
				UserState.ActiveAccount = await Api.Do.AccountTwitter(long.Parse(userId));
				if (UserState.ActiveAccount == null)
				{
					Frame.Navigate(typeof(CreatePage), new CreatePage.AutofillInfo
						{
							SocialId = long.Parse(userId),
							Authenticator = Authenticator.Twitter
						});
				}
				else
				{
					Frame.Navigate(typeof(MainPage), UserState.CurrentId);
				}
            }
        }
    }
}
