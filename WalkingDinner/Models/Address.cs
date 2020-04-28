using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {
    public class Address {

        [DatabaseGenerated( DatabaseGeneratedOption.Identity ), Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("CoupleID")]
        public Couple Couple { get; set; }
        public int CoupleID { get; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public string NumberPostfix { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string Place { get; set; }
    }
}
