using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();

            if(products is null)
            {
                return NotFound();
            }

            return products;
        }


        [HttpGet]
        [Route("Merchands")]
        public async Task<ActionResult<IEnumerable<Merchandising>>> GetMerchandProducts()
        {
            var merchandProducts  = await _context.Set<Merchandising>().AsNoTracking().ToListAsync();

            if (merchandProducts is null)
            {
                return NotFound();
            }

            return merchandProducts;
        }


        [HttpGet]
        [Route("RentalMovies")]
        public async Task<ActionResult<IEnumerable<RentableMovie>>> GetRentalMovies()
        {
            var rentalMovies = await _context.Set<RentableMovie>().AsNoTracking().ToListAsync();

            if (rentalMovies is null)
            {
                return NotFound();
            }

            return rentalMovies;
        }

    }
}
