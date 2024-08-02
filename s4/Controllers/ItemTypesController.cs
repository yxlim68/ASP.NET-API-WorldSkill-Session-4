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
    public class ItemTypesController : ControllerBase
    {
        private readonly DBContext context;

        public ItemTypesController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllItemTypes()
        {
            var ItemTypes = context.ItemTypes.ToList();
            return Ok(ItemTypes);
        }

        [HttpGet("itemTypeID")]
        public IActionResult GetitemType(int itemTypeID)
        {
            var itemType = context.ItemTypes.Where(u => u.ID == itemTypeID).FirstOrDefault();
            return Ok(itemType);
        }

        [HttpPost]
        public IActionResult AddItemType(ItemTypeDto ItemTypeDto)
        {
            var ItemTypeEntity = new ItemTypes() { Name = ItemTypeDto.Name };

            context.ItemTypes.Add(ItemTypeEntity);
            context.SaveChanges();

            return Ok(ItemTypeEntity);
        }

        [HttpPut("{ItemTypeID}")]
        public IActionResult ItemTypesType(int ItemTypeID, ItemTypeDto ItemTypeDto)
        {
            var ItemTypeEntity = context.ItemTypes.Find(ItemTypeID);

            ItemTypeEntity.Name = ItemTypeDto.Name;

            context.SaveChanges();

            return Ok(ItemTypeEntity);
        }

        [HttpDelete]
        public IActionResult DeleteItemTypes(int ItemTypeID)
        {
            var ItemType = context.ItemTypes.Find(ItemTypeID);

            if (ItemType is null)
            {
                return NotFound();
            }

            context.ItemTypes.Remove(ItemType);
            context.SaveChanges();

            return Ok(ItemType);
        }
    }
}
