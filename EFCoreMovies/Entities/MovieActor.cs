namespace EFCoreMovies.Entities
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string Character { get; set; }
        public int Order { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }

        //lazy loading
        //public virtual Movie Movie { get; set; }
        //public virtual Actor Actor { get; set; }
    }
}
