using AppOscar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppOscar.Persistence.Configuration
{
    public class FilmeConfiguration : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder.HasKey(p => p.IdFilme);

            builder.Property(p => p.NomeFilme).IsRequired(true);

            builder.Property(p => p.FilmePhotoUrl).IsRequired(true);

            builder.HasMany(p => p.Participantes)
                .WithOne(f => f.Filme)
                .HasForeignKey(f => f.IdFilme)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
