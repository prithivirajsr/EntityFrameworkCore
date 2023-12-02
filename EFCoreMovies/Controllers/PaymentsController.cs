using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PaymentsController(ApplicationDbContext contex) 
        {
            _context = contex;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAll()
        {
            var payments = await _context.Payments.AsNoTracking().ToListAsync();

            if(payments is null)
            {
                return NotFound();
            }

            return payments;
        }

        [HttpGet]
        [Route("CardPayments")]
        public async Task<ActionResult<IEnumerable<CardPayment>>> GetCardPayments()
        {
            var cardPayments = await _context.Payments.OfType<CardPayment>().AsNoTracking().ToListAsync();

            if (cardPayments is null)
            {
                return NotFound();
            }

            return cardPayments;
        }

        [HttpGet]
        [Route("PaypalPayments")]
        public async Task<ActionResult<IEnumerable<PaypalPayment>>> GetPaypalPayments()
        {
            var paypalPayments = await _context.Payments.OfType<PaypalPayment>().AsNoTracking().ToListAsync();

            if (paypalPayments is null)
            {
                return NotFound();
            }

            return paypalPayments;
        }
    }
}
