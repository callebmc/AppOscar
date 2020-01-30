using AppOscar.API.ViewModels.Categoria;
using AppOscar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOscar.API.Repositories
{
    public interface ICategoriaRepository
    {
        Task Save(Categoria categoria);
        Task Update(Guid Id, Categoria categoria);
        Task Delete(Guid Id);
        Task<IEnumerable<Categoria>> GetAllCategorias();
    }
}
