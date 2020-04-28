using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {

    public class Person {

        [DatabaseGenerated( DatabaseGeneratedOption.Identity ), Key]
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        public string Preposition { get; set; }

        [Required]
        public string LastName { get; set; }

        public override string ToString() {

            string result = FirstName + ' ';
            if ( !string.IsNullOrWhiteSpace(Preposition) ) {
                result += Preposition + ' ';
            }

            return result + LastName;
        }
    }

    public class PersonAdmin : Person {

        public Dinner Dinner { get; set; }
        public int DinnerID { get; }
    }

    public abstract class PersonCouple : Person {

        public Couple Couple { get; set; }
        public int CoupleID { get; }
    }

    public class PersonMain : PersonCouple {

    }

    public class PersonGuest : PersonCouple {

    }
}
