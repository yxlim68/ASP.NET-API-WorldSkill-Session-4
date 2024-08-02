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
    public class AttractionsController : ControllerBase
    {
        private readonly DBContext context;

        public AttractionsController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllAttractions()
        {
            var Attractions = context.Attractions.ToList();
            return Ok(Attractions);
        }

        [HttpGet("attractionID")]
        public IActionResult Getattraction(int attractionID)
        {
            var attraction = context.Attractions.Where(u => u.ID == attractionID).FirstOrDefault();
            return Ok(attraction);
        }

        [HttpGet("area/{areaID}")]
        public IActionResult GetAttractionsByArea(int areaID)
        {
            var attractions = context.Attractions.Where(a => a.AreaID == areaID).ToList();
            return Ok(attractions);
        }

        [HttpPost]
        public IActionResult AddAttraction([FromQuery] int areaID, [FromBody] AttractionDto AttractionDto)
        {
            var AttractionEntity = new Attractions() { Name = AttractionDto.Name, Address = AttractionDto.Address, AreaID = areaID };

            context.Attractions.Add(AttractionEntity);
            context.SaveChanges();

            return Ok(AttractionEntity);
        }

        [HttpPut("{AttractionID}")]
        public IActionResult UpdateAttraction(int AttractionID, AttractionDto AttractionDto)
        {
            var AttractionEntity = context.Attractions.Find(AttractionID);

            AttractionEntity.Name = AttractionDto.Name;
            AttractionEntity.AreaID = AttractionDto.AreaID;
            AttractionEntity.Address = AttractionDto.Address;

            context.SaveChanges();

            return Ok(AttractionEntity);
        }

        [HttpDelete]
        public IActionResult DeleteAttraction(int AttractionID)
        {
            var attraction = context.Attractions.Find(AttractionID);

            if(attraction is null)
            {
                return NotFound();
            }

            context.Attractions.Remove(attraction);
            context.SaveChanges();

            return Ok(attraction);
        }
    }
}
