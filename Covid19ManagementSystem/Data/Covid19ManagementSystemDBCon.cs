using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Covid19ManagementSystem.Models;
using Microsoft.EntityFrameworkCore;


namespace Covid19ManagementSystem.Data
{
    public class Covid19ManagementSystemDBCon : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<CoronaVaccine> CoronaVaccines { get; set; }
        public DbSet<CoronaTest> CoronaTests { get; set; }

        // Constructor to configure the DbContext options
        public Covid19ManagementSystemDBCon(DbContextOptions<Covid19ManagementSystemDBCon> options) : base(options)
        {
        }
    }
}