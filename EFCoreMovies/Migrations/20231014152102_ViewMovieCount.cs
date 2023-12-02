using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMovies.Migrations
{
    /// <inheritdoc />
    public partial class ViewMovieCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view dbo.MoviesWithCounts as
                                    select Id, Title,
                                    (select count(*) from GenreMovie where MoviesId = Movies.Id) as AmountGenres,
                                    (select count(distinct moviesId) from CinemaHallMovie
                                    inner join CinemaHalls on CinemaHalls.Id = CinemaHallMovie.CinemaHallsId
                                    where MoviesId = Movies.Id) as AmountCinemas,
                                    (select count(*) from MoviesActors where MovieId = Movies.Id) as AmountActors
                                    from Movies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop view dbo.MoviesWithCounts");
        }
    }
}
