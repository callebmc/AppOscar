using AppOscar.API.ViewModels.Filme;
using AppOscar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOscar.API.Repositories
{
    public interface IFilmeRepository
    {
        Task Save(FilmeCreate filme);
        Task Update(Guid Id, Filme filme);
        Task Delete(Guid Id);
        Task<IEnumerable<Filme>> GetAllFilmes();

    }
}
