using Microsoft.AspNetCore.Mvc;
using s4.Data;

namespace s4.Models.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypesController : ControllerBase
    {
        private readonly DBContext context;

        public TransactionTypesController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllTransactionTypes()
        {
            var TransactionTypes = context.TransactionTypes.ToList();
            return Ok(TransactionTypes);
        }

        [HttpGet("ID")]
        public IActionResult Getattraction(int ID)
        {
            var attraction = context.TransactionTypes.Where(u => u.ID == ID).FirstOrDefault();
            return Ok(attraction);
        }

        [HttpPost]
        public IActionResult AddAttraction([FromBody] TransactionTypesDto TransactionTypesDto)
        {
            var AttractionEntity = new TransactionTypes() { Name = TransactionTypesDto.Name };

            context.TransactionTypes.Add(AttractionEntity);
            context.SaveChanges();

            return Ok(AttractionEntity);
        }

        [HttpDelete]
        public IActionResult DeleteAttraction(int ID)
        {
            var attraction = context.TransactionTypes.Find(ID);

            if (attraction is null)
            {
                return NotFound();
            }

            context.TransactionTypes.Remove(attraction);
            context.SaveChanges();

            return Ok(attraction);
        }
    }
}
