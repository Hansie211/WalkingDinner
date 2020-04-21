using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {
    public class Couple {

        [Key]
        public int ID { get; set; }

        [ForeignKey( "DinnerID" )]
        public Dinner Dinner { get; set; }
        public int DinnerID { get; private set; }

        [ForeignKey( "PersonMainID" )]
        public Person PersonMain { get; set; }
        public int PersonMainID { get; private set; }

        [ForeignKey( "PersonExtraID" )]
        public Person PersonExtra { get; set; }
        public int PersonExtraID { get; private set; }

        public string EmailAddress { get; set; }

        public bool EmailValidated { get; set; }

        public string UniqueCode { get; set; }

        public string PhoneNumber { get; set; }

        [ForeignKey( "AddressID" )]
        public Address Address { get; set; }
        public int AddressID { get; private set; }
    }
}
