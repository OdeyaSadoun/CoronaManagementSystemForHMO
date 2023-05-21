using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Covid19ManagementSystem.Models
{
    public class CoronaTest
    {
        public int TestId { get; set; }

        public int PersonId { get; set; }

        public DateTime PositiveDate { get; set; }

        public DateTime? RecoveryDate { get; set; }
    }
}