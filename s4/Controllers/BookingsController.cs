using Microsoft.AspNetCore.Mvc;
using s4.Data;

namespace s4.Models.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly DBContext context;

        public BookingsController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllBookings()
        {
            var Bookings = context.Bookings.ToList();
            return Ok(Bookings);
        }

        [HttpGet("ID")]
        public IActionResult Getattraction(int ID)
        {
            var attraction = context.Bookings.Where(u => u.ID == ID).FirstOrDefault();
            return Ok(attraction);
        }

        [HttpPost]
        public IActionResult AddAttraction([FromQuery] int UserID, [FromQuery] int CouponID, [FromQuery] int TransactionID, [FromBody] BookingsDto BookingsDto)
        {
            var AttractionEntity = new Bookings() { UserID = UserID, CouponID = CouponID, TransactionID = TransactionID, AmountPaid = BookingsDto.AmountPaid, BookingDate = BookingsDto.BookingDate };

            context.Bookings.Add(AttractionEntity);
            context.SaveChanges();

            return Ok(AttractionEntity);
        }

        [HttpDelete]
        public IActionResult DeleteAttraction(int ID)
        {
            var attraction = context.Bookings.Find(ID);

            if (attraction is null)
            {
                return NotFound();
            }

            context.Bookings.Remove(attraction);
            context.SaveChanges();

            return Ok(attraction);
        }
    }
}
