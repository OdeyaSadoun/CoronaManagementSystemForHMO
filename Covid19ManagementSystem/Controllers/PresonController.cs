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


        //get all persons:
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

        //get person by id:
        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM person WHERE PersonId = @PersonId";

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonId", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
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

                        return Ok(person);
                    }
                    else
                    {
                        return NotFound(); // Return 404 Not Found if the record with the specified ID is not found
                    }
                }
            }
        }

        //insert new persont to database
        [HttpPost]
        public ActionResult<Person> InsertPerson([FromBody] Person person)
        {

            // Check if the DateOfBirth is in the future
            if (person.DateOfBirth > DateTime.Now)
            {
                ModelState.AddModelError("DateOfBirth", "Invalid DateOfBirth. Date cannot be in the future.");
                return BadRequest(ModelState);
            }
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO Person (FirstName, LastName, ID, DateOfBirth, Telephone, MobilePhone, City, Street, NumberStreet) " +
                           "VALUES (@FirstName, @LastName, @ID, @DateOfBirth, @Telephone, @MobilePhone, @City, @Street, @NumberStreet)";

                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", person.FirstName);
                        command.Parameters.AddWithValue("@LastName", person.LastName);
                        command.Parameters.AddWithValue("@ID", person.ID);
                        command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
                        command.Parameters.AddWithValue("@Telephone", person.Telephone);
                        command.Parameters.AddWithValue("@MobilePhone", person.MobilePhone);
                        command.Parameters.AddWithValue("@City", person.City);
                        command.Parameters.AddWithValue("@Street", person.Street);
                        command.Parameters.AddWithValue("@NumberStreet", person.NumberStreet);
                        command.Parameters.AddWithValue("@PersonImage", person.PersonImage);


                        command.ExecuteNonQuery();

                        // Retrieve the auto-generated PersonId
                        int generatedId = (int)command.LastInsertedId;

                        // Assign the generated PersonId to the Person object
                        person.PersonId = generatedId;
                    }
                }

                return CreatedAtAction(nameof(GetAllPersons), new { id = person.PersonId }, person);
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., log the error, return a specific error response)
                return StatusCode(500, "An error occurred while inserting the person.");
            }
        }

    }
}
