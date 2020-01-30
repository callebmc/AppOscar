using AppOscar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOscar.API.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        public List<Filme> Filmes { get; }
        
        public FilmeRepository()
        {
            Filmes = new List<Filme>();
        }

        public async Task Delete(Guid Id)
        {
            int index = Filmes.FindIndex(m => m.IdFilme == Id);
            await Task.Run(() => Filmes.RemoveAt(index));
        }

        public async Task<IEnumerable<Filme>> GetAllFilmes()
        {
            return await Task.FromResult(Filmes);
        }

        public async Task Save(Filme filme)
        {
            await Task.Run(() => Filmes.Add(filme));
        }

        public async Task Update(Guid Id, Filme filme)
        {
            int index = Filmes.FindIndex(m => m.IdFilme == Id);
            if (index >= 0)
                await Task.Run(() => Filmes[index] = filme);
        }
    }
}
