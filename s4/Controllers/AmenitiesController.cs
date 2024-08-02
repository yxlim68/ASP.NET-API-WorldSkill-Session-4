using s4.Data;
using s4.Models;
using s4.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace s4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly DBContext context;

        public AmenitiesController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllAmenities()
        {
            var amenities = context.Amenities.ToList();
            return Ok(amenities);
        }

        [HttpGet("amenitiesID")]
        public IActionResult GetAmenities(int amentiesID)
        {
            var Amenities = context.Amenities.Where(a => a.ID == amentiesID).FirstOrDefault();
            return Ok(Amenities);
        }

        [HttpPost]
        public IActionResult AddAmenities(AmenitiesDto amenitiesDto)
        {
            var amenitiesEntity = new Amenities() { Name = amenitiesDto.Name, IconName = amenitiesDto.IconName };

            context.Amenities.Add(amenitiesEntity);
            context.SaveChanges();

            return Ok(amenitiesEntity);
        }

        [HttpPut("{AmenityID}")]
        public IActionResult UpdateAmenity(int AmenityID, AmenitiesDto AmenitiesDto)
        {
            var AmenitiesEntity = context.UserTypes.Find(AmenityID);

            AmenitiesEntity.Name = AmenitiesDto.Name;

            context.SaveChanges();

            return Ok(AmenitiesEntity);
        }

        [HttpDelete]
        public IActionResult DeleteAmenitiess(long AmenitiesID)
        {
            var Amenities = context.Amenities.Find(AmenitiesID);

            if (Amenities is null)
            {
                return NotFound();
            }

            context.Amenities.Remove(Amenities);
            context.SaveChanges();

            return Ok(Amenities);
        }
    }
}
