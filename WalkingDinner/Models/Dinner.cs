using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {

    public class Dinner {

        [DatabaseGenerated( DatabaseGeneratedOption.Identity ), Key]
        public int ID { get; set; }

        [Required]
        [StringLength(maximumLength: 20)]
        public string Title { get; set; }

        [Required]
        [DataType( DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [DataType( DataType.DateTime )]
        public DateTime Date { get; set; }

        [Required]
        [DataType( DataType.DateTime )]
        public DateTime SubscriptionStop { get; set; }

        [Required]
        [DataType( DataType.Currency )]
        public float Price { get; set; }

        [Required]
        public string IBAN { get; set; }

        [Required]
        public PersonAdmin Admin { get; set; }

        [Required]
        [DataType( DataType.EmailAddress)]
        public string AdminEmailAddress { get; set; }

        [Required]
        public IList<Couple> Couples { get; set; }

        public string AccessCode { get; set; }

        public string AdminCode { get; set; }

        [Required]
        public bool IsActivated { get; set; }

        public Dinner() {

            Couples = new List<Couple>();
        }
    }
}
