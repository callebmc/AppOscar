using AppOscar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppOscar.Persistence.Configuration
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(p => p.idCategoria);
            builder.Property(p => p.nomeCategoria).IsRequired(true);
            builder.Property(p => p.pontosCategoria).IsRequired(true);
        }
    }
}
