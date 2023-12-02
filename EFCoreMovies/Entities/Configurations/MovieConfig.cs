using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreMovies.Entities.Configurations
{
    public class MovieConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(150);
            builder.Property(p => p.ReleaseDate).HasColumnType("Date");
            builder.Property(p => p.PosterURL).IsUnicode(false).HasMaxLength(500);
        }
    }
}
