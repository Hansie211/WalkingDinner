using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {

    public class Dinner {

        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public int ID { get; set; }

        [Required, StringLength(maximumLength: 20)]
        public string Title { get; set; }

        [DataType( DataType.MultilineText)]
        public string Description { get; set; }

        public string Location { get; set; }

        [System.ComponentModel.DataAnnotations.DataType( DataType.DateTime )]
        public DateTime Date { get; set; }

        [DataType( DataType.DateTime )]
        public DateTime SubscriptionStop { get; set; }

        [DataType( DataType.Currency )]
        public float Price { get; set; }

        public string IBAN { get; set; }

        public Person Admin { get; set; }
        public int AdminID { get; set; }

        [DataType( DataType.EmailAddress)]
        public string AdminEmailAddress { get; set; }

        public IList<Couple> Couples { get; set; }

        public string AccessCode { get; set; }

        public string AdminCode { get; set; }

        public bool IsActivated { get; set; }

        public Dinner() {

            Couples = new List<Couple>();
        }
    }
}
