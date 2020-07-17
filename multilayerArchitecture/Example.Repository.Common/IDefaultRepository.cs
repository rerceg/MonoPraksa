using System;
using System.Collections.Generic;
using Example.Models;

namespace Example.Repository.Common
{
    public interface IDefaultRepository
    {
        List<Person> SelectAllPersons();

        Person SelectPerson(Guid id);

        List<Receipt> SelectAllReceipts();

        Receipt SelectReceipt(Guid id);

        Tuple<Person, List<Receipt>> SelectPersonsReceipts(Guid id);

        void InsertPerson(string name, string surname);

        void UpdatePerson(Person person);

        void DeletePerson(Guid id);
    }
}
