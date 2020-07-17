using System;
using System.Collections.Generic;
using Example.Models;
using System.Data.SqlClient;
using Example.Repository.Common;
using System.Threading.Tasks;

namespace Example.Repository
{
    public class ExampleRepository : IDefaultRepository 
    {
        static public SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebApiExample;Integrated Security=True");
        public async Task<List<Person>> SelectAllPersons()
        {
            var list = new List<Person>();
            var command = new SqlCommand("SELECT * FROM Person", connection);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new Person(reader.GetString(1), reader.GetString(2), reader.GetGuid(0)));
                }
            }

            reader.Close();
            connection.Close();
            return list;
        }

        public async Task<Person> SelectPerson(Guid id)
        {
            Person person;
            var command = new SqlCommand(String.Format("SELECT * FROM Person WHERE Id = '{0}'", id), connection);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                person = new Person(reader.GetString(1), reader.GetString(2), reader.GetGuid(0));
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }

            reader.Close();
            connection.Close();
            return person;
        }

        public async Task<List<Receipt>> SelectAllReceipts()
        {
            var list = new List<Receipt>();
            var command = new SqlCommand("SELECT * FROM Receipt", connection);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    list.Add(new Receipt(reader.GetGuid(0), reader.GetGuid(1), reader.GetDateTime(2), reader.GetDecimal(3)));
                }
            }

            reader.Close();
            connection.Close();
            return list;
        }

        public async Task<Receipt> SelectReceipt(Guid id)
        {
            Receipt receipt;
            var command = new SqlCommand(String.Format("SELECT * FROM Receipt WHERE Id = '{0}'", id), connection);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
           
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                receipt = new Receipt(reader.GetGuid(0), reader.GetGuid(1), reader.GetDateTime(2), reader.GetDecimal(3));
            }
            else
            {
                reader.Close();
                connection.Close();
                return null;
            }

            reader.Close();
            connection.Close();
            return receipt;
        }

        public async Task<Tuple<Person, List<Receipt>>> SelectPersonsReceipts(Guid id)
        {
            Tuple<Person, List<Receipt>> tuple;
            var list = new List<Receipt>();
            Person person = new Person();
            var command = new SqlCommand(String.Format(
                "SELECT r.Id, p.Name, p.Surname, r.Date, r.Total " +
                "FROM Receipt r JOIN Person p ON r.BuyerId = p.Id " +
                "WHERE r.BuyerId = '{0}'", id), connection);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                bool flag = true;
                while (await reader.ReadAsync())
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
                return null;
            }

            reader.Close();
            connection.Close();
            tuple = new Tuple<Person, List<Receipt>>(person, list);
            return tuple;
        }

        public async Task InsertPerson(string name, string surname)
        {
            var command = new SqlCommand(String.Format("INSERT INTO Person(Name, Surname) VALUES('{0}','{1}')", name, surname), connection);
            connection.Open();
            await command.ExecuteReaderAsync();
            connection.Close();
            return;
        }

        public async Task UpdatePerson(Person person)
        {
            connection.Open();
            var commandUpdate = new SqlCommand(String.Format("UPDATE Person SET Name = '{0}', Surname = '{1}' WHERE Id = '{2}';", person.Name, person.Surname, person.Id), connection);
            await commandUpdate.ExecuteReaderAsync();
            connection.Close();
            return;
        }

        public async Task DeletePerson(Guid id)
        {
            connection.Open();
            var commandDelete = new SqlCommand(String.Format("DELETE FROM Person WHERE Id = '{0}'", id), connection);
            await commandDelete.ExecuteReaderAsync();
            connection.Close();
            return;
        }
    }
}
