using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Models;

namespace Example.Service.Common
{
    public interface IExampleService
    {
        List<Person> ReturnAllPersons();
        
        Person ReturnPersonById(Guid id);

        List<Receipt> ReturnAllReceipts();

        Receipt ReturnReceiptById(Guid id);

        Tuple<Person, List<Receipt>> ReturnPersonsReceipts(Guid id);

        void CreatePerson(string name, string surname);

        bool UpdatePersonById(Guid id, Person person);

        Tuple<int, string> RemovePersonById(Guid id);
    }
}
