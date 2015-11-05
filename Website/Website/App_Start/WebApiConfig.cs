using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Website.BusinessLogic;
using Website.Models;

namespace Website
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "teammates",
                routeTemplate: "api/Account/GetTeammateWithBadges/{badgeId}/{userId}",
                defaults: new { controller = "Account", action = "GetTeammateWithBadges" }
            );
            
            config.Routes.MapHttpRoute(
                name: "AccountFacebook",
                routeTemplate: "api/Account/Facebook/{facebookId}",
                defaults: new { controller = "Account", action = "Facebook" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiSearch",
                routeTemplate: "api/{controller}/Search/{key}"
            );

            config.Routes.MapHttpRoute(
                name: "AccountTwitter",
                routeTemplate: "api/Account/Twitter/{twitterId}",
                defaults: new { controller = "Account", action = "Twitter" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "MembershipApi3",
                routeTemplate: "api/{controller}/{action}/{teamId}/{toId}/{fromId}"
            );
            config.Routes.MapHttpRoute(
                name: "MembershipApi2",
                routeTemplate: "api/{controller}/{action}/{teamId}/{userId}"
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithId",
                routeTemplate: "api/{controller}/{action}/{id}"
            );


            /*
            #region Account
            config.Routes.MapHttpRoute(
                name: "CreateAccountApi",
                routeTemplate: "api/Account/Create",
                defaults: new { controller = "Account", action = "Create" }
            );

            config.Routes.MapHttpRoute(
                name: "ModifyAccountApi",
                routeTemplate: "api/Account/Modify",
                defaults: new { controller = "Account", action = "Modify" }
            );

            config.Routes.MapHttpRoute(
                name: "GetAccountApi",
                routeTemplate: "api/Account/Get/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "Account", action = "Get" }
            );

            config.Routes.MapHttpRoute(
                name: "SearchAccountApi",
                routeTemplate: "api/Account/Search/{key}",
                defaults: new { key = RouteParameter.Optional, controller = "Account", action = "Search" }
            );

            config.Routes.MapHttpRoute(
                name: "GetAccountFacebook",
                routeTemplate: "api/Account/Facebook/{facebookId}",
                defaults: new { id = RouteParameter.Optional, controller = "Account", action = "Facebook" }
            );
            #endregion

            #region Membership
            config.Routes.MapHttpRoute(
                name: "GetMembershipApi",
                routeTemplate: "api/Membership/{action}/{id}",
                defaults: new { controller = "Membership" }
            );

            config.Routes.MapHttpRoute(
                name: "ChangeStatusNoToApi",
                routeTemplate: "api/Membership/{action}/{teamId}/{userId}",
                defaults: new { controller = "Membership" }
            );

            config.Routes.MapHttpRoute(
                name: "ChangeStatusApi",
                routeTemplate: "api/Membership/{action}/{teamId}/{fromId}/{toId}",
                defaults: new { controller = "Membership" }
            );
            #endregion

            #region Team
            config.Routes.MapHttpRoute(
                name: "GetTeamApi",
                routeTemplate: "api/Team/Get/{id}",
                defaults: new { controller = "Team", action = "Get" }
            );

            config.Routes.MapHttpRoute(
                name: "GetAllTeamApi",
                routeTemplate: "api/Team/All",
                defaults: new { controller = "Team", action = "All" }
            );

            config.Routes.MapHttpRoute(
                name: "SearchTeamApi",
                routeTemplate: "api/Team/Search",
                defaults: new { controller = "Team", action = "Search" }
            );

            config.Routes.MapHttpRoute(
                name: "DeleteTeamApi",
                routeTemplate: "api/Team/Delete/{id}",
                defaults: new { controller = "Team", action = "Delete" }
            );

            config.Routes.MapHttpRoute(
                name: "CreateTeamApi",
                routeTemplate: "api/Team/Create/{userId}",
                defaults: new { controller = "Team", action = "Create" }
            );

            config.Routes.MapHttpRoute(
                name: "ModifyTeamApi",
                routeTemplate: "api/Team/Modify",
                defaults: new { controller = "Team", action = "Modify" }
            );
            #endregion

            #region Activity
            config.Routes.MapHttpRoute(
                name: "GetActivityApiWithId",
                routeTemplate: "api/Activity/{action}/{id}",
                defaults: new { controller = "Activity" }
            );

            config.Routes.MapHttpRoute(
                name: "DeleteActivityNoId",
                routeTemplate: "api/Activity/{action}",
                defaults: new { controller = "Activity" }
            );
            #endregion

            #region Goal
            config.Routes.MapHttpRoute(
                name: "GoalApiWithActId",
                routeTemplate: "api/Goal/UpdateGoals/{activityId}",
                defaults: new { activityId = RouteParameter.Optional, controller = "Goal", action = "UpdateGoals" }
            );
            config.Routes.MapHttpRoute(
                name: "GetGoalApiWithId",
                routeTemplate: "api/Goal/{action}/{id}",
                defaults: new { controller = "Goal" }
            );

            config.Routes.MapHttpRoute(
                name: "DeleteGoalNoId",
                routeTemplate: "api/Goal/{action}",
                defaults: new { controller = "Goal" }
            );

            
            #endregion

            #region Target
            config.Routes.MapHttpRoute(
                name: "TargetApiWithId",
                routeTemplate: "api/Target/{action}/{id}",
                defaults: new { controller = "Target" }
            );

            config.Routes.MapHttpRoute(
                name: "TargetNoId",
                routeTemplate: "api/Target/{action}",
                defaults: new { controller = "Target" }
            );
            #endregion

            #region Attainment
            config.Routes.MapHttpRoute(
                name: "AttainmentApiWithActId",
                routeTemplate: "api/Attainment/UpdateAttainments/{activityId}",
                defaults: new { controller = "Attainment", action = "UpdateAttainments" }
            );
            
            config.Routes.MapHttpRoute(
                name: "AttainmentApiWithId",
                routeTemplate: "api/Attainment/{action}/{id}",
                defaults: new { controller = "Attainment" }
            );

            config.Routes.MapHttpRoute(
                name: "AttainmentNoId",
                routeTemplate: "api/Attainment/{action}",
                defaults: new { controller = "Attainment" }
            );
            #endregion*/
        }
    }
}
