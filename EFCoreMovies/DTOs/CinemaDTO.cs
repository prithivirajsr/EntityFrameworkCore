using AutoMapper;

namespace EFCoreMovies.DTOs
{
    public class CinemaDTO 
    {
       public int Id { get; set; }
       public string Name { get; set; }
       public double longitude { get; set; }
       public double latitude { get; set; }
    }
}
