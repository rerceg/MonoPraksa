using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Example.Models;
using Example.Service.Common;
using AutoMapper;
using System.Threading.Tasks;

namespace ExampleWebAPI.Controllers
{
    public class DefaultController : ApiController
    {
        protected IDefaultService defaultService
        { get; private set; }

        static MapperConfiguration restPersonMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Person, RestPerson>());
        static MapperConfiguration restReceiptMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Receipt, RestReceipt>());

        public DefaultController(IDefaultService defaultService)
        {
            this.defaultService = defaultService;
        }

        [Route("api/Default/Persons")]
        public async Task<HttpResponseMessage> GetAllPersons()
        {
            var mapper = restPersonMapperConfig.CreateMapper();
            List<RestPerson> restPersonList = mapper.Map<List<RestPerson>>(await defaultService.ReturnAllPersons());
            return Request.CreateResponse(HttpStatusCode.OK, restPersonList);
        }
        
        [Route("api/Default/Persons")]
        public async Task<HttpResponseMessage> GetPersonById(Guid id)
        {
            var mapper = restPersonMapperConfig.CreateMapper();
            RestPerson person = mapper.Map<RestPerson>(await defaultService.ReturnPersonById(id));
            if (person == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no person with Id: {0}", id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, person);
        }

        [Route("api/Default/Receipts")]
        public async Task<HttpResponseMessage> GetAllReceipts()
        {
            var mapper = restReceiptMapperConfig.CreateMapper();
            List<RestReceipt> list = mapper.Map<List<RestReceipt>>(await defaultService.ReturnAllReceipts());
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        [Route("api/Default/Receipts")]
        public async Task<HttpResponseMessage> GetReceiptById(Guid id)
        {
            var mapper = restReceiptMapperConfig.CreateMapper();
            RestReceipt receipt = mapper.Map<RestReceipt>(await defaultService.ReturnReceiptById(id));
            if (receipt == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no receipt with Id: {0}", id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, receipt);
        }

        [Route("api/Default/PersonsReceipts")]
        public async Task<HttpResponseMessage> GetPersonsReceiptsByPersonsId(Guid id)
        {
            Tuple<Person, List<Receipt>> tuple = await defaultService.ReturnPersonsReceipts(id);
            if (tuple == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There are no receipts for person with Id: {0}", id));
            }

            var personMapper = restPersonMapperConfig.CreateMapper();
            var receiptMapper = restReceiptMapperConfig.CreateMapper();
            RestPerson restPerson = personMapper.Map<RestPerson>(tuple.Item1);
            List<RestReceipt> restReceipts = receiptMapper.Map<List<RestReceipt>>(tuple.Item2);
            Tuple<RestPerson, List<RestReceipt>> restTuple = new Tuple<RestPerson, List<RestReceipt>>(restPerson, restReceipts);
            return Request.CreateResponse(HttpStatusCode.OK, restTuple);
        }

        [Route("api/Default/Persons")]
        public async Task<HttpResponseMessage> PostPerson(string name, string surname)
        {
            await defaultService.CreatePerson(name, surname);
            return Request.CreateResponse(HttpStatusCode.OK, String.Format("{0} {1} inserted in the database",name, surname));
        }

        [Route("api/Default/Persons")]
        public async Task<HttpResponseMessage> PutById(Guid id, RestPerson restPerson)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RestPerson, Person>());
            var mapper = config.CreateMapper();
            Person person = mapper.Map<Person>(restPerson);
            bool flag = await defaultService.UpdatePersonById(id, person);
            if (!flag)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no person with Id: {0}", id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, restPerson); 
        }

        [Route("api/Default/Persons")]
        public async Task<HttpResponseMessage> DeleteById(Guid id)
        {
            Tuple<int, string> tuple = await defaultService.RemovePersonById(id);
            if(tuple.Item1 == -1)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no person with Id: {0}", id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, String.Format("{0} removed from the list", tuple.Item2));
        }
    }

    public class RestPerson
    {
        public string Name
        { get; set; }

        public string Surname
        { get; set; }

        public RestPerson() { }

        public RestPerson(string name, string surname) 
        {
            Name = name;
            Surname = surname;
        }
    }

    public class RestReceipt
    {
        public DateTime Date
        { get; set; }

        public decimal Total
        { get; set; }

        public RestReceipt(DateTime date, decimal total)
        {
            Date = date;
            Total = total;
        }
    }
}
