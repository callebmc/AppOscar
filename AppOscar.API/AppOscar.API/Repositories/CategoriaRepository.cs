using AppOscar.Models;
using AppOscar.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppOscar.API.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppOscarContext context;

        public CategoriaRepository(AppOscarContext context)
        {
            this.context = context ?? throw new System.ArgumentNullException(nameof(context));
        }
        public async Task Delete(Guid Id)
        {
            var categoria = await context.Categorias.FindAsync(Id);
            context.Categorias.Remove(categoria);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categoria>> GetAllCategorias()
        {
            List<Categoria> categorias;

            categorias = await context.Categorias.ToListAsync();
            if (categorias == null)
                await Task.FromResult("Nenhum filme Cadastrado");
            return await Task.FromResult(categorias);
        }

        public async Task Save(Categoria categoria)
        {
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();
        }

        public Task Update(Guid Id, Categoria categoria)
        {
            throw new NotImplementedException();
        }
    }
}
