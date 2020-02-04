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
                optionsBuilder.UseSqlServer("Server=oscardosamigosdb.database.windows.net,1433;Database=oscardb;User ID=calleb.cecco;Password=250793aS@;");
        }
    }
}
