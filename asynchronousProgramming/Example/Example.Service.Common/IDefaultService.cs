using System;
using System.Collections.Generic;
using Example.Models;
using System.Threading.Tasks;

namespace Example.Service.Common
{
    public interface IDefaultService
    {
        Task<List<Person>> ReturnAllPersons();
        
        Task<Person> ReturnPersonById(Guid id);

        Task<List<Receipt>> ReturnAllReceipts();

        Task<Receipt> ReturnReceiptById(Guid id);

        Task<Tuple<Person, List<Receipt>>> ReturnPersonsReceipts(Guid id);

        Task CreatePerson(string name, string surname);

        Task<bool> UpdatePersonById(Guid id, Person person);

        Task<Tuple<int, string>> RemovePersonById(Guid id);
    }
}
