using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Models.Common
{
    public interface IPerson
    {
        string Name
        { get; set; }

        string Surname
        { get; set; }

        Guid Id
        { get; set; }
    }
}
