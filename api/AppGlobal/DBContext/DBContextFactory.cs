using Microsoft.EntityFrameworkCore;
using System;

namespace AppGlobal.DBContext
{
    public class DBContextFactory<TContext> where TContext : DbContext
    {
        public TContext DBConnect(string cnnString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseSqlServer(cnnString);

            return (TContext)Activator.CreateInstance(typeof(TContext), optionsBuilder.Options);
        }
    }
}
