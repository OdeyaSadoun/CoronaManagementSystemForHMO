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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VaccineId { get; set; }

        [Required]
        [ForeignKey("Person")]
        public int PersonId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime VaccinationDate { get; set; }

        [Required]
        [Column(TypeName = "varchar(50")]
        public string Manufacturer { get; set; }

    }

}