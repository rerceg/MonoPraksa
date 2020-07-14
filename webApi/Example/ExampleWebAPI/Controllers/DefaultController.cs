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
        static public List<Person> list = new List<Person>() { 
            new Person("Roko", "Erceg", 1),
            new Person("Marko", "Markovic", 2),
            new Person("Grgur", "Grgic", 3)
        };
        public HttpResponseMessage GetAll()
        {
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        public HttpResponseMessage GetById(int id)
        {
            int position = -1;
            int counter = 0;
            foreach (Person person in list)
            {
                if (person.Id == id) { 
                    position = counter;
                    break;
                }
                counter++;
            }
            if(position == -1)
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no person with Id: {0}",id));
            return Request.CreateResponse(HttpStatusCode.OK, list[position]);
        }

        public HttpResponseMessage Post(Person person)
        {
            list.Add(person);
            return Request.CreateResponse(HttpStatusCode.OK, String.Format("{0} {1} inserted in the list",person.Name, person.Surname));
        }

        public HttpResponseMessage PutById(string name, string surname, int id)
        {
            int position = -1;
            int counter = 0;
            foreach (Person person in list)
            {
                if (person.Id == id)
                {
                    position = counter;
                    break;
                }
                counter++;
            }
            if (position == -1)
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no person with Id: {0}", id));
            list[position].Name = name;
            list[position].Surname = surname;
            return Request.CreateResponse(HttpStatusCode.OK, list[position]);
        }

        public HttpResponseMessage DeleteById(int id)
        {
            int position = -1;
            int counter = 0;
            foreach (Person person in list)
            {
                if (person.Id == id)
                {
                    position = counter;
                    break;
                }
                counter++;
            }
            if (position == -1)
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no person with Id: {0}", id));
            string name = list[position].Name;
            string surname = list[position].Surname;
            list.RemoveAt(position);
            return Request.CreateResponse(HttpStatusCode.OK, String.Format("{0} {1} removed from the list", name, surname));
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
