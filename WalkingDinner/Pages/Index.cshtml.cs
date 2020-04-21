using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WalkingDinner.Database;

namespace WalkingDinner.Pages {
    public class IndexModel : PageModel {

        private readonly ILogger<IndexModel> _logger;

        private readonly DatabaseController Database;

        public IndexModel( ILogger<IndexModel> logger, DatabaseContext database ) {

            _logger = logger;
            Database = new DatabaseController( database );
        }

        public void OnGet() {


            //Database.Persons.Add(
            //    new Models.Person() {
            //        FirstName = "Henk",
            //        Preposition = "Van",
            //        LastName = "Achter",
            //    }
            //);

            //Database.SaveChanges();
        }
    }
}
