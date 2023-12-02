using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMovies.DTOs;
using EFCoreMovies.Entities;
using EFCoreMovies.Entities.Keyless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/Cinema")]
    public class CinemaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CinemaController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<CinemaDTO>> GetAll()
        {
            return await _context.Cinemas.AsNoTracking()
                .ProjectTo<CinemaDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet]
        [Route("WithoutLocation")]
        public async Task<IEnumerable<CinemaWithoutLocation>> WithoutLocation()
        {
            //return await _context.Set<CinemaWithoutLocation>().ToListAsync();
            return await _context.CinemaWithoutLocations.AsNoTracking().ToListAsync();
        }

        [HttpGet]
        [Route("Nearest")]
        public async Task<ActionResult> Nearest(double longitude, double latitude)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var myLocation = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));

            var maxDistanceInMeters = 2000; //2km

            var cinema = await _context.Cinemas.AsNoTracking().OrderBy(c => c.Location.Distance(myLocation))
                .Where(c => c.Location.IsWithinDistance(myLocation,maxDistanceInMeters))
                .Select(c => new {
                    Name = c.Name,
                    Distance = Math.Round(c.Location.Distance(myLocation))
                }).ToListAsync();
            return Ok(cinema);
        }

        //Inserting cinema with related entity data without using the dto
        [HttpPost]
        public async Task<ActionResult> Post()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var cinemaLocation = geometryFactory.CreatePoint(new Coordinate(-69.913539, 18.476256));

            var cinema = new Cinema()
            {
                Name = "My Cinema",
                Location = cinemaLocation,
                CinemaDetail = new CinemaDetail()
                {
                    History = "the history...",
                    Values = "the values",
                    Missions =  "the missions...",
                    CodeOfConduct = "the code of conduct"
                },
                CinemaOffer = new CinemaOffer()
                {
                    DiscountPercentage = 5,
                    Begin = DateTime.Today,
                    End = DateTime.Today.AddDays(7)
                },
                CinemaHalls = new List<CinemaHall>()
                {
                    new CinemaHall()
                    {
                        Cost = 200,
                        CinemaHallTypes = CinemaHallTypes.TwoDimensions,
                        Currency = CurrencyType.Rupees
                    },
                    new CinemaHall()
                    {
                        Cost = 500,
                        CinemaHallTypes = CinemaHallTypes.ThreeDimensions,
                        Currency = CurrencyType.Dollar
                    }
                }
            };

            await _context.AddAsync(cinema);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("InsertUsingDto")]
        public async Task<ActionResult> Post(CinemaCreationDTO cinemaCreationDto)
        {
            var cinema = _mapper.Map<Cinema>(cinemaCreationDto);
            await _context.AddAsync(cinema);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var cinema = await _context.Cinemas.Include(c => c.CinemaHalls)
                .Include(c => c.CinemaOffer).Include(c => c.CinemaDetail).FirstOrDefaultAsync(c => c.Id == id);

            //combining the raw sql with the linq query
            //var cinema = await _context.Cinemas.FromSqlInterpolated($"select * from cinemas where Id = {id}")
            //    .AsNoTracking().Include(c => c.CinemaHalls)
            //   .Include(c => c.CinemaOffer).Include(c => c.CinemaDetail).FirstOrDefaultAsync();

            if (cinema is null)
            {
                return NotFound();
            }

            cinema.Location = null;

            return Ok(cinema);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Put(int id, CinemaCreationDTO cinemaCreationDto)
        {
            var cinema = await _context.Cinemas.Include(c => c.CinemaHalls)
                .Include(c => c.CinemaOffer).FirstOrDefaultAsync(c => c.Id == id);

            if(cinema is null)
            {
                return NotFound();
            }

            cinema = _mapper.Map(cinemaCreationDto, cinema);

            _context.Cinemas.Update(cinema);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
