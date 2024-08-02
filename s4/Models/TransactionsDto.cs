﻿namespace s4.Models.Entities
{
    public class TransactionsDto
    {
        public long UserID { get; set; }
        public long TransactionTypeID { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string GatewayReturnID { get; set; }
    }
}
