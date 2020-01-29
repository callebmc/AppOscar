using AppOscar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppOscar.Persistence.Configuration
{
    public class ParticipacaoConfiguration : IEntityTypeConfiguration<Participacao>
    {
        public void Configure(EntityTypeBuilder<Participacao> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasKey(p => p.Id);

            builder.HasOne(c => c.Categoria)
                .WithMany(a => a.Participantes)
                .HasForeignKey(a => a.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(c => c.Filme)
                .WithMany(a => a.Participantes)
                .HasForeignKey(a => a.IdFilme)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
