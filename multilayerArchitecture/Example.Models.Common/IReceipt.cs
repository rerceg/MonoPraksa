using System;

namespace Example.Models.Common
{
    public interface IReceipt
    {
        Guid Id
        { get; set; }

        Guid BuyerId
        { get; set; }

        DateTime Date
        { get; set; }

        decimal Total
        { get; set; }
    }
}
