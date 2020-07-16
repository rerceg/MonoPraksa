using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Example.Models.Common;
using Example.Service.Common;
using Example.Models;
using Example.Service;
using AutoMapper;

namespace ExampleWebAPI.Controllers
{
    public class DefaultController : ApiController
    {
        static ExampleService exampleService = new ExampleService();

        [Route("api/Default/Persons")]
        public HttpResponseMessage GetAllPersons()
        {
            List<Person> list = exampleService.ReturnAllPersons();
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }
        
        [Route("api/Default/Persons")]
        public HttpResponseMessage GetPersonById(Guid id)
        {
            Person person = exampleService.ReturnPersonById(id);
            if(person == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no person with Id: {0}", id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, person);
        }

        [Route("api/Default/Receipts")]
        public HttpResponseMessage GetAllReceipts()
        {
            List<Receipt> list = exampleService.ReturnAllReceipts();
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        [Route("api/Default/Receipts")]
        public HttpResponseMessage GetReceiptById(Guid id)
        {
            Receipt receipt = exampleService.ReturnReceiptById(id);
            if(receipt == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no receipt with Id: {0}", id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, receipt);
        }

        [Route("api/Default/PersonsReceipts")]
        public HttpResponseMessage GetPersonsReceiptsByPersonsId(Guid id)
        {
            Tuple<Person, List<Receipt>> tuple = exampleService.ReturnPersonsReceipts(id);
            if(tuple == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There are no receipts for person with Id: {0}", id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, tuple);
        }

        [Route("api/Default/Persons")]
        public HttpResponseMessage PostPerson(string name, string surname)
        {
            exampleService.CreatePerson(name, surname);
            return Request.CreateResponse(HttpStatusCode.OK, String.Format("{0} {1} inserted in the database",name, surname));
        }

        [Route("api/Default/Persons")]
        public HttpResponseMessage PutById(Guid id, Person person)
        {
            bool flag = exampleService.UpdatePersonById(id, person);
            if (!flag)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no person with Id: {0}", id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, person);
        }

        [Route("api/Default/Persons")]
        public HttpResponseMessage DeleteById(Guid id)
        {
            Tuple<int, string> tuple = exampleService.RemovePersonById(id);
            if(tuple.Item1 == -1)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no person with Id: {0}", id));
            }
            return Request.CreateResponse(HttpStatusCode.OK, String.Format("{0} removed from the list", tuple.Item2));
        }
    }
}
