using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Models.Common;
using Example.Models;
using Example.Repository.Common;
using Example.Service.Common;
using Example.Repository;

namespace Example.Service
{
    public class ExampleService : IExampleService
    {
        static ExampleRepository exampleRepository = new ExampleRepository();

        public List<Person> ReturnAllPersons()
        {
            return exampleRepository.SelectAllPersons();
        }

        public Person ReturnPersonById(Guid id)
        {
            return exampleRepository.SelectPerson(id);
        }

        public List<Receipt> ReturnAllReceipts()
        {
            return exampleRepository.SelectAllReceipts();
        }

        public Receipt ReturnReceiptById(Guid id)
        {
            return exampleRepository.SelectReceipt(id);
        }

        public Tuple<Person, List<Receipt>> ReturnPersonsReceipts(Guid id)
        {
            return exampleRepository.SelectPersonsReceipts(id);
        }

        public void CreatePerson(string name, string surname)
        {
            exampleRepository.InsertPerson(name, surname);
        }

        public bool UpdatePersonById(Guid id, Person person)
        {
            Person selectPerson = exampleRepository.SelectPerson(id);
            if (selectPerson == null)
            {
                return false;
            }
            exampleRepository.UpdatePerson(person);
            return true;
        }

        public Tuple<int, string> RemovePersonById(Guid id)
        {
            Person person = exampleRepository.SelectPerson(id);
            if(person == null)
            {
                return new Tuple<int, string>(-1, "");
            }
            exampleRepository.DeletePerson(id);
            return new Tuple<int, string>(1, person.Name + " " + person.Surname);
        }
    }
}
