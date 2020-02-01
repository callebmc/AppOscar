using AppOscar.Models;
using AppOscar.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AppOscar.Persistence
{
    public class AppOscarContext : DbContext
    {
        public AppOscarContext(DbContextOptions options) : base(options) { }

        protected AppOscarContext() { }

        public DbSet<User> Usuarios { get; set; }

        public DbSet<Filme> Filmes { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Participacao> Participacoes { get; set; }

        public DbSet<Voto> Votos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserConfiguration)));
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(FilmeConfiguration)));
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(CategoriaConfiguration)));
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ParticipacaoConfiguration)));
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(VotoConfiguration)));
            base.OnModelCreating(modelBuilder);
        }
    }
}
