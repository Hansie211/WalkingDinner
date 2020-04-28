using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {

    public class Couple {

        [DatabaseGenerated( DatabaseGeneratedOption.Identity ), Key]
        public int ID { get; set; }

        [Required]
        public Dinner Dinner { get; set; }
        public int DinnerID { get; }

        [Required]
        public PersonMain PersonMain { get; set; }

        public PersonGuest PersonGuest { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public bool Accepted { get; set; }

        public string DietaryGuidelines { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string AdminCode { get; set; }

        [Required]
        public Address Address { get; set; }
    }
}
