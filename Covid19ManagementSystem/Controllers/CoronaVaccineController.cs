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
                        return NotFound(); // Return 404 Not Found if the record with the specified ID is not found
                    }
                }
            }
        }

        //insert new corona vaccine to database:
        [HttpPost]
        [HttpPost]
        public ActionResult<CoronaVaccine> InsertCoronaVaccine(CoronaVaccine coronaVaccine)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    // Check if the person already has 4 vaccines:
                    string countQuery = "SELECT COUNT(*) FROM CoronaVaccine WHERE PersonId = @PersonId";
                    MySqlCommand countCommand = new MySqlCommand(countQuery, connection);
                    countCommand.Parameters.AddWithValue("@PersonId", coronaVaccine.PersonId);

                    int vaccineCount = Convert.ToInt32(countCommand.ExecuteScalar());

                    if (vaccineCount >= 4)
                    {
                        // Return a response indicating the maximum limit has been reached (4 vaccines):
                        return BadRequest("Maximum number of vaccines per person reached.");
                    }


                    // Insert the CoronaVaccine record
                    string query = "INSERT INTO CoronaVaccine (PersonId, VaccinationDate, Manufacturer) " +
                                   "VALUES (@PersonId, @VaccinationDate, @Manufacturer)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@PersonId", coronaVaccine.PersonId);
                    command.Parameters.AddWithValue("@VaccinationDate", coronaVaccine.VaccinationDate);
                    command.Parameters.AddWithValue("@Manufacturer", coronaVaccine.Manufacturer);

                    command.ExecuteNonQuery();

                    // Retrieve the auto-generated VaccineId
                    int generatedId = (int)command.LastInsertedId;

                    // Assign the generated VaccineId to the CoronaVaccine object
                    coronaVaccine.VaccineId = generatedId;
                }
                catch (MySqlException ex)
                {
                    //Checking for a correct input of personid that really exists in the person table
                    //since it is currently a foreign key:
                    if (ex.Number == 1452) // MySQL error code for foreign key constraint violation
                    {
                        // PersonId does not exist:
                        ModelState.AddModelError("PersonId", "Invalid PersonId");
                        return BadRequest(ModelState);
                    }

                    // other exceptions
                    return BadRequest("An error occurred while inserting the CoronaVaccine: " + ex.Message);
                }
            }

            return CreatedAtAction(nameof(GetAllCoronaVaccines), new { id = coronaVaccine.VaccineId }, coronaVaccine);
        }

    }
}
