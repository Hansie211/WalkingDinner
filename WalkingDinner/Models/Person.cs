using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {

    public class Person {

        [Key]
        public int ID { get; set; }

        public string FirstName { get; set; }
        
        public string Preposition { get; set; }

        public string LastName { get; set; }
    }
}
