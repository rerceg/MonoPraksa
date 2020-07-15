using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;

namespace ExampleWebAPI.Controllers
{
    public class DefaultController : ApiController
    {
        static public SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebApiExample;Integrated Security=True");
        [Route("api/Default/Persons")]
        public HttpResponseMessage GetAllPersons()
        {
            var list = new List<Person>();
            var command = new SqlCommand("SELECT * FROM Person", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
    
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    list.Add(new Person(reader.GetString(1), reader.GetString(2), reader.GetGuid(0)));
                }
            }
            
            reader.Close();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }
        
        [Route("api/Default/Persons")]
        public HttpResponseMessage GetPersonById(Guid id)
        {
            
            var command = new SqlCommand(String.Format("SELECT * FROM Person WHERE Id = '{0}'", id), connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Person person;
            if (reader.HasRows)
            {
                reader.Read();
                person = new Person(reader.GetString(1), reader.GetString(2), reader.GetGuid(0));
            }
            else
            {
                reader.Close();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no person with Id: {0}", id));
            }

            reader.Close();
            connection.Close();

            return Request.CreateResponse(HttpStatusCode.OK, person);
        }

        [Route("api/Default/Receipts")]
        public HttpResponseMessage GetAllReceipts()
        {
            var list = new List<Receipt>();
            var command = new SqlCommand("SELECT * FROM Receipt", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    list.Add(new Receipt(reader.GetGuid(0), reader.GetGuid(1), reader.GetDateTime(2), reader.GetDecimal(3)));
                }
            }

            reader.Close();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        [Route("api/Default/Receipts")]
        public HttpResponseMessage GetReceiptById(Guid id)
        {

            var command = new SqlCommand(String.Format("SELECT * FROM Receipt WHERE Id = '{0}'", id), connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Receipt receipt;
            if (reader.HasRows)
            {
                reader.Read();
                receipt = new Receipt(reader.GetGuid(0), reader.GetGuid(1), reader.GetDateTime(2), reader.GetDecimal(3));
            }
            else
            {
                reader.Close();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no receipt with Id: {0}", id));
            }

            reader.Close();
            connection.Close();

            return Request.CreateResponse(HttpStatusCode.OK, receipt);
        }

        [Route("api/Default/PersonsReceipts")]
        public HttpResponseMessage GetPersonsReceiptsByPersonsId(Guid id)
        {
            Tuple<Person, List<Receipt>> tuple;
            var list = new List<Receipt>();
            Person person = new Person();
            var command = new SqlCommand(String.Format(
                "SELECT r.Id, p.Name, p.Surname, r.Date, r.Total " + 
                "FROM Receipt r JOIN Person p ON r.BuyerId = p.Id " + 
                "WHERE r.BuyerId = '{0}'", id), connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                bool flag = true;
                while (reader.Read())
                {
                    list.Add(new Receipt(reader.GetGuid(0), id, reader.GetDateTime(3),
                        reader.GetDecimal(4)));
                    if (flag) 
                    {
                        person.Name = reader.GetString(1);
                        person.Surname = reader.GetString(2);
                        person.Id = id;
                        flag = false;
                    } 
                }
            }
            else
            {
                reader.Close();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no receipts for person with Id: {0}", id));
            }

            reader.Close();
            connection.Close();
            tuple = new Tuple<Person, List<Receipt>>(person, list);
            return Request.CreateResponse(HttpStatusCode.OK, tuple);
        }

        [Route("api/Default/Persons")]
        public HttpResponseMessage PostPerson(string name, string surname)
        {
            var command = new SqlCommand(String.Format("INSERT INTO Person(Name, Surname) VALUES('{0}','{1}')", name, surname), connection);
            connection.Open();
            command.ExecuteReader();
            connection.Close();
            return Request.CreateResponse(HttpStatusCode.OK, String.Format("{0} {1} inserted in the list",name, surname));
        }

        [Route("api/Default/Persons")]
        public HttpResponseMessage PutById(Person person)
        {
            var commandSelect = new SqlCommand(String.Format("SELECT * FROM Person WHERE Id = '{0}'", person.Id), connection);
            connection.Open();
            SqlDataReader reader = commandSelect.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                var commandUpdate = new SqlCommand(String.Format("UPDATE Person SET Name = '{0}', Surname = '{1}' WHERE Id = '{2}';", person.Name, person.Surname, person.Id), connection);
                commandUpdate.ExecuteReader();
            }
            else
            {
                reader.Close();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no person with Id: {0}", person.Id));
            }
            connection.Close();

            return Request.CreateResponse(HttpStatusCode.OK, person);
        }

        [Route("api/Default/Persons")]
        public HttpResponseMessage DeleteById(Guid id)
        {
            string name, surname;
            var commandSelect = new SqlCommand(String.Format("SELECT * FROM Person WHERE Id = '{0}'", id), connection);
            connection.Open();
            SqlDataReader reader = commandSelect.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                name = reader.GetString(1);
                surname = reader.GetString(2);
                reader.Close();
                var commandDelete = new SqlCommand(String.Format("DELETE FROM Person WHERE Id = '{0}'", id), connection);
                commandDelete.ExecuteReader();
            }
            else
            {
                reader.Close();
                connection.Close();
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format("There is no person with Id: {0}", id));
            }
            connection.Close();

            return Request.CreateResponse(HttpStatusCode.OK, String.Format("{0} {1} removed from the list", name, surname));
        }
    }

    public class Person
    {
        public string Name
        { get; set; }

        public string Surname
        { get; set; }

        public Guid Id
        { get; set; }
        public Person() { }
        public Person(string name, string surname, Guid id)
        {
            Name = name;
            Surname = surname;
            Id = id;
        }
    }

    public class Receipt
    {
        public Guid Id
        { get; set; }

        public Guid BuyerId
        { get; set; }

        public DateTime Date
        { get; set; }

        public decimal  Total
        { get; set; }

        public Receipt(Guid id, Guid buyerId, DateTime date, decimal total)
        {
            Id = id;
            BuyerId = buyerId;
            Date = date;
            Total = total;
        }
    }
}
