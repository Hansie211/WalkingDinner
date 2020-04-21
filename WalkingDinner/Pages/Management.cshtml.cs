using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WalkingDinner.DataLayer;
using WalkingDinner.Models;

namespace WalkingDinner.Pages {

    public class ManagementModel : PageModel {

        /**
         * Update Dinner name, description etc
         * 
         * Invite couples by Email and update Dinner. Maybe some administation for the payments?
         * 
         * If subscriptionstop < now, see possible combinations and choose one -> send hostes their courses, generate pdf's
         * 
         */

        [BindProperty( SupportsGet = true )]
        public string AdminCode { get; set; }

        public IActionResult OnGet() {

            return Page();
        }
    }
}
