using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMovies.Entities.Configurations
{
    public class GenreConfig : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable(name: "Genres", options =>
            {
                options.IsTemporal(); //Temporal table is a sql server feature but can be configured usign entity framework
            });

            //Temporal Table  must needed columns
            builder.Property<DateTime>("PeriodStart").HasColumnType("datetime2");
            builder.Property<DateTime>("PeriodEnd").HasColumnType("datetime2");


            //configured the concurrency conflict check using data annotation
            builder.Property(p => p.Name).HasMaxLength(150)/*.IsConcurrencyToken()*/; // IsConcurrencyToken used to prent from concurrency conflict issue
            builder.HasIndex(p => p.Name).IsUnique().HasFilter("IsDeleted = 'false'"); //create a index as unique with filter
            builder.HasQueryFilter(g => !g.IsDeleted); //query filter
            //builder.Property(p => p.RowVersion).IsRowVersion();  //row level concurrency conflict check, for data annotation syntax check genre.cs file
        }
    }
}
