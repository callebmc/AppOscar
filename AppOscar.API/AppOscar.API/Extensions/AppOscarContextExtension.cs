using AppOscar.Models;
using AppOscar.Persistence;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppOscar.API.Extensions
{
    public static class AppOscarContextExtension
    {
        public static async Task SeedData(this AppOscarContext context, int qtdeRegistros = 10, CancellationToken ct = default)
        {
            if (context.Usuarios.Any())
                return;

            var usuariosFake = new Faker<User>()
                .RuleFor(p => p.nomeUsuario, f => f.Name.FullName())
                .RuleFor(p => p.emailUsuario, (f, p) => f.Internet.Email(firstName: p.nomeUsuario))
                .RuleFor(p => p.senhaUsuario, f => f.Internet.Password())
                .Generate(qtdeRegistros);

            context.Usuarios.AddRange(usuariosFake);
            await context.SaveChangesAsync(ct);

            //if (context.Filmes.Any())
            //    return;

            //var filmesFake = new Faker<Filme>()
            //    .RuleFor(p => p.nomeFilme, f => f.Name.FullName());
        }
    }
}
