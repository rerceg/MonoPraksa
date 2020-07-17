using System;

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
