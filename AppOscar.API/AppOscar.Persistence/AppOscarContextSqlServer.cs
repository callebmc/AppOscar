using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppOscar.Persistence
{
    public class AppOscarContextSqlServer : AppOscarContext
    {
        public AppOscarContextSqlServer(DbContextOptions options) : base(options)
        {
        }

        protected AppOscarContextSqlServer()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=tcp:oscardosamigosdb.database.windows.net,1433;Initial Catalog=oscardb;Persist Security Info=False;User ID=calleb.cecco;Password=25071993;");
        }
    }
}
