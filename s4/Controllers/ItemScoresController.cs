using Microsoft.AspNetCore.Mvc;
using s4.Data;

namespace s4.Models.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemScoresController : ControllerBase
    {
        private readonly DBContext context;

        public ItemScoresController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllItemScores()
        {
            var ItemScores = context.ItemScores.ToList();
            return Ok(ItemScores);
        }

        [HttpGet("ID")]
        public IActionResult Getattraction(int ID)
        {
            var attraction = context.ItemScores.Where(u => u.ID == ID).FirstOrDefault();
            return Ok(attraction);
        }

        [HttpPost]
        public IActionResult AddAttraction([FromQuery] int ItemID, [FromQuery] int ScoreID, [FromQuery] int UserID, [FromBody] ItemScoresDto ItemScoresDto)
        {
            var AttractionEntity = new ItemScores() { ItemID = ItemID, UserID = ItemScoresDto.UserID, ScoreID = ItemScoresDto.ScoreID, Value = ItemScoresDto.Value};

            context.ItemScores.Add(AttractionEntity);
            context.SaveChanges();

            return Ok(AttractionEntity);
        }

        [HttpDelete]
        public IActionResult DeleteAttraction(int ID)
        {
            var attraction = context.ItemScores.Find(ID);

            if (attraction is null)
            {
                return NotFound();
            }

            context.ItemScores.Remove(attraction);
            context.SaveChanges();

            return Ok(attraction);
        }
    }
}
