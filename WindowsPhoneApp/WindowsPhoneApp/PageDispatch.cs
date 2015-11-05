using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPhoneApp.Models;
using Windows.UI.Xaml.Controls;

namespace WindowsPhoneApp
{
    public static class PageDispatch
    {

        public static void ViewTeam(Frame frame, long teamId)
        {
            frame.Navigate(typeof(TeamPage), teamId);
        }

        public static void EditUser(Frame frame)
        {
            frame.Navigate(typeof(EditPage));
        }

        public static void Logout(Frame frame)
        {
            Facebook.Client.Session.ActiveSession.Logout();
            frame.Navigate(typeof(LoginPage));
        }

        public static void GoHome(Frame frame)
        {
            frame.Navigate(typeof(MainPage), UserState.CurrentId);
        }

        public static void SearchTeams(Frame frame)
        {
            frame.Navigate(typeof(TeamSearchPage));
        }

        public static void CreateTeam(Frame frame)
        {
            frame.Navigate(typeof(CreateTeamPage));
        }

        public static void EditTeam(Frame frame, long teamId)
        {
            frame.Navigate(typeof(TeamEditPage), teamId);
        }

        public static void ViewUser(Frame frame, string id)
        {
            frame.Navigate(typeof(MainPage), id);
        }

        public static void ViewActivity(Frame frame, Activity activity)
        {
            frame.Navigate(typeof(ViewActivityPage), activity);
        }

        public static void ShareStatus(Frame frame)
        {
            frame.Navigate(typeof(ShareStatusPage));
        }

        public static void FriendsWithBadge(Frame frame, long badgeId)
        {
            frame.Navigate(typeof(FriendsWithBadgePage), badgeId);
        }

        public static void Invite(Frame frame, long teamId)
        {
            frame.Navigate(typeof(InvitePage), teamId);
        }

        public static void SetGoal(Frame frame)
        {
            frame.Navigate(typeof(SetGoalPage));
        }

        public static void RecordFood(Frame frame)
        {
            frame.Navigate(typeof(RecordFoodPage));
        }

        public static void RecordManualActibity(Frame frame)
        {
            frame.Navigate(typeof(ManualActivityPage));
        }

        public static void EditActivity(Frame frame, Activity activity)
        {
            frame.Navigate(typeof(UpdateActivityPage), activity);
        }

        public static void GiveAdmin(Frame frame, long teamId)
        {
            frame.Navigate(typeof(GiveAdminPage), teamId);
        }

        public static void Challenge(Frame frame, string userId)
        {
            frame.Navigate(typeof(SetGoalPage), userId);
        }

        public static void SearchChallenge(Frame frame)
        {
            frame.Navigate(typeof(ChallengePage));
        }
    }
}
