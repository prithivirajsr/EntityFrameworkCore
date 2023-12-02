namespace EFCoreMovies.DTOs
{
    public class MovieFilterDto
    {
        public string? Title { get; set; }
        public string? GenreName { get; set; }
        public bool InCinemas { get; set; }
        public bool UpcomingReleases { get; set; }
    }
}
