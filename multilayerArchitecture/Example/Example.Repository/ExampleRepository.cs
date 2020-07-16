﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Models.Common;
using Example.Models;
using System.Data.SqlClient;
using Example.Repository.Common;

namespace Example.Repository
{
    public class ExampleRepository : IExampleRepository 
    {
        static public SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebApiExample;Integrated Security=True");
        public List<Person> SelectAllPersons()
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
            return list;
        }

        public Person SelectPerson(Guid id)
        {
            Person person;
            var command = new SqlCommand(String.Format("SELECT * FROM Person WHERE Id = '{0}'", id), connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
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

        public List<Receipt> SelectAllReceipts()
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
            return list;
        }

        public Receipt SelectReceipt(Guid id)
        {
            Receipt receipt;
            var command = new SqlCommand(String.Format("SELECT * FROM Receipt WHERE Id = '{0}'", id), connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
           
            if (reader.HasRows)
            {
                reader.Read();
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

        public Tuple<Person, List<Receipt>> SelectPersonsReceipts(Guid id)
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
                return null;
            }

            reader.Close();
            connection.Close();
            tuple = new Tuple<Person, List<Receipt>>(person, list);
            return tuple;
        }

        public void InsertPerson(string name, string surname)
        {
            var command = new SqlCommand(String.Format("INSERT INTO Person(Name, Surname) VALUES('{0}','{1}')", name, surname), connection);
            connection.Open();
            command.ExecuteReader();
            connection.Close();
            return;
        }

        public void UpdatePerson(Person person)
        {
            connection.Open();
            var commandUpdate = new SqlCommand(String.Format("UPDATE Person SET Name = '{0}', Surname = '{1}' WHERE Id = '{2}';", person.Name, person.Surname, person.Id), connection);
            commandUpdate.ExecuteReader();
            connection.Close();
            return;
        }

        public void DeletePerson(Guid id)
        {
            connection.Open();
            var commandDelete = new SqlCommand(String.Format("DELETE FROM Person WHERE Id = '{0}'", id), connection);
            commandDelete.ExecuteReader();
            connection.Close();
            return;
        }
    }
}
