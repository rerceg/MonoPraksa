using System;
using Example.Models.Common;

namespace Example.Models
{
    public class Receipt : IReceipt
    {
        public Guid Id
        { get; set; }

        public Guid BuyerId
        { get; set; }

        public DateTime Date
        { get; set; }

        public decimal Total
        { get; set; }

        public Receipt(Guid id, Guid buyerId, DateTime date, decimal total)
        {
            Id = id;
            BuyerId = buyerId;
            Date = date;
            Total = total;
        }
    }
}
