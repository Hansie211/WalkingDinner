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

namespace WalkingDinner.Pages.Invitation {

    public class IndexModel : DataBoundModel {

        public IndexModel( DatabaseContext context ) : base( context ) {
        }

        [BindProperty( SupportsGet = true )]
        public int CoupleID { get; set; }

        [BindProperty( SupportsGet = true )]
        public string AdminCode { get; set; }

        [BindProperty]
        public Couple Couple { get; set; }

        public async Task<IActionResult> OnGetAsync() {
            Couple = await Database.GetCoupleAcceptedAsAdmin( CoupleID, AdminCode );

            if ( Couple == null ) {

                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {

            if ( !ModelState.IsValid ) {
                return Page();
            }

            //Couple dbcouple = await Database.GetCoupleAcceptedAsAdmin( CoupleID, AdminCode );
            //if ( Couple == null ) {

            //    return NotFound();
            //}

            Couple = await Database.UpdateCoupleAsync( CoupleID, Couple );
            if ( Couple == null ) {
                // Error
                return BadRequest();
            }

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

            //return RedirectToPage( "./Index" );
        }

    }
}
