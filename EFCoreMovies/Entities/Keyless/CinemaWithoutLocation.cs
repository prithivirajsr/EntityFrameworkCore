using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries.Prepared;

namespace EFCoreMovies.Entities.Keyless
{
    [Keyless]
    public class CinemaWithoutLocation
    {
        public int Id { get; set; }
        public string  Name { get; set; }
    }
}
