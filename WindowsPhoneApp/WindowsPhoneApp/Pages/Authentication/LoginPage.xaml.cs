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
using Facebook.Client.Controls;
using WindowsPhoneApp.Common;
using WindowsPhoneApp.Authentication;
using System.Threading;
using Windows.UI.Core;
using Windows.Networking.Connectivity;
using System.Net.NetworkInformation;


namespace WindowsPhoneApp
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
			Session.OnFacebookAuthenticationFinished += OnFacebookAuthenticationFinished;
			LoginButton.SessionStateChanged += OnSessionStateChanged;
        }

        private void OnFacebookAuthenticationFinished(AccessTokenData session)
        {
            Frame.Navigate(typeof(FacebookAuthentication), session);
        }

        private void OnSessionStateChanged(object sender, SessionStateChangedEventArgs e)
        {
            if (e.SessionState == FacebookSessionState.Opened)
            {
                Frame.Navigate(typeof(FacebookAuthentication), Session.ActiveSession.CurrentAccessTokenData);
            }
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(TwitterAuthentication));
        }
    }
}
