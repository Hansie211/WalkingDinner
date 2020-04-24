using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WalkingDinner.Database;
using WalkingDinner.Models;

namespace WalkingDinner.Pages {

    public class CreateNew : DataBoundModel {

        public CreateNew( DatabaseContext context ) : base( context ) {
        }

        /**
         * User setup Title, Description, Location, Date, SubscriptionStop, Price, IBAN, 
         * Admin and AdminEmailAdress
         * 
         * Generate Code for inventations and AdminCode for management
         * 
         * Send AdminCode to AdminEmailAddress
         * 
         */

        public IActionResult OnGet() {

            return Page();
        }

        [BindProperty]
        public Dinner Dinner { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {

            if ( !ModelState.IsValid ) {

                return Page();
            }

            await Database.CreateDinnerAsync( Dinner );

            EmailServer.SendEmail( Dinner.AdminEmailAddress, "Nieuw dinner", $"Nieuw diner aangemaakt, code: <a href=\"https://{ Request.Host }/Manage/{Dinner.ID}/{Dinner.AdminCode}/\">Beheer</a>" );

            return RedirectToPage( "AwaitEmail" );
        }

    }
}
