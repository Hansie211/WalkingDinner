using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WalkingDinner.Database;
using WalkingDinner.Extensions;
using WalkingDinner.Models;
using WalkingDinner.Mollie;
using WalkingDinner.Mollie.Models;

namespace WalkingDinner.Pages.Invitation {
    public class PaymentModel : DataBoundModel {


        public PaymentModel( DatabaseContext context ) : base( context ) {
        }

        [BindProperty( SupportsGet = true )]
        public int CoupleID { get; set; }

        [BindProperty( SupportsGet = true )]
        public string AdminCode { get; set; }


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

            PaymentResponse response = await MollieAPI.PaymentRequest(
                new Payment(){
                    Amount = new Amount( Currency.EUR, couple.Dinner.Price ),
                    Description = "Betaling voor het dinner",
                    RedirectUrl = ModelPath.GetAbsolutePath<RedirectPaymentModel>( Request.Host, CoupleID, AdminCode ),
                }
            );

            string url = response?._links["checkout"]?.Href;
            if ( string.IsNullOrEmpty( url ) ) {

                // Error
                return Page();
            }

            // Save the paymentId
            couple.PaymentId = response.Id;
            await Database.SaveChangesAsync();


            // Go to mollie
            return Redirect( url );
        }
    }
}
