using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WalkingDinner.Database;
using WalkingDinner.Extensions;
using WalkingDinner.Models;

namespace WalkingDinner.Pages.Invitation {

    public class SeeInvitationModel : DataBoundModel {

        public SeeInvitationModel( DatabaseContext context ) : base( context ) {
        }

        /**
         * 
         * Accept & payment if applicable
         * 
         */

        [BindProperty( SupportsGet = true )]
        public int CoupleID { get; set; }

        [BindProperty( SupportsGet = true )]
        public string AdminCode { get; set; }

        public Couple Couple { get; set; }

        public async Task<IActionResult> OnGetAsync() {

            Couple = await Database.GetCoupleAsAdminAsync( CoupleID, AdminCode );
            if ( Couple == null ) {

                return NotFound();
            }

            if ( Couple.Accepted ) {

                // Couple has accepted / payed
                return Redirect( ModelPath.Get<EditCoupleModel>() );
            }

            return Page();
        }

        public async Task<IActionResult> OnPostPayment() {

            Couple = await Database.GetCoupleAsAdminAsync( CoupleID, AdminCode );
            if ( Couple == null ) {

                return NotFound();
            }

            return Redirect( ModelPath.Get<PaymentModel>() );
        }

        public async Task<IActionResult> OnPostAccept() {

            Couple = await Database.GetCoupleAsAdminAsync( CoupleID, AdminCode );
            if ( Couple == null ) {

                return NotFound();
            }

            if ( Couple.Dinner.HasPrice ) { // not so fast

                return BadRequest();
            }

            Couple.Accepted = true;
            await Database.SaveChangesAsync();

            return Redirect( ModelPath.Get<EditCoupleModel>() );
            //, new { handler = (string)null } 
        }
    }
}
