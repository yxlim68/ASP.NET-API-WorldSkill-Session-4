using Microsoft.AspNetCore.Mvc;
using s4.Data;

namespace s4.Models.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPricesController : ControllerBase
    {
        private readonly DBContext context;

        public ItemPricesController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllItemPrices()
        {
            var ItemPrices = context.ItemPrices.ToList();
            return Ok(ItemPrices);
        }

        [HttpGet("ID")]
        public IActionResult Getattraction(int ID)
        {
            var attraction = context.ItemPrices.Where(u => u.ID == ID).FirstOrDefault();
            return Ok(attraction);
        }

        [HttpPost]
        public IActionResult AddAttraction([FromQuery] int ItemID, [FromBody] ItemPricesDto ItemPricesDto)
        {
            var AttractionEntity = new ItemPrices() { ItemID = ItemID, Price = ItemPricesDto.Price, Date = ItemPricesDto.Date, CancellationPolicyID = ItemPricesDto.CancellationPolicyID};

            context.ItemPrices.Add(AttractionEntity);
            context.SaveChanges();

            return Ok(AttractionEntity);
        }

        [HttpDelete]
        public IActionResult DeleteAttraction(int ID)
        {
            var attraction = context.ItemPrices.Find(ID);

            if (attraction is null)
            {
                return NotFound();
            }

            context.ItemPrices.Remove(attraction);
            context.SaveChanges();

            return Ok(attraction);
        }
    }
}
