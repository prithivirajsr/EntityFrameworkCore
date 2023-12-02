using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMovies.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //private string _name;
        //public string Name { 
        //    get
        //    {
        //        return _name;
        //    }
        //    set
        //    {
        //        //tOm hoLLanD => Tom Holland
        //        _name = string.Join(' ',
        //            value.Split(' ')
        //            .Select(l => l[0].ToString().ToUpper() + l.Substring(1).ToLower()).ToArray());
        //    } 
        //}

        public string Biography { get; set; }

        //configured the date of birth property to be nullable(not required)
        public DateTime? DateOfBirth { get; set; }
       // [NotMapped]  //Ignoring the age property not to mapped with the table column
       // public int? Age { get
       //     {
       //         if (!DateOfBirth.HasValue)
       //         {
       //             return null;
       //         }
       //         var dob = DateOfBirth.Value;
       //         var age = DateTime.Today.Year - dob.Year;
       //         //checking whether birthday already occured
       //         if(new DateTime(DateTime.Today.Year,dob.Month,dob.Day) > DateTime.Today)
       //         {
       //             age--;
       //         }
       //         return age;
       //     } }

       //public string PictureURL { get; set; }
       public ICollection<MovieActor> MoviesActors { get; set; }
       public Address Address { get; set; }

        //lazy loading
        //public virtual ICollection<MovieActor> MoviesActors { get; set; }


        //[Column(TypeName = "Date")]
        //public DateTime? DateOfBirth { get; set; }
    }
}
