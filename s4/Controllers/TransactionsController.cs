using Microsoft.AspNetCore.Mvc;
using s4.Data;

namespace s4.Models.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly DBContext context;

        public TransactionsController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            var Transactions = context.Transactions.ToList();
            return Ok(Transactions);
        }

        [HttpGet("ID")]
        public IActionResult Getattraction(int ID)
        {
            var attraction = context.Transactions.Where(u => u.ID == ID).FirstOrDefault();
            return Ok(attraction);
        }

        [HttpPost]
        public IActionResult AddAttraction([FromQuery] int TransactionTypeID, [FromBody] TransactionsDto TransactionsDto)
        {
            var AttractionEntity = new Transactions() { TransactionTypeID = TransactionTypeID, UserID = TransactionsDto.UserID, TransactionDate = TransactionsDto.TransactionDate, Amount = TransactionsDto.Amount, GatewayReturnID = TransactionsDto.GatewayReturnID };

            context.Transactions.Add(AttractionEntity);
            context.SaveChanges();

            return Ok(AttractionEntity);
        }

        [HttpDelete]
        public IActionResult DeleteAttraction(int ID)
        {
            var attraction = context.Transactions.Find(ID);

            if (attraction is null)
            {
                return NotFound();
            }

            context.Transactions.Remove(attraction);
            context.SaveChanges();

            return Ok(attraction);
        }
    }
}
