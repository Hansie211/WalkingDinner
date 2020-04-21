using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {
    public class Dinner {

        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime Date { get; set; }

        public DateTime SubscriptionStop { get; set; }

        public float Price { get; set; }

        public string IBAN { get; set; }

        [ForeignKey("AdminID")]
        public Person Admin { get; set; }
        public int AdminID { get; private set; }

        public string AdminEmailAddress { get; set; }

        public IList<Couple> Couples { get; set; }

        public string Code { get; set; }

        public string AdminCode { get; set; }

        public Dinner() {

            Couples = new List<Couple>();
        }
    }
}
