using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Covid19ManagementSystem.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Telephone { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string MobilePhone { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string City { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Street { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string NumberStreet { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? PersonImage { get; set; }

        public IFormFile? ImageFile { get; set; }

    }

}