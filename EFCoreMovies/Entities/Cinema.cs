using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace EFCoreMovies.Entities
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public decimal Price { get; set; }
        public Point Location { get; set; }

        //navigation property
        public CinemaOffer CinemaOffer { get; set; }

        public ICollection<CinemaHall> CinemaHalls { get; set; }
        public CinemaDetail CinemaDetail { get; set; }
        public Address Address { get; set; }
        //lazy loading
        //public virtual CinemaOffer CinemaOffer { get; set; }
        //public virtual ICollection<CinemaHall> CinemaHalls { get; set; }


        //configured the precisiona and scale using the precision data annotation attribute
        //[Precision(precision:9,scale:2)]
        //public decimal Price { get; set; }
    }
}
