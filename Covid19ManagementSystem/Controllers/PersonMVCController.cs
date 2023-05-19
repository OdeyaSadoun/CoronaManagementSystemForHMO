using Covid19ManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Covid19ManagementSystem.Controllers
{
    public class PersonMVCController : Controller
    {
        private readonly string connectionString = "Server=localhost;Port=3306;Database=coronadatabase;Uid=root;Pwd=password;";

        public IActionResult Index()
        {
            List<Person> persons = GetAllPersons(); // Retrieve all persons from the API

            return View(persons);
        }

        public IActionResult AddMember(Person person = null)
        {
            return View(person);
        }

        // Helper method to retrieve all persons from the API
        private List<Person> GetAllPersons()
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

            return persons;
        }


        public IActionResult AddVaccine()
        {
            return RedirectToAction("AddVaccine", "CoronaVaccineMVC");
        }
        public IActionResult AddPositiveTest()
        {
            return RedirectToAction("AddPositiveTest", "CoronaTestMVC");
        }
    }




}
