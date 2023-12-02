using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMovies.DTOs;
using EFCoreMovies.Entities;
using EFCoreMovies.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/Actors")]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ActorsController(ApplicationDbContext context, IMapper mapper) 
        {  
            _context = context;
            _mapper = mapper;
        }

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IEnumerable<ActorDTO>> GetAll(int page=1,int pageSize=2)
        //{
        //    return await _context.Actors.AsNoTracking()
        //        .ProjectTo<ActorDTO>(_mapper.ConfigurationProvider)
        //        .Paginate(page, pageSize).ToListAsync();
        //}

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<ActorDTO>> GetAll()
        {
            return await _context.Actors.AsNoTracking()
                .ProjectTo<ActorDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActorCreationDTO actorCreationDto)
        {
            var actor = _mapper.Map<Actor>(actorCreationDto);
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
            return Ok(actor);
        }

        //update using the connected architecture (i.e) to fetch and upated used same db context instance
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Put(int id, ActorCreationDTO actorCreationDto)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if(actor is null)
            {
                return NotFound();
            }

            actor = _mapper.Map(actorCreationDto, actor); //actorCreationDto object will mapped to the actor object

            await _context.SaveChangesAsync();
            return Ok(actor);
        }

        //update using the dis-connected architecture (i.e) to fetch and upated used different db context instance
        [HttpPut]
        [Route("disconnect/{id:int}")]
        public async Task<ActionResult> PutDisconnected(int id, ActorCreationDTO actorCreationDto)
        {
            var IsActorExists = await _context.Actors.AnyAsync(a => a.Id == id);

            if (!IsActorExists)
            {
                return NotFound();
            }

            var actor = _mapper.Map<Actor>(actorCreationDto);
            actor.Id = id;
            _context.Actors.Update(actor); //This will update the every column
            //_context.Entry(actor).Property(a => a.Name).IsModified = true; //This is update only the name property
            await _context.SaveChangesAsync();
            return Ok(actor);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Actor>> Get(int id)
        {
            var actor = await _context.Actors.AsNoTracking().FirstOrDefaultAsync(a => a.Id.Equals(id));

            if (actor is null)
            {
                return NotFound();
            }

            return actor;
        }
    }
}
