using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Covid19ManagementSystem.Models
{
    public class CoronaVaccine
    {
        public int VaccineId { get; set; }

        public int PersonId { get; set; }

        public DateTime VaccinationDate { get; set; }

        public string Manufacturer { get; set; }
    }
}