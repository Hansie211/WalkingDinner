using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingDinner.Database;

namespace WalkingDinner.Pages {

    public abstract class DataBoundModel : PageModel {

        protected DatabaseManager Database; 

        public DataBoundModel( DatabaseContext context ) {

            Database = new DatabaseManager( context );
        }
    }
}
