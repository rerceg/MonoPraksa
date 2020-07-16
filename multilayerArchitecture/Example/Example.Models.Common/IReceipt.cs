using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
