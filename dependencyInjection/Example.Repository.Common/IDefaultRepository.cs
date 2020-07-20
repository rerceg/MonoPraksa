using System;
using System.Collections.Generic;
using Example.Models;
using System.Threading.Tasks;

namespace Example.Repository.Common
{
    public interface IDefaultRepository
    {
        Task<List<Person>> SelectAllPersons();

        Task<Person> SelectPerson(Guid id);

        Task<List<Receipt>> SelectAllReceipts();

        Task<Receipt> SelectReceipt(Guid id);

        Task<Tuple<Person, List<Receipt>>> SelectPersonsReceipts(Guid id);

        Task InsertPerson(string name, string surname);

        Task UpdatePerson(Person person);

        Task DeletePerson(Guid id);
    }
}
