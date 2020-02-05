using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOscar.API
{
    public class DatabaseDriverSettings
    {
        /// <summary>
        /// Chave para armazenamento da configuração de Provider.
        /// </summary>
        public const string DriverSettingsKey = "DatabaseDriver";

        /// <summary>
        /// Provedor do EntityFrameworkCore para SQLite.
        /// </summary>
        public const string SQLiteProvider = "Sqlite";

        /// <summary>
        /// Provedor do EntityFrameworkCore para SQLServer.
        /// </summary>
        public const string SqlServerProvider = "SQLServer";

        /// <summary>
        /// Provedor do EntityFrameworkCore para InMemory.
        /// </summary>
        public const string InMemoryProvider = "InMemory";
    }
}

