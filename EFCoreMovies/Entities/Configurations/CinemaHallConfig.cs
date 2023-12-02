using EFCoreMovies.Entities.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreMovies.Entities.Configurations
{
    public class CinemaHallConfig : IEntityTypeConfiguration<CinemaHall>
    {
        public void Configure(EntityTypeBuilder<CinemaHall> builder)
        {
            builder.Property(p => p.Cost).HasPrecision(precision: 9, scale: 2);
            builder.Property(p => p.CinemaHallTypes)
                .HasDefaultValue(CinemaHallTypes.TwoDimensions)
                /*.HasConversion<string>()*/;
            //It will store the string representation of cinemahall type instead of numberic value
            //- HasConversion() [built-in conversion]
            builder.Property(p => p.Currency).HasConversion<CurrencyToStringConverter>();
        }
    }
}
