using System.ComponentModel.DataAnnotations;

namespace EFCoreMovies.Entities
{
    //Changing the table name and schema name using data annotation attribute
    //[Table(name:"GenreTbl",Schema:"Movies")]
    //[Index(nameof(Name), IsUnique = true)]
    public class Genre : AuditableEntity
    {
        //Configured as primary key by default conventional model because it contains "id" text.
        public int Id { get; set; }

        /*Configuring property as primary key using data annotation attribute because property name does not contains
        "id" text so its not following conventional model
        [Key]
        public int Identifier { get; set; }
         */
        //[ConcurrencyCheck]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Movie> Movies { get; set; }
        [Timestamp] //row concurreny conflict check
        public byte[] RowVersion { get; set; }

        //lazy loading
        // public virtual ICollection<Movie> Movies { get; set; }

        //configuring property to have maximum length of 150 characters using data annotation attribute
        //[StringLength(maximumLength: 150)]
        //public string Name { get; set; }

        //configuring property to be required(not nullable) using data annotation attribute
        //[Required]
        //public string Name { get; set; }

        //changed column name using data annotation attribute
        //[Column("GenreName")]
        //public string Name { get; set; }
    }
}
