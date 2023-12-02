using EFCoreMovies.Entities;
using EFCoreMovies.Entities.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var invoice = new Invoice()
                {
                    CreationDate = DateTime.Now
                };

                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();

                //throw new ApplicationException("this is an error");

                var invoiceDetail = new List<InvoiceDetail>()
            {
                new InvoiceDetail()
                {
                    InvoiceId = invoice.Id,
                    Product = "Product A",
                    Price = 123
                },
                 new InvoiceDetail()
                {
                     InvoiceId = invoice.Id,
                    Product = "Product B",
                    Price = 456
                }
            };

                _context.InvoiceDetails.AddRange(invoiceDetail);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok("all good");
            }
            catch (Exception ex)
            {
                return BadRequest("There was an error");
            }
        }

        [HttpGet("Scalars")]
        public async Task<ActionResult> GetScalars()
        {
            var invoices = await _context.Invoices.Select(f => new
            {
                Id = f.Id,
                Total = _context.InvoiceDetailSum(f.Id),
                Average = Scalars.InvoiceDetailAverage(f.Id)
            }).OrderByDescending(f => _context.InvoiceDetailSum(f.Id)).ToListAsync();

            return Ok(invoices);
        }

        [HttpGet("Detail/{id:int}")]
        public async Task<ActionResult<IEnumerable<InvoiceDetail>>> GetDetail(int id)
        {
            var invoicedetail = await _context.InvoiceDetails.AsNoTracking()
            .Where(p => p.InvoiceId.Equals(id))
                .OrderByDescending(inv => inv.Total).ToListAsync();

            if (invoicedetail is null)
            {
                return NotFound();
            }

            return invoicedetail;
        }
    }
}

