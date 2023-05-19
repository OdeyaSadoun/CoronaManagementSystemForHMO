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

 
        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string ID { get; set; }

  
        public DateTime DateOfBirth { get; set; }


        public string Telephone { get; set; }


        public string MobilePhone { get; set; }


        public string City { get; set; }

   
        public string Street { get; set; }


        public string NumberStreet { get; set; }

    
        //public string? PersonImage { get; set; }

        //public IFormFile? ImageFile { get; set; }

    }

}