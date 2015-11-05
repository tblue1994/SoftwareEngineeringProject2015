using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Models
{
    public interface IWebsiteContext
    {
        IDbSet<Website.Models.Team> Teams { get; }

        IDbSet<Website.Models.Membership> Memberships { get; }

        IDbSet<Website.Models.Account> Users { get; }
        //new
        IDbSet<Website.Models.Activity> Activities { get; }
        IDbSet<Website.Models.Goal> Goals { get; }

        IDbSet<Website.Models.Badge> Badges { get; }
        IDbSet<Website.Models.Attainment> Attainments { get; }
        IDbSet<Website.Models.Target> Targets { get; }
        IDbSet<Website.Models.Report> Reports { get; }
        /*IDbAccess<Website.Models.Team> Teams { get; }

        IDbAccess<Website.Models.Membership> Memberships { get; }

        IDbAccess<Website.Models.Account> Accounts { get; }
        //new
        IDbAccess<Website.Models.Activity> Activities { get; }
        IDbAccess<Website.Models.Goal> Goals { get; }

        IDbAccess<Website.Models.Badge> Badges { get; }
        IDbAccess<Website.Models.Attainment> Attainments { get; }
        IDbAccess<Website.Models.Target> Targets { get; }
        */
        IDbSet<Website.Models.Mood> Moods { get; }

        IDbSet<Website.Models.Status> Statuses { get; }

        int SaveChanges();

        void Dispose();

        void Put(Object obj);
        /*void Put(Membership membership);
        void Put(Team team);
        void Put(Activity activity);*/
    }
}
