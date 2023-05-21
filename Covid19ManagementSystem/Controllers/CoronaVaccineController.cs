using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using Covid19ManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Covid19ManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoronaVaccineController : ControllerBase
    {
        private readonly string connectionString = "Server=localhost;Port=3306;Database=coronadatabase;Uid=root;Pwd=password;";

        //get all corona vaccines:
        [HttpGet]
        public ActionResult<IEnumerable<CoronaVaccine>> GetAllCoronaVaccines()
        {
            List<CoronaVaccine> coronaVaccines = new List<CoronaVaccine>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM coronavaccine";

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CoronaVaccine coronaVaccine = new CoronaVaccine
                        {
                            VaccineId = reader.GetInt32("VaccineId"),
                            PersonId = reader.GetInt32("PersonId"),
                            VaccinationDate = reader.GetDateTime("VaccinationDate"),
                            Manufacturer = reader.GetString("Manufacturer")
                        };

                        coronaVaccines.Add(coronaVaccine);
                    }
                }
            }

            return Ok(coronaVaccines);
        }

        //get specific corona vaccines by id:
        [HttpGet("{id}")]
        public ActionResult<CoronaVaccine> GetCoronaVaccineById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM CoronaVaccine WHERE VaccineId = @VaccineId";

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@VaccineId", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        CoronaVaccine coronaVaccine = new CoronaVaccine
                        {
                            VaccineId = reader.GetInt32("VaccineId"),
                            PersonId = reader.GetInt32("PersonId"),
                            VaccinationDate = reader.GetDateTime("VaccinationDate"),
                            Manufacturer = reader.GetString("Manufacturer")
                        };

                        return Ok(coronaVaccine);
                    }
                    else
                    {
                        return NotFound("Corona vaccine is not found"); // Return 404 Not Found if the record with the specified ID is not found
                    }
                }
            }
        }

        //get specific corona vaccines by id:
        [HttpGet("person/{id}")]
        public ActionResult<IEnumerable<CoronaVaccine>> GetCoronaVaccinesByPersonId(int id)
        {
            List<CoronaVaccine> coronaVaccines = new List<CoronaVaccine>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM CoronaVaccine WHERE PersonId = @PersonId";

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonId", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CoronaVaccine coronaVaccine = new CoronaVaccine
                        {
                            VaccineId = reader.GetInt32("VaccineId"),
                            PersonId = reader.GetInt32("PersonId"),
                            VaccinationDate = reader.GetDateTime("VaccinationDate"),
                            Manufacturer = reader.GetString("Manufacturer")
                        };

                        coronaVaccines.Add(coronaVaccine);
                    }
                }
            }

            return Ok(coronaVaccines);
        }
        

        //insert new corona vaccine to database:
        [HttpPost]
        public ActionResult<CoronaVaccine> InsertCoronaVaccine(CoronaVaccine coronaVaccine)
        {
            // Check if the VaccinationDate is in the future
            if (coronaVaccine.VaccinationDate > DateTime.Now)
            {
                ModelState.AddModelError("VaccinationDate", "Invalid VaccinationDate. Date cannot be in the future.");
                return BadRequest(ModelState);
            }
            // Validate the manufacturer
            if (coronaVaccine.Manufacturer != "Pfizer" && coronaVaccine.Manufacturer != "Moderna" && coronaVaccine.Manufacturer != "AstraZeneca" && coronaVaccine.Manufacturer != "Johnson & Johnson")
            {
                ModelState.AddModelError("Manufacturer", "Invalid Manufacturer. Allowed values are Pfizer, Moderna, AstraZeneca and Johnson & Johnson.");
                return BadRequest(ModelState);
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {

                    /*
                    * Checking for a correct input of personid that really exists in the person table
                    * since it is currently a foreign key:
                    */
                    string checkQuery = "SELECT COUNT(*) FROM Person WHERE PersonId = @PersonId";

                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@PersonId", coronaVaccine.PersonId); ;

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count <= 0)
                        {
                            ModelState.AddModelError("PersonId", "The PersonId is not exists in the system.");
                            return BadRequest(ModelState);
                        }
                    }

                    // Check if the person already has 4 vaccines:
                    string countQuery = "SELECT COUNT(*) FROM coronavaccine WHERE PersonId = @PersonId";
                    MySqlCommand countCommand = new MySqlCommand(countQuery, connection);
                    countCommand.Parameters.AddWithValue("@PersonId", coronaVaccine.PersonId);

                    int vaccineCountForPerson = Convert.ToInt32(countCommand.ExecuteScalar());

                    if (vaccineCountForPerson >= 4) //maximum 4 vaccines per person:
                    {
                        ModelState.AddModelError("vaccineCountForPerson", "Maximum number of vaccines per person reached.");
                        return BadRequest(ModelState);
                    }
                   
                    // Insert the new CoronaVaccine
                    string query = "INSERT INTO CoronaVaccine (PersonId, VaccinationDate, Manufacturer) " +
                                   "VALUES (@PersonId, @VaccinationDate, @Manufacturer)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@PersonId", coronaVaccine.PersonId);
                    command.Parameters.AddWithValue("@VaccinationDate", coronaVaccine.VaccinationDate);
                    command.Parameters.AddWithValue("@Manufacturer", coronaVaccine.Manufacturer);

                    command.ExecuteNonQuery();

                    // auto-generated VaccineId
                    int generatedId = (int)command.LastInsertedId;
                    coronaVaccine.VaccineId = generatedId;
                }
                catch (MySqlException ex)
                {
                    return BadRequest("An error occurred while inserting the CoronaVaccine: " + ex.Message);
                }
            }

            return CreatedAtAction(nameof(GetAllCoronaVaccines), new { id = coronaVaccine.VaccineId }, coronaVaccine);
        }
    }
}
