using System;
using System.Collections.Generic;
using Example.Models;
using Example.Service.Common;
using Example.Repository.Common;
using System.Threading.Tasks;

namespace Example.Service
{
    public class DefaultService : IDefaultService
    {
        protected IDefaultRepository defaultRepository
        { get; private set; }

        public DefaultService(IDefaultRepository defaultRepository)
        {
            this.defaultRepository = defaultRepository;
        }

        public async Task<List<Person>> ReturnAllPersons()
        {
            return await defaultRepository.SelectAllPersons();
        }

        public async Task<Person> ReturnPersonById(Guid id)
        {
            return await defaultRepository.SelectPerson(id);
        }

        public async Task<List<Receipt>> ReturnAllReceipts()
        {
            return await defaultRepository.SelectAllReceipts();
        }

        public async Task<Receipt> ReturnReceiptById(Guid id)
        {
            return await defaultRepository.SelectReceipt(id);
        }

        public async Task<Tuple<Person, List<Receipt>>> ReturnPersonsReceipts(Guid id)
        {
            return await defaultRepository.SelectPersonsReceipts(id);
        }

        public async Task CreatePerson(string name, string surname)
        {
            await defaultRepository.InsertPerson(name, surname);
        }

        public async Task<bool> UpdatePersonById(Guid id, Person person)
        {
            Person selectPerson = await defaultRepository.SelectPerson(id);
            if (selectPerson == null)
            {
                return false;
            }
            person.Id = id;
            await defaultRepository.UpdatePerson(person);
            return true;
        }

        public async Task<Tuple<int, string>> RemovePersonById(Guid id)
        {
            Person person = await defaultRepository.SelectPerson(id);
            if(person == null)
            {
                return new Tuple<int, string>(-1, "");
            }
            await defaultRepository.DeletePerson(id);
            return new Tuple<int, string>(1, person.Name + " " + person.Surname);
        }
    }
}
