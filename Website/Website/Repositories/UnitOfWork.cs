using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private IWebsiteContext context;

        public UnitOfWork(IWebsiteContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }

        public void SaveChanges()
        {
            int i = context.SaveChanges();
        }
    }
}