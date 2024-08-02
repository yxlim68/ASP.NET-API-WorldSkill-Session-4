using s4.Data;
using s4.Models.Entities;
using s4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace s4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemAttractionsController : ControllerBase
    {
        private readonly DBContext context;

        public ItemAttractionsController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllItemAttractions()
        {
            var ItemAttractions = context.ItemAttractions.ToList();
            return Ok(ItemAttractions);
        }

        [HttpGet("itemAttractionID")]
        public IActionResult GetitemAttraction(int itemAttractionID)
        {
            var itemAttraction = context.ItemAttractions.Where(u => u.ID == itemAttractionID).FirstOrDefault();
            return Ok(itemAttraction);
        }

        [HttpPost]
        public IActionResult AddItemAttraction([FromQuery] int itemID, [FromQuery] int attractionID, [FromBody] ItemAttractionDto ItemAttractionDto)
        {
            var ItemAttractionEntity = new ItemAttractions() { ItemID = itemID, AttractionID = attractionID, Distance = ItemAttractionDto.Distance, DurationByCar = ItemAttractionDto.DurationByCar, DurationOnFoot = ItemAttractionDto.DurationOnFoot };

            context.ItemAttractions.Add(ItemAttractionEntity);
            context.SaveChanges();

            return Ok(ItemAttractionEntity);
        }

        [HttpPut("{ItemAttractionID}")]
        public IActionResult ItemAttractionsType(int ItemAttractionID, ItemAttractionDto ItemAttractionDto)
        {
            var ItemAttractionEntity = context.ItemAttractions.Find(ItemAttractionID);

            ItemAttractionEntity.AttractionID = ItemAttractionDto.AttractionID;
            ItemAttractionEntity.ItemID = ItemAttractionDto.ItemID;
            ItemAttractionEntity.DurationOnFoot = ItemAttractionDto.DurationOnFoot;
            ItemAttractionEntity.DurationByCar = ItemAttractionDto.DurationByCar;
            ItemAttractionEntity.Distance = ItemAttractionDto.Distance;

            context.SaveChanges();

            return Ok(ItemAttractionEntity);
        }

        [HttpDelete]
        public IActionResult DeleteItemAttractions(int ItemAttractionID)
        {
            var itemAttraction = context.ItemAttractions.Find(ItemAttractionID);

            if (itemAttraction is null)
            {
                return NotFound();
            }

            context.ItemAttractions.Remove(itemAttraction);
            context.SaveChanges();

            return Ok(itemAttraction);
        }
    }
}
