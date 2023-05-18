using System;
using System.Collections.Generic;
using Covid19ManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Covid19ManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly string connectionString = "Server=localhost;Port=3306;Database=coronadatabase;Uid=root;Pwd=password;";

        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAllPersons()
        {
            List<Person> persons = new List<Person>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Person";

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Person person = new Person
                        {
                            PersonId = reader.GetInt32("PersonId"),
                            FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString("LastName"),
                            ID = reader.GetString("ID"),
                            DateOfBirth = reader.GetDateTime("DateOfBirth"),
                            Telephone = reader.GetString("Telephone"),
                            MobilePhone = reader.GetString("MobilePhone"),
                            City = reader.GetString("City"),
                            Street = reader.GetString("Street"),
                            NumberStreet = reader.GetString("NumberStreet")
                        };

                        persons.Add(person);
                    }
                }
            }

            return Ok(persons);
        }

        [HttpPost]
        public ActionResult<Person> InsertPerson(Person person)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Person (PersonId, FirstName, LastName, ID, DateOfBirth, Telephone, MobilePhone, City, Street, NumberStreet) " +
                               "VALUES (@PersonId, @FirstName, @LastName, @ID, @DateOfBirth, @Telephone, @MobilePhone, @City, @Street, @NumberStreet)";

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@PersonId", person.PersonId);
                command.Parameters.AddWithValue("@FirstName", person.FirstName);
                command.Parameters.AddWithValue("@LastName", person.LastName);
                command.Parameters.AddWithValue("@ID", person.ID);
                command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                command.Parameters.AddWithValue("@Telephone", person.Telephone);
                command.Parameters.AddWithValue("@MobilePhone", person.MobilePhone);
                command.Parameters.AddWithValue("@City", person.City);
                command.Parameters.AddWithValue("@Street", person.Street);
                command.Parameters.AddWithValue("@NumberStreet", person.NumberStreet);

                command.ExecuteNonQuery();
            }

            return CreatedAtAction(nameof(GetAllPersons), new { id = person.PersonId }, person);
        }
    }

}
