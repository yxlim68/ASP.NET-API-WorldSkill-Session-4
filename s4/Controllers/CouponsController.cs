using Microsoft.AspNetCore.Mvc;
using s4.Data;
using s4.Model.Entities;
using s4.Model;

namespace s4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly DBContext context;

        public CouponsController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllData()
        {
            var Addon = context.Coupons.ToList();
            return Ok(Addon);
        }

        [HttpGet("ID")]
        public IActionResult GetAllDataByID(long ID)
        {
            var Addon = context.Coupons.Where(a => a.ID == ID).FirstOrDefault();
            return Ok(Addon);
        }

        [HttpPost]
        public IActionResult AddServices([FromBody] CouponsDto servicesDto)
        {
            var amenitiesEntity = new Coupons()
            {
                CouponCode = servicesDto.CouponCode,
                DiscountPercent = servicesDto.DiscountPercent,
                MaximimDiscountAmount = servicesDto.MaximimDiscountAmount
            };

            context.Coupons.Add(amenitiesEntity);
            context.SaveChanges();

            return Ok(amenitiesEntity);
        }

        [HttpDelete]
        public IActionResult DeleteUsers(long ID)
        {
            var User = context.Coupons.Find(ID);

            if (User is null)
            {
                return NotFound();
            }

            context.Coupons.Remove(User);
            context.SaveChanges();

            return Ok(User);
        }
    }
}
