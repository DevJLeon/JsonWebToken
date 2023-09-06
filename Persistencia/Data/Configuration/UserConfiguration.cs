using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.ToTable("user");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(p => p.UserName)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(p => p.Password)
            .IsRequired()
            .HasMaxLength(255);

            builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(100);

            builder
            .HasMany(p=> p.Rols)
            .WithMany(r=> r.Users)
            .UsingEntity<UserRol>(
                j=> j
                .HasOne(pt => pt.Rol)
                .WithMany(t=> t.UserRols)
                .HasForeignKey(ut => ut.RolIdFk),

                j=>j
                .HasOne(et => et.Usuario)
                .WithMany(et => et.UserRols)
                .HasForeignKey(el => el.UsuarioIdFk),

                j => 
                {
                    j.ToTable("userRol");
                    j.HasKey(t => new { t.UsuarioIdFk, t.RolIdFk });
                }
            );
        }
    }
}