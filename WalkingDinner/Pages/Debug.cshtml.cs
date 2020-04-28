using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WalkingDinner.Database;
using WalkingDinner.Models;

namespace WalkingDinner.Pages {

    public class DebugModel : DataBoundModel {

        public DebugModel( DatabaseContext context ) : base( context ) {
        }

        public IList<Dinner> Dinners { get; set; }

        public async Task OnGetAsync() {
            Dinners = await Database.Database.Dinners.Include( d => d.Admin ).ToListAsync();
        }
    }
}
