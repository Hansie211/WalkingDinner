using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WalkingDinner.Database;
using WalkingDinner.Extensions;
using WalkingDinner.Models;
using WalkingDinner.Mollie;

namespace WalkingDinner.Pages.Invitation {

    public class RedirectPaymentModel : DataBoundModel {

        public RedirectPaymentModel( DatabaseContext context ) : base( context ) {
        }

        [BindProperty( SupportsGet = true )]
        public int CoupleID { get; set; }

        [BindProperty( SupportsGet = true )]
        public string AdminCode { get; set; }

        [BindProperty()]
        public string Status { get; set; }

        public async Task<IActionResult> OnGetAsync() {

            Couple couple = await Database.GetCoupleAsAdminAsync( CoupleID, AdminCode );
            if ( couple == null ) {

                return NotFound();
            }

            if ( couple.Accepted ) {

                return Redirect( ModelPath.Get<Invitation.EditCoupleModel>() );
            }

            if ( !couple.Dinner.HasPrice ) {

                return Redirect( ModelPath.Get<Invitation.SeeInvitationModel>() );
            }

            Status = await MollieAPI.GetPaymentStatus( couple.PaymentId );
            if ( Status == "paid" ) {

                couple.Accepted = true;
                await Database.SaveChangesAsync();

                return Redirect( ModelPath.Get<Invitation.EditCoupleModel>() );
            }

            return Page();
        }
    }
}