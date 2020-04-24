using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {

    public class Invite : Person {

        [Required]
        [DataType( DataType.EmailAddress )]
        public string EmailAddress { get; set; }
    }
}
