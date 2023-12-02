using EFCoreMovies.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Utilities
{
    public class Singleton
    {
        private readonly IServiceProvider _serviceProvider;
        public Singleton(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                return await context.Genres.ToListAsync();
            }
        }
    }
}
