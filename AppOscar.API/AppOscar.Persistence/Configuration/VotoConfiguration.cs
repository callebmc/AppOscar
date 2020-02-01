using AppOscar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppOscar.Persistence.Configuration
{
    public class VotoConfiguration : IEntityTypeConfiguration<Voto>
    {
        public void Configure(EntityTypeBuilder<Voto> builder)
        {
            builder.Property(v => v.Id).ValueGeneratedOnAdd();
            builder.HasKey(v => v.Id);

            builder.Property(v => v.DthCriacao).IsRequired(true);
            builder.Property(v => v.IdParticipacao).IsRequired(true);

            builder.HasOne(v => v.Participacao)
                .WithMany(p => p.Votos)
                .HasForeignKey(v => v.IdParticipacao)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
