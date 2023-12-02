using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMovies.Entities
{
    //[NotMapped] //Ignore class to be mapped to database table - other way check db context file
    [Owned] //used to mark entity/class as owned type - centeralized entity which can be used in multiple entity eg: acto, cinema
    // check actor and cinema config for owned type configurations
    public class Address
    {
        //public int Id { get; set; } //Commented for owned type because to use principal entity id address entity will act as dependent 
        public string Street { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
    }
}
