using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.IO;

namespace Covid19ManagementSystem
{

    public static class DatabaseInitializer
    {
        public static void InitializeDatabase(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    MySqlCommand command = connection.CreateCommand();

                    // Create the database
                    command.CommandText = "CREATE DATABASE IF NOT EXISTS coronadatabase;";
                    command.ExecuteNonQuery();

                    // Switch to the database
                    command.CommandText = "USE coronadatabase;";
                    command.ExecuteNonQuery();

                    // Read and execute the schema creation SQL queries from a file
                    string sqlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "../SQLFiles/craeteTables.sql");
                    string sqlQueries = File.ReadAllText(sqlFilePath);

                    command.CommandText = sqlQueries;
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
