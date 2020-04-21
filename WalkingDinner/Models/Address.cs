using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {
    public class Address {

        [Key]
        public int ID { get; set; }

        public string Street { get; set; }
        public int Number { get; set; }
        public string NumberPostfix { get; set; }

        public string ZipCode { get; set; }
        public string Place { get; set; }
    }
}
