using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterURL { get; set; }
        public List<Genre> Genres { get; set; }
        public List<CinemaHall> CinemaHalls { get; set; }
        public List<MovieActor> MoviesActors { get; set; }

        //lazy loading
        //public virtual ICollection<Genre> Genres { get; set; }
        //public virtual ICollection<CinemaHall> CinemaHalls { get; set; }
        //public virtual ICollection<MovieActor> MoviesActors { get; set; }

        //congirued to not use all chaaracters to reduce space
        //[Unicode(false)]
        //public string PosterURL { get; set; }
    }
}
