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
    public class AreasController : ControllerBase
    {
        private readonly DBContext context;

        public AreasController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllAreas()
        {
            var areas = context.Areas.ToList();
            return Ok(areas);
        }

        [HttpGet("areaID")]
        public IActionResult Getarea(int areaID)
        {
            var area = context.Areas.Where(u => u.ID == areaID).FirstOrDefault();
            return Ok(area);
        }


        [HttpPost]
        public IActionResult AddArea(AreaDto AreaDto)
        {
            var AreaEntity = new Areas() { Name = AreaDto.Name};

            context.Areas.Add(AreaEntity);
            context.SaveChanges();

            return Ok(AreaEntity);
        }

        [HttpPut("{AreaID}")]
        public IActionResult UpdateUserType(int AreaID, AreaDto AreaDto)
        {
            var AreaEntity = context.UserTypes.Find(AreaID);

            AreaEntity.Name = AreaDto.Name;

            context.SaveChanges();

            return Ok(AreaEntity);
        }

        [HttpDelete]
        public IActionResult DeleteAreas(int AreaID)
        {
            var Area = context.Areas.Find(AreaID);

            if (Area is null)
            {
                return NotFound();
            }

            context.Areas.Remove(Area);
            context.SaveChanges();

            return Ok(Area);
        }
    }
}
