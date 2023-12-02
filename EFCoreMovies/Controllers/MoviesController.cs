using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMovies.DTOs;
using EFCoreMovies.Entities;
using EFCoreMovies.Entities.Keyless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/Movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<MovieDTO>> Get(int id)
        //{
        //    var movie = await _context.Movies.AsNoTracking().Include(m => m.Genres
        //    .Where(g => !g.Name.Contains("m")).OrderByDescending(g => g.Name))
        //        .Include(m => m.CinemaHalls.OrderByDescending(ch => ch.Cinema.Id)).ThenInclude(ch => ch.Cinema)
        //        .Include(m => m.MoviesActors).ThenInclude(ma => ma.Actor)
        //        .FirstOrDefaultAsync(m => m.Id == id);

        //    if (movie is null)
        //    {
        //        return NotFound();
        //    }

        //    var movieDTO = _mapper.Map<MovieDTO>(movie);

        //    movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(c => c.Id).ToList();

        //    return Ok(movieDTO);
        //}

        ////Explict Loading using AutoMapper
        //[HttpGet("automapper/{id:int}")]
        //public async Task<ActionResult<MovieDTO>> GetWithAutoMapper(int id)
        //{
        //    var movieDTO = await _context.Movies.AsNoTracking().ProjectTo<MovieDTO>(_mapper.ConfigurationProvider)
        //        .FirstOrDefaultAsync(m => m.Id == id);

        //    if (movieDTO is null)
        //    {
        //        return NotFound();
        //    }


        //    movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(c => c.Id).ToList();

        //    return Ok(movieDTO);
        //}

        //[HttpGet("selectloading/{id:int}")]
        //public async Task<ActionResult<MovieDTO>> GetSelectLoading(int id)
        //{
        //    var movieDTO = await _context.Movies.Select(m => new MovieDTO
        //    {
        //        Id = m.Id,
        //        Title = m.Title,
        //        Genres = _mapper.Map<List<GenreDTO>>(m.Genres
        //        .OrderByDescending(g=>g.Name).Where(g => !g.Name.Contains("m"))),
        //        Cinemas = _mapper.Map<List<CinemaDTO>>(m.CinemaHalls
        //        .OrderByDescending(ch => ch.Cinema.Id).Select(ch => ch.Cinema)),
        //        Actors = _mapper.Map<List<ActorDTO>>(m.MoviesActors.Select(ma => ma.Actor)),
        //    }).FirstOrDefaultAsync(m => m.Id == id);

        //    movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(c => c.Id).ToList();

        //    if (movieDTO is null)
        //    {
        //        return NotFound();
        //    }

        //    return movieDTO;
        //}

        //[HttpGet("explicitloading/{id:int}")]
        //public async Task<ActionResult<MovieDTO>> GetExplicit(int id)
        //{
        //    var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

        //    if(movie is null)
        //    {
        //        return NotFound();
        //    }

        //    await _context.Entry(movie).Collection(m => m.Genres).LoadAsync();

        //    var movieDto = _mapper.Map<MovieDTO>(movie);

        //    return movieDto;
        //}

        //[HttpGet("lazyloading/{id:int}")]
        //public async Task<ActionResult<MovieDTO>> GetLazyLoading(int id)
        //{
        //    var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

        //    if(movie is null)
        //    {
        //        return NotFound();
        //    }

        //    var movieDto = _mapper.Map<MovieDTO>(movie);

        //    movieDto.Cinemas = movieDto.Cinemas.DistinctBy(c => c.Id).ToList();

        //    return movieDto;
        //}

        //[HttpGet("GroupedByCinema")]
        //public async Task<ActionResult> GetGroupedByInCinema()
        //{
        //    var groupedMovies = await _context.Movies.GroupBy(m => m.InCinemas).Select(g => new
        //    {
        //        InCinema = g.Key,
        //        Count = g.Count(),
        //        Movies = g.ToList()
        //    }).ToListAsync();
        //    return Ok(groupedMovies);
        //}

        //[HttpGet("GroupedByGenre")]
        //public async Task<ActionResult> GetGroupedByGenre()
        //{
        //    var groupedMovies = await _context.Movies.GroupBy(m => m.Genres.Count()).Select(g => new
        //    {
        //        Count = g.Key,
        //        Title = g.Select(m => m.Title),
        //        Genre = g.Select(m => m.Genres).SelectMany(g => g).Select(ge => ge.Name).Distinct()
        //    }).ToListAsync();
        //    return Ok(groupedMovies);
        //}

        //[HttpGet("Filtered")]
        //public async Task<ActionResult<List<MovieDTO>>> GetFiltered([FromQuery] MovieFilterDto movieFilterDto)
        //{
        //    var moviesQueryable = _context.Movies.Include(m => m.Genres).AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(movieFilterDto.Title))
        //    {
        //        moviesQueryable = moviesQueryable.Where(m => m.Title.Contains(movieFilterDto.Title));
        //    }

        //    if (movieFilterDto.InCinemas)
        //    {
        //        moviesQueryable = moviesQueryable.Where(m => m.InCinemas);
        //    }

        //    if (movieFilterDto.UpcomingReleases)
        //    {
        //        var today = DateTime.Today;
        //        moviesQueryable = moviesQueryable.Where(m => m.ReleaseDate > today);
        //    }

        //    if(!string.IsNullOrWhiteSpace(movieFilterDto.GenreName))
        //    {
        //        moviesQueryable = moviesQueryable.Where(m => m.Genres.Select(g => g.Name) 
        //                          .Contains(movieFilterDto.GenreName));
        //    }

        //    var movies = await moviesQueryable.ToListAsync();

        //    return _mapper.Map<List<MovieDTO>>(movies);
        //}

        //Explict Loading using AutoMapper
        [HttpGet("automapper/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetWithAutoMapper(int id)
        {
            var movieDTO = await _context.Movies.AsNoTracking().ProjectTo<MovieDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movieDTO is null)
            {
                return NotFound();
            }


            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(c => c.Id).ToList();

            return Ok(movieDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post(MovieCreationDTO movieCreationDto)
        {
            var movie = _mapper.Map<Movie>(movieCreationDto);

            movie.Genres.ForEach(g => _context.Entry(g).State = EntityState.Unchanged);
            movie.CinemaHalls.ForEach(ch => _context.Entry(ch).State = EntityState.Unchanged);

            if (movie.MoviesActors is not null)
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i + 1;
                }
            }

            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //MoviesWithCounts View
        //[HttpGet]
        //[Route("WithCounts")]
        //public async Task<ActionResult<IEnumerable<MoviesWithCounts>>> WithCounts()
        //{
        //    return await _context.MoviesWithCounts.AsNoTracking().ToListAsync();
        //}

        [HttpGet("WithCounts/{id:int}")]
        public async Task<ActionResult<MoviesWithCounts>> GetWithCounts(int id)
        {
            var result = await _context.MoviesWithCounts(id).FirstOrDefaultAsync();

            if (result is null)
            {
                return NotFound();
            }

            return result;
        }
    }
}
