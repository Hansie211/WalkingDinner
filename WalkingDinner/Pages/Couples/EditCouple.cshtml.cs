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

namespace WalkingDinner.Pages.Couples {

    public class EditCoupleModel : DataBoundModel {

        public EditCoupleModel( DatabaseContext context ) : base( context ) {
        }

        [BindProperty]
        public Couple Couple { get; set; }

        public async Task<IActionResult> OnGetAsync() {

            Couple = await GetAuthorizedCouple();
            if ( Couple == null ) {

                return NotFound();
            }

            if ( Couple.PaymentStatus != null && Couple.PaymentId != null && !Couple.HasPayed ) {

                string paymentStatus = await MollieAPI.GetPaymentStatus( Couple.PaymentId );
                if ( paymentStatus != Couple.PaymentStatus ) {

                    Couple.PaymentStatus = paymentStatus;
                    await Database.SaveChangesAsync();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {

            if ( !ModelState.IsValid ) {
                return await OnGetAsync();
            }

            if ( await Database.UpdateCoupleAsync( AuthorizedID, Couple ) == null ) {

                ModelState.AddModelError( "Couple", "Kan nu niet opslaan, probeer het later opnieuw." );
                return await OnGetAsync();
            }

            ViewData[ "status" ] = "Wijzigingen opgeslagen!";

            return await OnGetAsync();
        }

    }
}
