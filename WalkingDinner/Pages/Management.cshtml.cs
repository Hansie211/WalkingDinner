using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WalkingDinner.Database;
using WalkingDinner.Extensions;
using WalkingDinner.Models;

namespace WalkingDinner.Pages.Demo {

    public class ManagementModel : DataBoundModel {

        public ManagementModel( DatabaseContext context ) : base( context ) {
        }

        /**
         * Update Dinner name, description etc
         * 
         * Invite couples by Email and update Dinner. Maybe some administation for the payments?
         * 
         * Remove couples
         * 
         * After Subscriptionstop, see possible combinations and choose one -> send hostes their courses, generate pdf's
         * 
         */

        [BindProperty( SupportsGet = true )]
        public int DinnerID { get; set; }

        [BindProperty( SupportsGet = true )]
        public string AdminCode { get; set; }

        [BindProperty]
        public Dinner Dinner { get; set; }

        [BindProperty]
        public Invite Invite { get; set; }

        public async Task<IActionResult> OnGetAsync() {

            Dinner = await Database.GetDinnerAsAdmin( DinnerID, AdminCode );
            if ( Dinner == null ) {

                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostInvite() {

            Dinner = await Database.GetDinnerAsAdmin( DinnerID, AdminCode );
            if ( Dinner == null ) {

                return NotFound();
            }

            if ( !ModelState.IsValid( nameof(Invite) ) ) { 

                return Page();
            }

            Invite.EmailAddress = Invite.EmailAddress.ToLower();

            foreach ( Couple storedCouple in Dinner.Couples ) {

                if ( storedCouple.EmailAddress == Invite.EmailAddress ) {

                    ViewData[ "InviteResult" ] = $"{ ( Invite as Person ) } met emailadres { Invite.EmailAddress } is al uitgenodigd.";
                    return Page();
                }
            }

            Couple couple = await Database.CreateCoupleAsync( DinnerID, new Couple() {
                Accepted = false,
                Address = new Address(),
                Dinner = Dinner,
                EmailAddress = Invite.EmailAddress,
                PersonMain = ( Invite as Person ),
                PersonGuest = null,
                PhoneNumber = "",
            } );

            if ( couple  == null ) {

                ViewData[ "InviteResult" ] = $"Kan { ( Invite as Person ) } nu niet uitnodigen.";
                return Page();
            }

            EmailServer.SendEmail( Invite.EmailAddress, "Uitnodiging", 
                $"U ben uitgenodigd door { Dinner.Admin } om deel te namen aan een WalkingDinner. Bekijk uit uitnodiging <a href=\"https://{ Request.Host }/Invite/{ couple.ID }/{ couple.AdminCode }/\">Hier</a>" );

            ViewData[ "InviteResult" ] = $"{ ( Invite as Person ) } is uitgenodigd.";


            // Clear input data
            ModelState.Clear( nameof(Invite) );
            Invite = null;

            return Page();
        }
    }
}
