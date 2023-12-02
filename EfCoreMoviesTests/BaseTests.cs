using EFCoreMovies;
using Microsoft.EntityFrameworkCore;

namespace EfCoreMoviesTests
{
    public class BaseTests
    {
        public ApplicationDbContext BuildContext(string nameDB)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(nameDB).Options;
            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }
    }
}
