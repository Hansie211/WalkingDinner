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

        [BindProperty]
        public Dinner Dinner { get; set; }

        public async Task<IActionResult> OnGetAsync() {

            Couple couple = await Database.GetCoupleAsAdminAsync( CoupleID, AdminCode );
            if ( couple == null ) {

                return NotFound();
            }

            if ( couple.Accepted ) {

                // Couple has accepted / payed
                return RedirectToPage( ModelPath.Get<IndexModel>(), new { CoupleID, AdminCode } );
            }

            Dinner = couple.Dinner;

            return Page();
        }

        public async Task<IActionResult> OnPostPayment() {

            Couple couple = await Database.GetCoupleAsAdminAsync( CoupleID, AdminCode );
            if ( couple == null ) {

                return NotFound();
            }

            if ( couple.Accepted ) {

                // Couple has accepted / payed
                return RedirectToPage( ModelPath.Get<IndexModel>(), new { CoupleID, AdminCode } );
            }

            return RedirectToPage( ModelPath.Get<PaymentModel>(), new { CoupleID, AdminCode } );
        }
    }
}
