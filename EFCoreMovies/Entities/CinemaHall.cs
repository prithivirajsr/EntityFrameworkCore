namespace EFCoreMovies.Entities
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public CinemaHallTypes CinemaHallTypes { get; set; }
        public decimal Cost { get; set; }
        public CurrencyType Currency { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        public ICollection<Movie> Movies { get; set; }

        //lazy loading
        //public virtual Cinema Cinema { get; set; }
        //public virtual ICollection<Movie> Movies { get; set; }
    }
}
