using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExampleWebAPI.Controllers
{
    public class DefaultController : ApiController
    {
        public List<Person> list = new List<Person>() { 
            new Person("Roko", "Erceg", 1),
            new Person("Marko", "Markovic", 2),
            new Person("Grgur", "Grgic", 3)
        };
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        public HttpResponseMessage Get(int id)
        {
            int position = -1;
            int counter = 0;
            foreach (var el in list)
            {
                if (el.Id == id) { 
                    position = counter;
                    break;
                }
                counter++;
            }
            if(position == -1)
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            return Request.CreateResponse(HttpStatusCode.OK, list[position]);
        }

        public HttpResponseMessage Post(Person person)
        {
            list.Add(person);
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }
    }

    public class Person
    {
        public string Name
        { get; set; }

        public string Surname
        { get; set; }

        public int Id
        { get; set; }
        public Person(string name, string surname, int id)
        {
            Name = name;
            Surname = surname;
            Id = id;
        }
    }
}
