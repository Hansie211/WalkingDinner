using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {

    public class Person {

        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public int ID { get; set; }

        public string FirstName { get; set; }
        
        public string Preposition { get; set; }

        public string LastName { get; set; }

        public override string ToString() {

            string result = FirstName + ' ';
            if ( !string.IsNullOrWhiteSpace(Preposition) ) {
                result += Preposition + ' ';
            }

            return result + LastName;
        }
    }
}
