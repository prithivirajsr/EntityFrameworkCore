﻿using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Entities.Keyless
{
    [Keyless]
    public class MoviesWithCounts
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AmountGenres { get; set; }
        public int AmountCinemas { get; set; }
        public int AmountActors { get; set; }
    }
}
