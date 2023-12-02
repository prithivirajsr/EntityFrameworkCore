using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreMovies.Entities.Configurations
{
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder) 
        {
            builder.Property(p => p.Name).HasMaxLength(150);
            builder.Property(p => p.DateOfBirth).HasColumnType("Date");
            builder.Property(p => p.Biography).IsRequired(false);
            //builder.Property(p => p.PictureURL).IsRequired(false);
            //Ignoring the age property not to mapped with the table column
            //builder.Ignore(p => p.Age);

            //owned type configuration
            builder.OwnsOne(p => p.Address, add =>
            {
                add.Property(a => a.Street).HasColumnName("Street");
                add.Property(a => a.Country).HasColumnName("Country");
                add.Property(a => a.Province).HasColumnName("Province");
            });
        }
    }
}
