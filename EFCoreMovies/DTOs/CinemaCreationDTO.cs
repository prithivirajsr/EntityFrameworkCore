using EFCoreMovies.Entities.Configurations;

namespace EFCoreMovies.DTOs
{
    public class CinemaCreationDTO
    {
        public string Name { get; set; }
        public double Latitue { get; set; }
        public double Longitue { get; set; }
        public CinemaOfferCreationDTO CinemaOffer { get; set; }
        public CinemaHallCreationDTO[] CinemaHalls { get; set; }
    }
}
