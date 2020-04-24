using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {

    public class Couple {

        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public int ID { get; set; }

        public Dinner Dinner { get; set;}
        public int DinnerID { get; }

        public Person PersonMain { get; set; }
        public int PersonMainID { get; }

        public Person PersonGuest { get; set; }
        public int? PersonGuestID { get; }

        public string EmailAddress { get; set; }

        public bool Accepted { get; set; }

        public string PhoneNumber { get; set; }

        public string AdminCode { get; set; }

        public Address Address { get; set; }
        public int AddressID { get; }
    }
}
