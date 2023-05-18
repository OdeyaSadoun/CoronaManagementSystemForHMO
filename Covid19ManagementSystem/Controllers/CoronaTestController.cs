using System;
using System.Collections.Generic;
using Covid19ManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Covid19ManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoronaTestController : ControllerBase
    {
        private readonly string connectionString = "Server=localhost;Port=3306;Database=coronadatabase;Uid=root;Pwd=password;";

        //get all corona tests:
        [HttpGet]
        public ActionResult<IEnumerable<CoronaTest>> GetAllCoronaTests()
        {
            List<CoronaTest> coronaTests = new List<CoronaTest>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM coronatest";

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CoronaTest coronaTest = new CoronaTest
                        {
                            TestId = reader.GetInt32("TestId"),
                            PersonId = reader.GetInt32("PersonId"),
                            PositiveDate = reader.GetDateTime("PositiveDate"),
                            RecoveryDate = reader.IsDBNull(reader.GetOrdinal("RecoveryDate")) ? null : reader.GetDateTime("RecoveryDate")
                        };

                        coronaTests.Add(coronaTest);
                    }
                }
            }

            return Ok(coronaTests);
        }

        //get specific corona test by id:
        [HttpGet("{id}")]
        public ActionResult<CoronaTest> GetCoronaTestById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM coronatest WHERE TestId = @TestId";

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestId", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        CoronaTest coronaTest = new CoronaTest
                        {
                            TestId = reader.GetInt32("TestId"),
                            PersonId = reader.GetInt32("PersonId"),
                            PositiveDate = reader.GetDateTime("PositiveDate"),
                            RecoveryDate = reader.IsDBNull(reader.GetOrdinal("RecoveryDate")) ? null : reader.GetDateTime("RecoveryDate")
                        };

                        return Ok(coronaTest);
                    }
                    else
                    {
                        return NotFound(); // Return 404 Not Found if the record with the specified ID is not found
                    }
                }
            }
        }

        //insert new corona test to database:
        [HttpPost]
        public ActionResult<CoronaTest> InsertCoronaTest(CoronaTest coronaTest)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
           
               
                try
                {
                    // check that PositiveDate not null
                    if (coronaTest.PositiveDate == null)
                    {
                        return BadRequest("Positive date cannot be null.");
                    }

                    //check valid date for PositiveDate
                    if (coronaTest.PositiveDate > DateTime.Now)
                    {
                        ModelState.AddModelError("PositiveDate", "Invalid PositiveDate. Date cannot be in the future.");
                        return BadRequest(ModelState);
                    }

                    //check valid date for RecoveryDate
                    if (coronaTest.RecoveryDate > DateTime.Now)
                    {
                        ModelState.AddModelError("RecoveryDate", "Invalid RecoveryDate. Date cannot be in the future.");
                        return BadRequest(ModelState);
                    }

                    // Check if the person already had positive corona test:
                    string countQuery = "SELECT COUNT(*) FROM coronatest WHERE PersonId = @PersonId";
                    MySqlCommand countCommand = new MySqlCommand(countQuery, connection);
                    countCommand.Parameters.AddWithValue("@PersonId", coronaTest.PersonId);

                    int coronaTestCountForPerson = Convert.ToInt32(countCommand.ExecuteScalar());

                    if (coronaTestCountForPerson == 1)
                    {
                        /*
                         According to the premise of the exercise,
                         it must be assumed that a person who has already been sick with Corona cannot get sick again.
                         */

                        // Return a response indicating the maximum limit has been reached (1 time):
                        return BadRequest("This person was already sick with Corona, cannot be admitted again.");
                    }


                    // Insert the CoronaVaccine record
                    string query = "INSERT INTO coronatest (PersonId, PositiveDate, RecoveryDate) " +
                          "VALUES (@PersonId, @PositiveDate, @RecoveryDate)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@PersonId", coronaTest.PersonId);
                    command.Parameters.AddWithValue("@PositiveDate", coronaTest.PositiveDate);
                    command.Parameters.AddWithValue("@RecoveryDate", coronaTest.RecoveryDate);

                    command.ExecuteNonQuery();

                    // Retrieve the auto-generated TestId
                    int generatedId = (int)command.LastInsertedId;

                    // Assign the generated TestId to the CoronaTest object
                    coronaTest.TestId = generatedId;
                }
                catch (MySqlException ex)
                {
                    /*
                     * Checking for a correct input of personid that really exists in the person table
                     * since it is currently a foreign key:
                     */

                    if (ex.Number == 1452) // MySQL error code for foreign key constraint violation
                    {
                        // PersonId does not exist:
                        ModelState.AddModelError("PersonId", "Invalid PersonId");
                        return BadRequest(ModelState);
                    }

                    // other exceptions
                    return BadRequest("An error occurred while inserting the CoronaVaccine: " + ex.Message);
                }

                return CreatedAtAction(nameof(GetAllCoronaTests), new { id = coronaTest.TestId }, coronaTest);
            }
        }

        // Update the RecoveryDate for a specific person's corona test
        [HttpPatch("{personId}/recoverydate")]
        public ActionResult UpdateRecoveryDate(int personId, [FromBody] DateTime recoveryDate)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
              
                // Check if the corona test exists for the person
                string checkQuery = "SELECT COUNT(*) FROM coronatest WHERE PersonId = @PersonId";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@PersonId", personId);

                int testCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (testCount == 0)
                {
                    return NotFound(); // Return 404 Not Found if the corona test for the person is not found
                }

                // Retrieve the existing RecoveryDate for the current person
                string existingRecoveryDateQuery = "SELECT RecoveryDate FROM coronatest WHERE PersonId = @PersonId";
                MySqlCommand existingRecoveryDateCommand = new MySqlCommand(existingRecoveryDateQuery, connection);
                existingRecoveryDateCommand.Parameters.AddWithValue("@PersonId", personId);

                using (MySqlDataReader reader = existingRecoveryDateCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //update anable when the value in RecoveryDate is null
                        DateTime? existingRecoveryDate = reader.IsDBNull(0) ? null : reader.GetDateTime(0);

                        if (existingRecoveryDate != null)
                        {
                            return BadRequest("RecoveryDate cannot be updated because it is already set."); // Return 400 Bad Request if the existing RecoveryDate is not null
                        }
                    }
                    else
                    {
                        return NotFound(); // Return 404 Not Found if the existing RecoveryDate for the person is not found
                    }
                }

                // Retrieve the PositiveDate for the person for check valid RecoveryDate
                string positiveDateQuery = "SELECT PositiveDate FROM coronatest WHERE PersonId = @PersonId";
                MySqlCommand positiveDateCommand = new MySqlCommand(positiveDateQuery, connection);
                positiveDateCommand.Parameters.AddWithValue("@PersonId", personId);

                using (MySqlDataReader reader = positiveDateCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime positiveDate = reader.GetDateTime(0);

                        if (recoveryDate <= positiveDate)
                        {
                            return BadRequest("RecoveryDate must be later than the PositiveDate."); // Return 400 Bad Request if recoveryDate is not later than positiveDate
                        }
                    }
                    else
                    {
                        return NotFound(); // Return 404 Not Found if the positiveDate for the person is not found
                    }
                }

                // Update the RecoveryDate
                string updateQuery = "UPDATE coronatest SET RecoveryDate = @RecoveryDate WHERE PersonId = @PersonId";
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@RecoveryDate", recoveryDate);
                updateCommand.Parameters.AddWithValue("@PersonId", personId);

                updateCommand.ExecuteNonQuery();

                return NoContent(); // Return 204 No Content to indicate successful update
            }
        }



    }
}
