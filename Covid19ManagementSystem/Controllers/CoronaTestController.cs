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
                        return NotFound("Corona Test is not found"); // Return 404 Not Found if the record with the specified ID is not found
                    }
                }
            }
        }

        //insert new corona test to database:
        [HttpPost]
        public ActionResult<CoronaTest> InsertCoronaTest(CoronaTest coronaTest)
        {
            // check that PositiveDate not null
            if (coronaTest.PositiveDate == null)
            {
                ModelState.AddModelError("PositiveDate", "Positive date cannot be null.");
                return BadRequest(ModelState);
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
                        checkCommand.Parameters.AddWithValue("@PersonId", coronaTest.PersonId); ;

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count <= 0)
                        {
                            ModelState.AddModelError("PersonId", "The PersonId is not exists in the system.");
                            return BadRequest(ModelState);
                        }
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
                        ModelState.AddModelError("coronaTestCountForPerson", "This person was already sick with Corona, cannot be admitted again.");
                        return BadRequest(ModelState);
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
                    return BadRequest("An error occurred while inserting the CoronaVaccine: " + ex.Message);
                }

                return CreatedAtAction(nameof(GetAllCoronaTests), new { id = coronaTest.TestId }, coronaTest);
            }
        }

        // Update the RecoveryDate for a specific person's corona test
        [HttpPut("{personId}/recoverydate")]
        public ActionResult UpdateRecoveryDate(int personId, CoronaTest coronaTest)
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
                    ModelState.AddModelError("CoronaTestCount", "Not Found a corona test for this person.");
                    return BadRequest(ModelState);
                }

                /*
                 We will check that the current person does not already have a recovery date,
                 because if so, it is not possible to update once more.
                 */
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
                            ModelState.AddModelError("RecoveryDate", "RecoveryDate cannot be updated because it is already set.");
                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        return NotFound("Recovery Date is not found"); // Return 404 Not Found if the existing RecoveryDate for the person is not found
                    }
                }

                /*
                 We will check what is the date of receiving the positive answer for the current person,
                 and only if the date of recovery is after the date of receiving the positive answer - it can be updated in the system,
                 otherwise, it is not true because it is impossible to recover before getting sick...
                 */
                string positiveDateQuery = "SELECT PositiveDate FROM coronatest WHERE PersonId = @PersonId";
                MySqlCommand positiveDateCommand = new MySqlCommand(positiveDateQuery, connection);
                positiveDateCommand.Parameters.AddWithValue("@PersonId", personId);

                using (MySqlDataReader reader = positiveDateCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime positiveDate = reader.GetDateTime(0);

                        if (coronaTest.RecoveryDate <= positiveDate)
                        {
                            ModelState.AddModelError("RecoveryDate", "RecoveryDate must be later than the PositiveDate.");
                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        return NotFound("Positive Corona Test is not found"); // Return 404 Not Found if the positiveDate for the person is not found
                    }
                }

                // Update the RecoveryDate
                string updateQuery = "UPDATE coronatest SET RecoveryDate = @RecoveryDate WHERE PersonId = @PersonId";
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@RecoveryDate", coronaTest.RecoveryDate);
                updateCommand.Parameters.AddWithValue("@PersonId", personId);

                updateCommand.ExecuteNonQuery();

                return NoContent(); // successful update
            }
        }

        [HttpGet("statistics")]
        public ActionResult<IEnumerable<CoronaStatistics>> GetCoronaStatistics()
        {
            DateTime endDate = DateTime.Today; // Current date
            DateTime startDate = endDate.AddDays(-29); // 30 days ago

            List<CoronaStatistics> statistics = new List<CoronaStatistics>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // calculate the number of active patients per day within the date range
                string query = "SELECT DATE(PositiveDate) AS Date, COUNT(*) AS ActivePatients " +
                               "FROM coronatest " +
                               "WHERE PositiveDate >= @StartDate AND PositiveDate <= @EndDate " +
                               "GROUP BY DATE(PositiveDate)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime date = reader.GetDateTime("Date");
                        int activePatients = reader.GetInt32("ActivePatients");

                        CoronaStatistics stats = new CoronaStatistics
                        {
                            Date = date,
                            ActivePatients = activePatients
                        };

                        statistics.Add(stats);
                    }
                }

                // Fill in missing dates with 0 active patients - we want tha all 30 days
                DateTime currentDate = startDate;
                while (currentDate <= DateTime.Now.Date)
                {
                    if (!statistics.Any(s => s.Date.Date == currentDate.Date))
                    {
                        statistics.Add(new CoronaStatistics { Date = currentDate.Date, ActivePatients = 0 });
                    }
                    currentDate = currentDate.AddDays(1);
                }

                // Sort the statistics by date:
                statistics = statistics.OrderBy(s => s.Date).ToList();
            }

            return Ok(statistics);
        }

        // Get the number of persons who did not receive any vaccine
        [HttpGet("unvaccinated")]
        public ActionResult<int> GetUnvaccinatedPersonsCount()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // count the number of persons who did not receive any vaccine
                string query = "SELECT COUNT(*) FROM Person " +
                               "WHERE PersonId NOT IN (SELECT DISTINCT PersonId FROM CoronaVaccine)";

                MySqlCommand command = new MySqlCommand(query, connection);

                int unvaccinatedCount = Convert.ToInt32(command.ExecuteScalar());

                return unvaccinatedCount;
            }
        }

    }
   
}
