using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WalkingDinner.Database;
using WalkingDinner.Extensions;
using WalkingDinner.Models;
using WalkingDinner.Mollie;

namespace WalkingDinner.Pages.Invitation {

    public class EditCoupleModel : DataBoundModel {

        public EditCoupleModel( DatabaseContext context ) : base( context ) {
        }

        [BindProperty( SupportsGet = true )]
        public int CoupleID { get; set; }

        [BindProperty( SupportsGet = true )]
        public string AdminCode { get; set; }

        [BindProperty]
        public Couple Couple { get; set; }

        public async Task<IActionResult> OnGetAsync() {

            Couple = await Database.GetCoupleAsAdminAsync( CoupleID, AdminCode );
            if ( Couple == null ) {

                return NotFound();
            }

            if ( !Couple.Accepted ) {

                if ( Couple.Dinner.HasPrice && !string.IsNullOrEmpty( Couple.PaymentId ) ) {

                    return Redirect( ModelPath.Get<RedirectPaymentModel>() );
                }

                return Redirect( ModelPath.Get<SeeInvitationModel>() );
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {

            if ( !ModelState.IsValid ) {
                return Page();
            }

            if ( !await Database.CoupleHasAccepted( CoupleID, AdminCode ) ) {

                // Wrong id / admincode
                return NotFound();
            }

            if ( await Database.UpdateCoupleAsync( CoupleID, Couple ) == null ) {

                ModelState.AddModelError( "Couple", "Kan nu niet opslaan, probeer het later opnieuw." );
                return Page();
            }

            //Couple dbcouple = await Database.GetCoupleAcceptedAsAdmin( CoupleID, AdminCode );
            //if ( Couple == null ) {

            //    return NotFound();
            //}



            //Couple = await Database.UpdateCoupleAsync( CoupleID, Couple );
            //if ( Couple == null ) {
            //    // Error
            //    return BadRequest();
            //}

            return Page();

            //_context.Attach( Couple ).State = EntityState.Modified;

            //try {
            //    await _context.SaveChangesAsync();
            //} catch ( DbUpdateConcurrencyException ) {
            //    if ( !CoupleExists( Couple.ID ) ) {
            //        return NotFound();
            //    } else {
            //        throw;
            //    }
            //}

            //return Redirect( "./Index" );
        }

    }
}
