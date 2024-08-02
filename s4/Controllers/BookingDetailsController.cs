using Microsoft.AspNetCore.Mvc;
using s4.Data;

namespace s4.Models.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingDetailsController : ControllerBase
    {
        private readonly DBContext context;

        public BookingDetailsController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllBookingDetails()
        {
            var BookingDetails = context.BookingDetails.ToList();
            return Ok(BookingDetails);
        }

        [HttpGet("ID")]
        public IActionResult Getattraction(int ID)
        {
            var attraction = context.BookingDetails.Where(u => u.ID == ID).FirstOrDefault();
            return Ok(attraction);
        }

        [HttpPost]
        public IActionResult AddAttraction([FromQuery] int BookingID, [FromQuery] int ItemPriceID, [FromBody] BookingDetailsDto bookingDetailsDto)
        {
            var AttractionEntity = new BookingDetails() { BookingID = BookingID, ItemPriceID = ItemPriceID, isRefund = bookingDetailsDto.isRefund};

            context.BookingDetails.Add(AttractionEntity);
            context.SaveChanges();

            return Ok(AttractionEntity);
        }

        [HttpDelete]
        public IActionResult DeleteAttraction(int ID)
        {
            var attraction = context.BookingDetails.Find(ID);

            if (attraction is null)
            {
                return NotFound();
            }

            context.BookingDetails.Remove(attraction);
            context.SaveChanges();

            return Ok(attraction);
        }
    }
}
