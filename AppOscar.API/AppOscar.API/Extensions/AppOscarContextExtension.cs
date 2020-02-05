using AppOscar.Models;
using AppOscar.Persistence;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppOscar.API.Extensions
{
    public static class AppOscarContextExtension
    {
        public static void SeedData(this AppOscarContext context, int qtdeRegistros = 10)
        {
            if (context.Usuarios.Any())
               return;

            var usuariosFake = new Faker<User>("pt_BR")
                .RuleFor(p => p.nomeUsuario, f => f.Name.FullName())
                .RuleFor(p => p.emailUsuario, (f, p) => f.Internet.Email(firstName: p.nomeUsuario))
                .RuleFor(p => p.senhaUsuario, f => f.Internet.Password())
                .Generate(qtdeRegistros);

            context.Usuarios.AddRange(usuariosFake);

            var filmesFake = new Faker<Filme>("pt_BR")
                .RuleFor(f => f.IdFilme, f => f.Random.Guid())
                .RuleFor(f => f.NomeFilme, f => f.Hacker.Phrase())
                .RuleFor(f => f.FilmePhotoUrl, f => f.Internet.Url())
                .Generate(qtdeRegistros);

            context.Filmes.AddRange(filmesFake);

            var categoriasFake = new Faker<Categoria>("pt_BR")
                .RuleFor(c => c.IdCategoria, f => f.Random.Guid())
                .RuleFor(c => c.NomeCategoria, f => f.Lorem.Sentence())
                .RuleFor(c => c.PontosCategoria, f => f.Random.Int(0, 10))
                .RuleFor(f => f.CategoriaPhotoUrl, f => f.Internet.Url())
                .Generate(qtdeRegistros);

            context.Categorias.AddRange(categoriasFake);

            context.SaveChanges();
        }

        public static void AddOscarDbContextNovo(this IServiceCollection services, IConfiguration configuration)
        {
            // Buscando o Provedor configurado e a ConnectionString nos AppSettings.
            string databaseDriver = configuration.GetValue<string>(DatabaseDriverSettings.DriverSettingsKey);
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            switch (databaseDriver)
            {
                case DatabaseDriverSettings.SQLiteProvider:
                    services.AddDbContext<AppOscarContext, AppOscarContextSqlite>(options =>
                    {
                        options.UseSqlite(connectionString);
                    });
                    //Log.Warning("A utilização do SQLite não é recomendada para Produção/Homologação");
                    break;
                case DatabaseDriverSettings.SqlServerProvider:
                    services.AddDbContext<AppOscarContext, AppOscarContextSqlServer>(options =>
                    {
                        options.UseSqlServer("Server=tcp:oscardosamigosdb.database.windows.net,1433;Initial Catalog=oscardb;Persist Security Info=False;User ID=calleb.cecco;Password=250793aS@;");
                    });
                    //Log.Information("Utilizando EntityFrameworkCore SqlServer");
                    break;
                case DatabaseDriverSettings.InMemoryProvider:
                    services.AddDbContext<AppOscarContext>(options =>
                    {
                        options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                    });
                    //Log.Error("A utilização InMemory não é recomendada");
                    break;
                default:
                    throw new ArgumentException("Não foi especificado um driver de banco de dados", nameof(databaseDriver));
            }
        }
    }
}
