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
            builder.HasKey(p => p.IdCategoria);

            builder.Property(p => p.NomeCategoria).IsRequired(true);

            builder.Property(p => p.PontosCategoria).IsRequired(true);

            builder.Property(p => p.CategoriaPhotoUrl).IsRequired(true);

            builder.HasMany(p => p.Participantes)
                .WithOne(a => a.Categoria)
                .HasForeignKey(a => a.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
