using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Website.Models;
using Website.Repositories;
using Website.BusinessLogic;
using Website.Controllers;
using System.Web.Http;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using Microsoft.Owin.Security;

namespace Website.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IDbSetFactory, DbSetFactory>();

            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<IAttainmentRepository, AttainmentRepository>();
            container.RegisterType<IActivityRepository, ActivityRepository>();
            container.RegisterType<IBadgeRepository, BadgeRepository>();
            container.RegisterType<IGoalRepository, GoalRepository>();
            container.RegisterType<IMembershipRepository, MembershipRepository>();
            container.RegisterType<ITeamRepository, TeamRepository>();
            container.RegisterType<IPathRepository, PathRepository>();
            container.RegisterType<ITargetRepository, TargetRepository>();
            container.RegisterType<IMoodRepository, MoodRepository>();
            container.RegisterType<IStatusRepository, StatusRepository>();
            container.RegisterType<IFoodRepository, FoodRepository>();
            container.RegisterType<IReportRepository, ReportRepository>();

            container.RegisterType<IAccountLogic, AccountLogic>();
            container.RegisterType<IActivityLogic, ActivityLogic>();
            container.RegisterType<IAttainmentLogic, AttainmentLogic>();
            container.RegisterType<IMembershipLogic, MembershipLogic>();
            container.RegisterType<IPathLogic, PathLogic>();
            container.RegisterType<ITeamLogic, TeamLogic>();
            container.RegisterType<IBadgeLogic, BadgeLogic>();
            container.RegisterType<IGoalLogic, GoalLogic>();
            container.RegisterType<ITargetLogic, TargetLogic>();
            container.RegisterType<IMoodLogic, MoodLogic>();
            container.RegisterType<IStatusLogic, StatusLogic>();
            container.RegisterType<IFoodLogic, FoodLogic>();
            container.RegisterType<IReportLogic, ReportLogic>();

            container.RegisterType<IWebsiteContext, WebsiteContext>(new HierarchicalLifetimeManager());
            container.RegisterType<PredictionLogic, PredictionLogic>(new ContainerControlledLifetimeManager());
            container.RegisterType<DbContext, WebsiteContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<Account>>(new PerRequestLifetimeManager());

            container.RegisterType<IUserStore<Account>, UserStore<Account>>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));


        }
    }
}
