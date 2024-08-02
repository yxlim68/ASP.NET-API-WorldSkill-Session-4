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
    public class ItemAmenitiesController : ControllerBase
    {
        private readonly DBContext context;

        public ItemAmenitiesController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllItemAmenities()
        {
            var ItemAmenities = context.ItemAmenities.ToList();
            return Ok(ItemAmenities);
        }

        [HttpGet("itemAmenitiesID")]
        public IActionResult GetitemAmenities(int itemAmenitiesID)
        {
            var itemAmenities = context.ItemAmenities.Where(u => u.ID == itemAmenitiesID).FirstOrDefault();
            return Ok(itemAmenities);
        }

        [HttpGet("itemID")]
        public IActionResult GetitemAmenitiesByItemID(int itemsID)
        {
            var itemAmenities = context.ItemAmenities.Where(u => u.ItemID == itemsID).ToList();
            return Ok(itemAmenities);
        }

        [HttpPost]
        public IActionResult AddItemAmenities([FromQuery] long amenityID, [FromQuery] long itemID,[FromBody] ItemAmentiesDto ItemAmentiesDto)
        {
            var ItemAmenitiesEntity = new ItemAmenities() { AmenityID = amenityID, ItemID = itemID };

            context.ItemAmenities.Add(ItemAmenitiesEntity);
            context.SaveChanges();

            return Ok(ItemAmenitiesEntity);
        }

        [HttpPut("{ItemAmenitiesID}")]
        public IActionResult UpdateItemAmenities(int ItemAmenitiesID, ItemAmentiesDto ItemAmenitiesDto)
        {
            var ItemAmenitiesEntity = context.ItemAmenities.Find(ItemAmenitiesID);

            ItemAmenitiesEntity.AmenityID = ItemAmenitiesDto.AmenityID;
            ItemAmenitiesEntity.ItemID = ItemAmenitiesDto.ItemID;

            context.SaveChanges();

            return Ok(ItemAmenitiesEntity);
        }

        [HttpDelete]
        public IActionResult DeleteItemAmenities(int ItemAmenitiesID)
        {
            var ItemAmenities = context.ItemAmenities.Find(ItemAmenitiesID);

            if (ItemAmenities is null)
            {
                return NotFound();
            }

            context.ItemAmenities.Remove(ItemAmenities);
            context.SaveChanges();

            return Ok(ItemAmenities);
        }
    }
}
