using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Covid19ManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Covid19ManagementSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{

        //    services.AddDbContextPool<Covid19ManagementSystemDBCon>(options =>
        //    options.UseSqlServer(Configuration.GetConnectionString("DBConn")));
        //    services.AddControllersWithViews();



        //    // Database creation and table creation
        //    string connectionString = "Server=localhost;Port=3306;Database=coronadatabase;Uid=root;Pwd=password;";
        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        if (connection.State == System.Data.ConnectionState.Open)
        //        {
        //            MySqlCommand command = connection.CreateCommand();

        //            // Check if the database exists
        //            command.CommandText = "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'coronadatabase'";
        //            var result = command.ExecuteScalar();

        //            if (result == null)
        //            {
        //                // Create the database
        //                command.CommandText = "CREATE DATABASE coronadatabase";
        //                command.ExecuteNonQuery();
        //            }

        //            // Switch to the coronadatabase
        //            command.CommandText = "USE coronadatabase";
        //            command.ExecuteNonQuery();

        //            // Read SQL queries from the file
        //            string sqlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "../SQLfiles/createTable.sql");
        //            string[] sqlQueries = File.ReadAllLines(sqlFilePath);

        //            // Execute each SQL query
        //            foreach (string sqlQuery in sqlQueries)
        //            {
        //                if (!string.IsNullOrWhiteSpace(sqlQuery))
        //                {
        //                    command.CommandText = sqlQuery;
        //                    command.ExecuteNonQuery();
        //                }
        //            }

        //        }
        //    }
        //}


        public void ConfigureServices(IServiceCollection services)
        {
            // Other service registrations...

            // Create the database and schema
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    } 
}



