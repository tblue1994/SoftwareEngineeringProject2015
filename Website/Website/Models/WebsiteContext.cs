using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Website.Repositories;

namespace Website.Models
{
    public class WebsiteContext : IdentityDbContext<Account>, IWebsiteContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public WebsiteContext()
            : base("name=WebsiteContext", throwIfV1Schema: false)
        {
        }
        //public System.Data.Entity.IDbSet<Website.Models.Account> Accounts { get; set; }
        public System.Data.Entity.IDbSet<Website.Models.Team> Teams { get; set; }

        public System.Data.Entity.IDbSet<Website.Models.Membership> Memberships { get; set; }
        public System.Data.Entity.IDbSet<Website.Models.Goal> Goals { get; set; }

        public System.Data.Entity.IDbSet<Website.Models.Badge> Badges { get; set; }
        public System.Data.Entity.IDbSet<Website.Models.Attainment> Attainments { get; set; }
        public System.Data.Entity.IDbSet<Website.Models.Target> Targets { get; set; }
        public System.Data.Entity.IDbSet<Website.Models.Activity> Activities { get; set; }
        public System.Data.Entity.IDbSet<Website.Models.Path> Paths { get; set; }

        public System.Data.Entity.IDbSet<Website.Models.Mood> Moods { get; set; }
        public System.Data.Entity.IDbSet<Website.Models.Status> Statuses { get; set; }
        public System.Data.Entity.IDbSet<Website.Models.Report> Reports { get; set; }
        /*
        public System.Data.Entity.DbSet<Website.Models.Team> InnerTeams { get; set; }

        public System.Data.Entity.DbSet<Website.Models.Membership> InnerMemberships { get; set; }
        public System.Data.Entity.DbSet<Website.Models.Goal> InnerGoals { get; set; }
        
        public System.Data.Entity.DbSet<Website.Models.Badge> InnerBadges { get; set; }
        public System.Data.Entity.DbSet<Website.Models.Attainment> InnerAttainments { get; set; }
        public System.Data.Entity.DbSet<Website.Models.Target> InnerTargets { get; set; }
        public System.Data.Entity.DbSet<Website.Models.Activity> InnerActivities { get; set; }

        public System.Data.Entity.DbSet<Website.Models.Mood> InnerMoods { get; set; }
        public System.Data.Entity.DbSet<Website.Models.Status> InnerStatuses { get; set; }


        public IDbAccess<Team> Teams { get { return new DbAccess<Team>(InnerTeams); } }

        public IDbAccess<Membership> Memberships { get { return new DbAccess<Membership>(InnerMemberships); } }

        public IDbAccess<Account> Accounts { get { return new DbAccess<Account>(Users); } }
        public IDbAccess<Goal> Goals { get { return new DbAccess<Goal>(InnerGoals); } }
        public IDbAccess<Badge> Badges { get { return new DbAccess<Badge>(InnerBadges); } }
        public IDbAccess<Attainment> Attainments { get { return new DbAccess<Attainment>(InnerAttainments); } }
        public IDbAccess<Target> Targets { get { return new DbAccess<Target>(InnerTargets); } }
        public IDbAccess<Activity> Activities { get { return new DbAccess<Activity>(InnerActivities); } }*/



        public void Put(Object obj)
        {
            Entry(obj).State = EntityState.Modified;
        }
        /*
        public void Put(Membership membership)
        {
            Entry(membership).State = EntityState.Modified;
        }

        public void Put(Team team)
        {
            Entry(team).State = EntityState.Modified;
        }

        public void Put(Activity activity)
        {
            Entry(activity).State = EntityState.Modified;
        }*/


        public static WebsiteContext Create()
        {
            return new WebsiteContext();
        }
    }

    public class DbSetFactory : IDbSetFactory
    {
        private readonly WebsiteContext context;

        public DbSetFactory(WebsiteContext context)
        {
            this.context = context;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public DbSet<T> CreateDbSet<T>() where T : class
        {
            return context.Set<T>();
        }

        public void ChangeObjectState(object entity, EntityState state)
        {
            var entry = context.Entry(entity);
            entry.State = state;
        }
    }
}
