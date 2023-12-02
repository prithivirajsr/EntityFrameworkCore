using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreMovies.Entities.Configurations
{
    public class CinemaConfig : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(150);

            //one to one relationship configuration
            //builder.HasOne(c => c.CinemaOffer).WithOne().HasForeignKey<CinemaOffer>(co => co.CinemaId);

            // one to many relationship configuration
            //builder.HasMany(c => c.CinemaHalls).WithOne(ch => ch.Cinema)
            //    .HasForeignKey(ch => ch.CinemaId).OnDelete(DeleteBehavior.Restrict);

            //table splitting configuration
            builder.HasOne(c => c.CinemaDetail).WithOne(c => c.Cinema).HasForeignKey<CinemaDetail>(ce => ce.Id);

            //owned type configuration
            builder.OwnsOne(p => p.Address, add =>
            {
                add.Property(a => a.Street).HasColumnName(nameof(Address.Street));
                add.Property(a => a.Country).HasColumnName(nameof(Address.Country));
                add.Property(a => a.Province).HasColumnName(nameof(Address.Province));
            });
        }
    }
}
