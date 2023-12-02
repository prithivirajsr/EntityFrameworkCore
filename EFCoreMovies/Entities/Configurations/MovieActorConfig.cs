using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCoreMovies.Entities.Configurations
{
    public class MovieActorConfig : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.HasKey(p => new { p.MovieId, p.ActorId });
            builder.Property(p => p.Character).HasMaxLength(150);

            // many-to-many relationship configuration - 2 one-to-many relationship
            //builder.HasOne(ma => ma.Actor).WithMany(ma => ma.MoviesActors).HasForeignKey(ma => ma.ActorId);
            //builder.HasOne(ma => ma.Movie).WithMany(ma => ma.MoviesActors).HasForeignKey(ma => ma.MovieId);
        }
    }
}
