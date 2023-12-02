using AutoMapper;
using EFCoreMovies.DTOs;
using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/Genres")]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private ILogger<GenresController> _logger { get; set; }
        public GenresController(ApplicationDbContext context, IMapper mapper, ILogger<GenresController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IEnumerable<Genre>> GetAll(int page = 1,int pageSize = 2)
        //{
        //    return await _context.Genres.AsNoTracking().Paginate(page,pageSize).ToListAsync();
        //}

        //[HttpGet]
        //[Route("Get")]
        //public async Task<ActionResult<Genre>> Get()
        //{
        //    var genre =  await _context.Genres.AsNoTracking().FirstOrDefaultAsync(g => g.Name.Contains("z"));
        //    if (genre == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(genre);
        //}

        //[HttpGet]
        //[Route("FilteredGenre")]
        //public async Task<IEnumerable<Genre>> GetFilteredGenres(string name)
        //{
        //    return await _context.Genres.AsNoTracking().Where(g => g.Name.Contains(name)).ToListAsync();
        //}

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Genre>> GetAll()
        {
            _context.Logs.Add(new Log { Message = "Calling Genre GetAll Method" });
            //setting value to the shadow property
            //_context.Entry(Log).Property<DateTime>("CreatedDate").CurrentValue = DateTime.Now;
            //getting value to the shadow property
            //var createdDate = _context.Entry(Log).Property<DateTime>("CreatedDate").CurrentValue;
            await _context.SaveChangesAsync();
            return await _context.Genres.AsNoTracking().ToListAsync();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var genre = await _context.Genres.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);

            var periodStart = _context.Entry(genre).Property<DateTime>("PeriodStart").CurrentValue;
            var periodEnd = _context.Entry(genre).Property<DateTime>("PeriodEnd").CurrentValue;

            //var genre = await _context.Genres.FromSql($"select * from genres where Id = {id}")
            //    .IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync();

            //var genre = await _context.Genres.FromSqlRaw($"Select * from genres where Id = {0}", id)
            //    .IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync();

            //var genre = await _context.Genres.FromSqlInterpolated($"Select * from genres where Id = {id}")
            //    .IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync();

            if (genre is null)
            {
                return NotFound();
            }

            //var genreDto = _mapper.Map<GenreDTO>(genre);

            return Ok(new
            {
                Id = genre.Id,
                Name = genre.Name,
                PeriodStart = periodStart,
                PeriodEnd = periodEnd
            });
        }

        [HttpGet]
        [Route("TemporalAndHistoryAll/{id:int}")]
        public async Task<ActionResult> GetTemporalAndHistoryTableData(int id)
        {
            var genres = await _context.Genres.TemporalAll().IgnoreQueryFilters().AsNoTracking()
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
                })
                .Where(g => g.Id == id).ToListAsync();


            return Ok(genres);
        }

        [HttpGet]
        [Route("TemporalAsOf/{id:int}")]
        public async Task<ActionResult> GetTemporalAsOf(int id, DateTime date)
        {
            var genres = await _context.Genres.TemporalAsOf(date).IgnoreQueryFilters().AsNoTracking()
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
                })
                .FirstOrDefaultAsync(g => g.Id == id);


            return Ok(genres);
        }

        [HttpGet]
        [Route("TemporalFromTo/{id:int}")]
        public async Task<ActionResult> GetTemporalFromTo(int id, DateTime from, DateTime to)
        {
            var genres = await _context.Genres.TemporalFromTo(from, to).IgnoreQueryFilters().AsNoTracking()
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
                })
                .Where(g => g.Id == id).ToListAsync();


            return Ok(genres);
        }

        [HttpGet]
        [Route("TemporalContainedIn/{id:int}")]
        public async Task<ActionResult> GetTemporalContainedIn(int id, DateTime from, DateTime to)
        {
            var genres = await _context.Genres.TemporalContainedIn(from, to).IgnoreQueryFilters().AsNoTracking()
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
                })
                .Where(g => g.Id == id).ToListAsync();


            return Ok(genres);
        }

        [HttpGet]
        [Route("TemporalBetween/{id:int}")]
        public async Task<ActionResult> GetTemporalBetween(int id, DateTime from, DateTime to)
        {
            var genres = await _context.Genres.TemporalBetween(from, to).IgnoreQueryFilters().AsNoTracking()
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd")
                })
                .Where(g => g.Id == id).ToListAsync();


            return Ok(genres);
        }

        [HttpPut]
        [Route("RestoreFromHistoryTable/{id:int}")]
        public async Task<ActionResult> RestoreFromHistoryTable(int id, DateTime date)
        {
            var genre = await _context.Genres.TemporalAsOf(date).IgnoreQueryFilters().FirstOrDefaultAsync(g => g.Id == id); //Use IgnoreQueryFilters method, to exclude the query filter

            if (genre is null)
            {
                return NotFound();
            }

            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                SET IDENTITY_INSERT Genres ON;

                INSERT INTO Genres(Id, Name) VALUES({genre.Id},{genre.Name})
        
                SET IDENTITY_INSERT Genres OFF;");
            }
            finally
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"SET IDENTITY_INSERT Genres OFF");
            }
            return Ok(genre);
        }

        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult> Post(GenreCreationDTO genreCreationDto)
        {
            bool IsGenreExists = await _context.Genres.AnyAsync(g => g.Name == genreCreationDto.Name);

            if (IsGenreExists)
            {
                return BadRequest($"Genre already exists with the name {genreCreationDto.Name}");
            }

            var genre = _mapper.Map<Genre>(genreCreationDto);
            //var beforeAddStatus = _context.Entry(genre).State;
            //await _context.Genres.AddAsync(genre);

            _context.Database.ExecuteSqlInterpolated($@"Insert into genres(Name) values({genre.Name})");
            //_context.Entry(genre).State = EntityState.Added;
            //_context.Add(genre); This is same as the above line
            //var afterAddStatus = _context.Entry(genre).State;
            // _context.SaveChangesAsync(); //In this line only new genre record will added to the database table
            //state will changed from Added to Unchanged
            //because when again savechange method called with Added state
            //then again one record will be inserted into the database table(duplicated record creation problem)
            //var afterSaveChangesStatus = _context.Entry(genre).State; 
            return Ok(genre);
        }

        [HttpPost]
        [Route("BulkInsert")]
        public async Task<ActionResult> BulkInsert(GenreCreationDTO[] genreCreationDto)
        {
            var genres = _mapper.Map<Genre[]>(genreCreationDto);
            //1st way of marking added state to all entities
            //foreach (var genre in genres)
            //{
            //    _context.Genres.Add(genre);
            //}
            //2nd way of marking added state to all entities
            await _context.Genres.AddRangeAsync(genres);
            await _context.SaveChangesAsync();
            return Ok(genres);
        }

        //It will remove the record from the database table
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre is null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return Ok(genre);
        }

        //Soft Delete - It will not remove the record from the database table, instead it will mark the IsDeleted column to true -> 1
        [HttpDelete]
        [Route("SoftDelete/{id:int}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre is null)
            {
                return NotFound();
            }

            genre.IsDeleted = true;
            await _context.SaveChangesAsync();
            return Ok(genre);
        }

        [HttpPut]
        [Route("Restore/{id:int}")]
        public async Task<ActionResult> Restore(int id)
        {
            var genre = await _context.Genres.IgnoreQueryFilters().FirstOrDefaultAsync(g => g.Id == id); //Use IgnoreQueryFilters method, to exclude the query filter

            if (genre is null)
            {
                return NotFound();
            }

            genre.IsDeleted = false;
            await _context.SaveChangesAsync();
            return Ok(genre);
        }

        [HttpPut]
        [Route("Put")]
        public async Task<ActionResult> Put(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre is null)
            {
                return BadRequest($"Genre not exists with the id {id}");
            }

            genre.Name = "Updated " + genre.Name;
            var updtedGenre = _mapper.Map<Genre>(genre);
            _context.Genres.Update(updtedGenre);
            await _context.SaveChangesAsync();
            return Ok(genre);
        }

        [HttpGet("Stored_Procedure/{id:int}")]
        public async Task<ActionResult<Genre>> GetSP(int id)
        {
            //below query is a non-composable so we can't able to use linq methods directly with query so used asasyncenumerable
            var genres = _context.Genres.FromSqlInterpolated($"EXEC Genres_GetById {id}")
                .IgnoreQueryFilters().AsAsyncEnumerable();

            await foreach (var genre in genres)
            {
                return genre;
            }

            return NotFound();
        }

        [HttpPost("Stored_Procedure")]
        public async Task<ActionResult> PostSP(GenreCreationDTO genreCreationDto)
        {
            var genreexists = await _context.Genres.AnyAsync(g => g.Name.Equals(genreCreationDto.Name));

            if (genreexists)
            {
                return BadRequest($"The genre with name {genreCreationDto.Name} already exists.");
            }

            var output = new SqlParameter();
            output.ParameterName = "@id";
            output.SqlDbType = System.Data.SqlDbType.Int;
            output.Direction = System.Data.ParameterDirection.Output;

            await _context.Database.ExecuteSqlRawAsync("EXEC Genres_Insert @name = {0}, @id = {1} OUTPUT", genreCreationDto.Name, output);

            var id = (int)output.Value;

            return Ok(id);
        }

        //Field Level Concurrency Conflict
        [HttpPost("ConcurrencyToken")]
        public async Task<ActionResult> ConcurrencyToken()
        {
            var genreId = 1;

            //Felipe reads a record from the db
            var genre = await _context.Genres.FirstOrDefaultAsync(p => p.Id == genreId);
            genre.Name = "Felipe was here"; //Felipe modified the name but not yet updated that in db

            //Claudio updates the record in the db
            await _context.Database.ExecuteSqlRawAsync($@"Update Genres set Name = 'Claudia was here' where Id = {genreId}");

            await _context.SaveChangesAsync();

            return Ok();
        }

        //Row Level Concurrency Conflict
        [HttpPost("RowConcurrencyToken")]
        public async Task<ActionResult> RowConcurrencyToken()
        {
            var genreId = 1;
            try
            {
                //Felipe reads a record from the db
                var genre = await _context.Genres.FirstOrDefaultAsync(p => p.Id == genreId);
                genre.IsDeleted = true; //Felipe modified the name

                //Claudio updates the same row in the db 
                await _context.Database.ExecuteSqlRawAsync($@"Update Genres set Name = 'Felipe was here' where Id = {genreId}");

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single(); //if it is a multiple entries then don't use single
                //use AsNoTracking to get latest entry if not used then the above instances will be returned
                var currentGenre = await _context.Genres.AsNoTracking().FirstOrDefaultAsync(g => g.Id.Equals(genreId));

                foreach (var property in entry.Metadata.GetProperties())
                {
                    var triedValue = entry.Property(property.Name).CurrentValue;
                    var currentDbValue = _context.Entry(currentGenre).Property(property.Name).CurrentValue;
                    var previousValue = entry.Property(property.Name).OriginalValue;

                    if (currentDbValue?.ToString() == triedValue?.ToString())
                    {
                        //This is not the property that changed
                        continue;
                    }

                    _logger.LogInformation($"---Property {property.Name} ---");
                    _logger.LogInformation($"---Tried Value {triedValue} ---");
                    _logger.LogInformation($"---Value in the database {currentDbValue} ---");
                    _logger.LogInformation($"---Previous Value {previousValue} ---");
                }
            }
            return BadRequest("The record was updated by somebody else");
        }

    }
}
