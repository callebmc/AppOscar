using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppOscar.Persistence
{
    public class AppOscarContextSqlite : AppOscarContext
    {
        public AppOscarContextSqlite(DbContextOptions options) : base(options)
        {
        }

        protected AppOscarContextSqlite()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("Data Source=oscarLite-migrations.db");
        }
    }
}
