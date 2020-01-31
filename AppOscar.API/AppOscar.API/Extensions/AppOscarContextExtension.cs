using AppOscar.Models;
using AppOscar.Persistence;
using Bogus;
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
                .Generate(qtdeRegistros);

            context.Filmes.AddRange(filmesFake);

            var categoriasFake = new Faker<Categoria>("pt_BR")
                .RuleFor(c => c.IdCategoria, f => f.Random.Guid())
                .RuleFor(c => c.NomeCategoria, f => f.Lorem.Sentence())
                .RuleFor(c => c.PontosCategoria, f => f.Random.Int(0, 10))
                .Generate(qtdeRegistros);

            context.Categorias.AddRange(categoriasFake);

            context.SaveChanges();
        }
    }
}
