using AppOscar.API.ViewModels.Filme;
using AppOscar.Models;
using AppOscar.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOscar.API.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        public List<Filme> Filmes { get; }
        private readonly AppOscarContext context;

        public FilmeRepository(AppOscarContext context)
        {
            this.context = context ?? throw new System.ArgumentNullException(nameof(context));
            Filmes = new List<Filme>();
        }

        public async Task Delete(Guid Id)
        {
            var filme = await context.Filmes.FindAsync(Id);
            context.Filmes.Remove(filme);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Filme>> GetAllFilmes()
        {
            List<Filme> filmes;

            filmes = await context.Filmes.ToListAsync();
            if (filmes == null)
                await Task.FromResult("Nenhum filme Cadastrado");
            return await Task.FromResult(filmes);
            
        }

        public async Task Save(FilmeCreate filme)
        {
            Filme novoFilme;

            novoFilme = new Filme
            {
                IdFilme = filme.IdFilme,
                NomeFilme = filme.NomeFilme
            };

            context.Filmes.Add(novoFilme);
            await context.SaveChangesAsync();
        }

        public async Task Update(Guid Id, Filme filme)
        {
            int index = Filmes.FindIndex(m => m.IdFilme == Id);
            if (index >= 0)
                await Task.Run(() => Filmes[index] = filme);
        }
    }
}
