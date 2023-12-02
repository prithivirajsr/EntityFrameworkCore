using EFCoreMovies.Entities;
using EFCoreMovies.Entities.Functions;
using EFCoreMovies.Entities.Keyless;
using EFCoreMovies.Entities.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCoreMovies
{
    public class ApplicationDbContext : DbContext
    {
        //private readonly DbContextOptions options;
        //private readonly IUserService _userService;

        //public ApplicationDbContext()
        //{

        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("name=DefaultConnection", options =>
        //        {
        //            options.UseNetTopologySuite();
        //        });
        //    }
        //}
        //public ApplicationDbContext(DbContextOptions options, IUserService userService, IChangeTrackerEventHandler changeTrackerEventHandler) : base(options)
        //{
        //    this.options = options;
        //    _userService = userService;
        //    if (changeTrackerEventHandler is not null)
        //    {
        //        ChangeTracker.Tracked += changeTrackerEventHandler.TrackedHandler;
        //        ChangeTracker.StateChanged += changeTrackerEventHandler.StateChangedHandler;
        //        SavingChanges += changeTrackerEventHandler.SavingChangesHandler;
        //        SavedChanges += changeTrackerEventHandler.SavedChangesHandler;
        //        SaveChangesFailed += changeTrackerEventHandler.SaveChangesFailHandler;
        //    }
        //}

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    configurationBuilder.Properties<DateTime>().HaveColumnType("Date");
        //    configurationBuilder.Properties<string>().HaveMaxLength(150);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //configuring the primary key using fluent api "HasKey" method
            //modelBuilder.Entity<Genre>().HasKey(p => p.Identifier);

            //configuring the name property to have maximum length of 150 characters
            //using fluent api "HasMaxLength" method
            //modelBuilder.Entity<Genre>().Property(p => p.Name).HasMaxLength(150);

            //configuring the name property to be required(not nullable)
            //using fluent api "HasMaxLength" method
            //modelBuilder.Entity<Genre>().Property(p => p.Name).HasMaxLength(150).IsRequired();

            //changed table and schema name using flent api method
            //modelBuilder.Entity<Genre>().ToTable(name: "Gen reTbl", schema: "Movie");

            //changing column name using "HasColumnName" method
            //modelBuilder.Entity<Genre>().Property(p => p.Name).HasMaxLength(150)
            //    .HasColumnName("GenreName");

            //configuring the name property to have maximum length of 150 characters
            //using fluent api "HasMaxLength" method
            //modelBuilder.Entity<Actor>().Property(p => p.Name).HasMaxLength(150);

            //configuring the date of birth property to be not required(nullable)
            //using fluent api "HasMaxLength" method
            //modelBuilder.Entity<Actor>().Property(p => p.DateOfBirth).HasColumnType("Date");

            //modelBuilder.Entity<Cinema>().Property(p => p.Name).HasMaxLength(150);
            //configuring the price property with the precision and scale digit values
            //using fluent api "HasMaxLength" method
            //modelBuilder.Entity<Cinema>().Property(p => p.Price).HasPrecision(precision: 9, scale: 2);

            //modelBuilder.Entity<Movie>().Property(p => p.Title).HasMaxLength(150);
            //modelBuilder.Entity<Movie>().Property(p => p.ReleaseDate).HasColumnType("Date");

            //configured not to use all the character by using "IsUnicode" method by passing false
            //modelBuilder.Entity<Movie>().Property(p => p.PosterURL).IsUnicode(false).HasMaxLength(500);

            //modelBuilder.Entity<CinemaOffer>().Property(p => p.DiscountPercentage).HasPrecision(5, 2);
            //modelBuilder.Entity<CinemaOffer>().Property(p => p.Begin).HasColumnType("Date");
            //modelBuilder.Entity<CinemaOffer>().Property(p => p.End).HasColumnType("Date");

            //modelBuilder.Entity<CinemaHall>().Property(p => p.Cost).HasPrecision(precision: 9, scale: 2);
            //modelBuilder.Entity<CinemaHall>().Property(p => p.CinemaHallTypes)
            //    .HasDefaultValue(CinemaHallTypes.TwoDimensions);

            //modelBuilder.Entity<MovieActor>().HasKey(p => new { p.MovieId, p.ActorId });
            //modelBuilder.Entity<MovieActor>().Property(p => p.Character).HasMaxLength(150);

            //Adding all entities configuration by assembly method
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (!Database.IsInMemory())
            {
                Module3Seeding.Seed(modelBuilder);
                Module6Seeding.Seed(modelBuilder);
                Module9Seeding.Seed(modelBuilder);
            }

            //modelBuilder.Entity<Genre>().HasQueryFilter(g => !g.IsDeleted); //query filter

            //modelBuilder.Entity<Log>().Property(l => l.Id).ValueGeneratedNever(); //// This method will not to generate Guid automatically in sql server

            //Ignore the class/entity to be mapped to database table
            //modelBuilder.Ignore<Address>();

            modelBuilder.Entity<CinemaWithoutLocation>().ToSqlQuery("Select Id, Name from Cinemas").ToView(null);

            //modelBuilder.Entity<MoviesWithCounts>().ToView("MoviesWithCounts");

            modelBuilder.Entity<MoviesWithCounts>().ToTable(name: null);

            modelBuilder.HasDbFunction(() => MoviesWithCounts(0)).HasName("MovieWithCounts");

            //modelBuilder.Entity<MoviesWithCounts>().ToSqlQuery(@"create view dbo.MoviesWithCounts as
            //                        select Id, Title,
            //                        (select count(*) from GenreMovie where MoviesId = Movies.Id) as AmountGenres,
            //                        (select count(distinct moviesId) from CinemaHallMovie
            //                        inner join CinemaHalls on CinemaHalls.Id = CinemaHallMovie.CinemaHallsId
            //                        where MoviesId = Movies.Id) as AmountCinemas,
            //                        (select count(*) from MoviesActors where MovieId = Movies.Id) as AmountActors
            //                        from Movies");

            //foreach (var entityTypes in modelBuilder.Model.GetEntityTypes())
            //{
            //    foreach (var property in entityTypes.GetProperties())
            //    {
            //        if(property.ClrType == typeof(string) && property.Name.Contains("URL", StringComparison.CurrentCultureIgnoreCase))
            //        {
            //            property.SetIsUnicode(false);
            //        }
            //    }
            //}

            TablePerTypeConfigurations(modelBuilder);
            Scalars.RegisterFunctions(modelBuilder);
            modelBuilder.HasSequence<int>("InvoiceNumber", "invoice"); //sequence column will be created in defined schema

            base.OnModelCreating(modelBuilder);
        }

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    ProcessSaveChanges();
        //    return base.SaveChangesAsync(cancellationToken);
        //}

        //private void ProcessSaveChanges()
        //{
        //    foreach(var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is AuditableEntity))
        //    {
        //        var entity = item.Entity as AuditableEntity;
        //        entity.CreatedBy = _userService.GetUserId();
        //    }

        //    foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Entity is AuditableEntity))
        //    {
        //        var entity = item.Entity as AuditableEntity;
        //        entity.ModifiedBy = _userService.GetUserId();
        //        item.Property(nameof(entity.CreatedBy)).IsModified = false;
        //    }
        //}

        public static void TablePerTypeConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Merchandising>().ToTable("Merchandising");
            modelBuilder.Entity<RentableMovie>().ToTable("RentableMovies");

            var movie1 = new RentableMovie()
            {
                Id = 1,
                Name = "Spider-Man",
                MovieId = 1,
                Price = 5.99m
            };

            var merch1 = new Merchandising()
            {
                Id = 2,
                Available = true,
                IsClothing = true,
                Name = "One Piece T-Shirt",
                Weight = 1,
                Volume = 1,
                Price = 11
            };

            modelBuilder.Entity<Merchandising>().HasData(merch1);
            modelBuilder.Entity<RentableMovie>().HasData(movie1);
        }

        [DbFunction]
        public int InvoiceDetailSum(int invoiceId) //it is a placeholder used to invoke the db InvoiceDetailSum function from C# name must be same or use name in attribute
        {
            return 0;
        }

        public IQueryable<MoviesWithCounts> MoviesWithCounts(int movieId)
        {
            return FromExpression(() => MoviesWithCounts(movieId));
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CinemaOffer> CinemaOffers { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<MovieActor> MoviesActors { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<CinemaWithoutLocation> CinemaWithoutLocations { get; set; }
        //public DbSet<MoviesWithCounts> MoviesWithCounts { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<CinemaDetail> CinemaDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
