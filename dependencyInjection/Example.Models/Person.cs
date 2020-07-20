using System;
using Example.Models.Common;

namespace Example.Models
{
    public class Person : IPerson
    {
        public string Name
        { get; set; }

        public string Surname
        { get; set; }

        public Guid Id
        { get; set; }
        
        public Person() { }
        public Person(string name, string surname, Guid id)
        {
            Name = name;
            Surname = surname;
            Id = id;
        }
    }
}
