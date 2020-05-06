using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WalkingDinner.Database;
using WalkingDinner.Models;

namespace WalkingDinner.Pages.Management {

    [Authorize( Policy = "AdminOnly" )]
    public class RemoveCoupleModel : DataBoundModel {

        public RemoveCoupleModel( DatabaseContext context ) : base( context ) {
        }

        [BindProperty( SupportsGet = true )]
        public int CoupleID { get; set; }

        [BindProperty( SupportsGet = true )]
        public string AdminCode { get; set; }

        public Couple Couple { get; set; }

        public async Task<IActionResult> OnGetAsync( int? IdToRemove ) {

            if ( IdToRemove == null ) {

                return NotFound();
            }

            Couple adminCouple = await Database.GetCoupleAsAdminAsync( CoupleID, AdminCode );
            if ( adminCouple == null ) {

                return NotFound();
            }

            Couple = await Database.GetCoupleAsync( IdToRemove.Value );
            if ( Couple == null ) {

                return NotFound();
            }

            if ( Couple.Dinner.ID != adminCouple.Dinner.ID ) {

                return BadRequest();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync( int? id ) {
            if ( id == null ) {
                return NotFound();
            }

            //Couple = await _context.Couples.FindAsync( id );

            //if ( Couple != null ) {
            //    _context.Couples.Remove( Couple );
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage( "./Index" );
        }
    }
}
