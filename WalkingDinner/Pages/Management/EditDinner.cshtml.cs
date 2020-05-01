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

namespace WalkingDinner.Pages.Management {

    public class EditDinnerModel : DataBoundModel {

        public EditDinnerModel( DatabaseContext context ) : base( context ) {
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
        public int CoupleID { get; set; }

        [BindProperty( SupportsGet = true )]
        public string AdminCode { get; set; }

        [BindProperty]
        public Couple Couple { get; set; }

        public class PersonInvite {

            [Required]
            public PersonMain Person { get; set; }

            [Required]
            [DataType( DataType.EmailAddress )]
            public string EmailAddress { get; set; }
        }

        [BindProperty]
        public PersonInvite Invite { get; set; }

        public async Task<IActionResult> OnGetAsync() {

            Couple = await Database.GetCoupleAsAdminAsync( CoupleID, AdminCode );
            if ( Couple == null ) {

                return NotFound();
            }

            if ( !Couple.IsAdmin ) {

                return NotFound();
            }

            if ( !Couple.Accepted ) {

                Couple.Accepted = true;
                await Database.SaveChangesAsync();
            }

            // Load the dinner itself, including other couples
            await Database.GetDinnerAsync( Couple.Dinner.ID );

            return Page();
        }

        public async Task<IActionResult> OnPostInvite() {

            Couple = await Database.GetCoupleAsAdminAsync( CoupleID, AdminCode );
            if ( Couple == null ) {

                return NotFound();
            }

            if ( !ModelState.IsValid( nameof( Invite ) ) ) {

                return Page();
            }

            Invite.EmailAddress = Invite.EmailAddress.ToLower();

            foreach ( Couple storedCouple in Couple.Dinner.Couples ) {

                if ( storedCouple.EmailAddress == Invite.EmailAddress ) {

                    ModelState.AddModelError( nameof( Invite ), $"{ Invite.Person } met emailadres { Invite.EmailAddress } is al uitgenodigd." );
                    return Page();
                }
            }

            // Invite
            Couple invitedCouple = await Database.CreateCoupleAsync( Couple.Dinner.ID, Invite.EmailAddress, Invite.Person );

            if ( invitedCouple == null ) {

                ModelState.AddModelError( nameof( Invite ), $"Kan { Invite.Person } nu niet uitnodigen." );
                return Page();
            }

            EmailServer.SendEmail( invitedCouple.EmailAddress, "Uitnodiging",
                $"U ben uitgenodigd door { Couple.PersonMain } om deel te namen aan een WalkingDinner. Bekijk uit uitnodiging <a href=\"{ ModelPath.GetAbsolutePath<Invitation.SeeInvitationModel>( Request.Host, invitedCouple.ID, invitedCouple.AdminCode ) }\">Hier</a>" );

            ViewData[ "InviteResult" ] = $"{ Invite.Person } is uitgenodigd.";


            // Clear input data
            ModelState.Clear( nameof( Invite ) );
            Invite = null;

            return Page();
        }
    }
}
