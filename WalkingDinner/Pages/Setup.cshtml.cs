using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WalkingDinner.Pages {

    public class SetupModel : PageModel {

        /**
         * User setup Title, Description, Location, Date, SubscriptionStop, Price, IBAN, 
         * Admin and AdminEmailAdress
         * 
         * Generate Code for inventations and AdminCode for management
         * 
         * Send AdminCode to AdminEmailAddress
         * 
         */

        public void OnGet() {
        }
    }
}
