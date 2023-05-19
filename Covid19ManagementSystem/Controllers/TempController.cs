using System;
using System.Collections.Generic;
using Covid19ManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Covid19ManagementSystem.Controllers
{
    public class TempController : Controller
    {
        private readonly string connectionString = "Server=localhost;Port=3306;Database=coronadatabase;Uid=root;Pwd=password;";

        // GET: Person
        public ActionResult Index()
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

            return View(persons);
        }

        // GET: Person/Details/5
        public ActionResult Details(int id)
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

                        return View(person);
                    }
                    else
                    {
                        return NotFound(); // Return 404 Not Found if the record with the specified ID is not found
                    }
                }
            }
        }
        [HttpGet("PersonMVC/Edit")]
        public ActionResult Edit()
        {
            // Handle the case when no ID is specified
            // For example, you can redirect to a different action or display an error message
            return RedirectToAction("Index");
        }

        // GET: Person/Edit/5
        [HttpGet("PersonMVC/Edit/{id}")]
        public ActionResult Edit(int id)
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

                        return View(person);
                    }
                    else
                    {
                        //return HttpNotFound(); // Return 404 Not Found if the record with the specified ID is not found
                        return NotFound();
                    }
                }
            }
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Person person)
        {
            // Update the person in the database
            // ...

            // Redirect to the details page or display a success message
            return RedirectToAction("Details", new { id = person.PersonId });
        }

        // GET: PersonMVC/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: PersonMVC/Create
        [HttpPost]
        public ActionResult Create(Person person)
        {
            // Add the new person to the database or perform any necessary operations
            // ...

            // Redirect to the details page or display a success message
            return RedirectToAction("Details", new { id = person.PersonId });
        }
    }
}
