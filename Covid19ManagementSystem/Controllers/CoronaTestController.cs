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
                            PositiveDate = reader.IsDBNull(reader.GetOrdinal("PositiveDate")) ? (DateTime?)null : reader.GetDateTime("PositiveDate"),
                            RecoveryDate = reader.IsDBNull(reader.GetOrdinal("RecoveryDate")) ? (DateTime?)null : reader.GetDateTime("RecoveryDate")
                        };

                        coronaTests.Add(coronaTest);
                    }
                }
            }

            return Ok(coronaTests);
        }

        [HttpPost]
        public ActionResult<CoronaTest> InsertCoronaTest(CoronaTest coronaTest)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO coronatest (PersonId, PositiveDate, RecoveryDate) " +
                               "VALUES (@PersonId, @PositiveDate, @RecoveryDate)";

                connection.Open();

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

            return CreatedAtAction(nameof(GetAllCoronaTests), new { id = coronaTest.TestId }, coronaTest);
        }
    }
}
