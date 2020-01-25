using Microsoft.EntityFrameworkCore;
using AppOscar.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppOscar.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.id_user);
            builder.Property(p => p.nomeUsuario).IsRequired(true);
            builder.Property(p => p.emailUsuario).IsRequired(true);
            builder.Property(p => p.senhaUsuario).IsRequired(true);
        }
    }
}
