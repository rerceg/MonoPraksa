using System;
using System.Collections.Generic;
using Example.Models;
using Example.Service.Common;
using Example.Repository;
using System.Threading.Tasks;

namespace Example.Service
{
    public class DefaultService : IDefaultService
    {
        static ExampleRepository exampleRepository = new ExampleRepository();

        public async Task<List<Person>> ReturnAllPersons()
        {
            return await exampleRepository.SelectAllPersons();
        }

        public async Task<Person> ReturnPersonById(Guid id)
        {
            return await exampleRepository.SelectPerson(id);
        }

        public async Task<List<Receipt>> ReturnAllReceipts()
        {
            return await exampleRepository.SelectAllReceipts();
        }

        public async Task<Receipt> ReturnReceiptById(Guid id)
        {
            return await exampleRepository.SelectReceipt(id);
        }

        public async Task<Tuple<Person, List<Receipt>>> ReturnPersonsReceipts(Guid id)
        {
            return await exampleRepository.SelectPersonsReceipts(id);
        }

        public async Task CreatePerson(string name, string surname)
        {
            await exampleRepository.InsertPerson(name, surname);
        }

        public async Task<bool> UpdatePersonById(Guid id, Person person)
        {
            Person selectPerson = await exampleRepository.SelectPerson(id);
            if (selectPerson == null)
            {
                return false;
            }
            person.Id = id;
            await exampleRepository.UpdatePerson(person);
            return true;
        }

        public async Task<Tuple<int, string>> RemovePersonById(Guid id)
        {
            Person person = await exampleRepository.SelectPerson(id);
            if(person == null)
            {
                return new Tuple<int, string>(-1, "");
            }
            await exampleRepository.DeletePerson(id);
            return new Tuple<int, string>(1, person.Name + " " + person.Surname);
        }
    }
}
