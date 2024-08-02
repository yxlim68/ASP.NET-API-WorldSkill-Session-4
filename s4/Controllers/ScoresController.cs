using Microsoft.AspNetCore.Mvc;
using s4.Data;

namespace s4.Models.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly DBContext context;

        public ScoresController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllScores()
        {
            var Scores = context.Scores.ToList();
            return Ok(Scores);
        }

        [HttpGet("ID")]
        public IActionResult Getattraction(int ID)
        {
            var attraction = context.Scores.Where(u => u.ID == ID).FirstOrDefault();
            return Ok(attraction);
        }

        [HttpPost]
        public IActionResult AddAttraction([FromBody] ScoresDto ScoresDto)
        {
            var AttractionEntity = new Scores() { Name = ScoresDto.Name };

            context.Scores.Add(AttractionEntity);
            context.SaveChanges();

            return Ok(AttractionEntity);
        }

        [HttpDelete]
        public IActionResult DeleteAttraction(int ID)
        {
            var attraction = context.Scores.Find(ID);

            if (attraction is null)
            {
                return NotFound();
            }

            context.Scores.Remove(attraction);
            context.SaveChanges();

            return Ok(attraction);
        }
    }
}
