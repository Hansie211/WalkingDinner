using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WalkingDinner.Calculation.Models;
using WalkingDinner.Database;
using WalkingDinner.Extensions;
using WalkingDinner.Models;

namespace WalkingDinner.Pages.Management {

    public class OverviewModel : DataBoundModel {

        public OverviewModel( DatabaseContext context ) : base( context ) {
        }

        [BindProperty( SupportsGet = true )]
        public int CoupleID { get; set; }

        [BindProperty( SupportsGet = true )]
        public string AdminCode { get; set; }

        public Couple Couple { get; set; }

        public IEnumerable<KeyValuePair<string, bool>> MenuItems { get; } = new[] {

            new KeyValuePair<string, bool>( "Aperitief met amuse",  false ),
            new KeyValuePair<string, bool>( "Koud voorgerecht",     true ),
            new KeyValuePair<string, bool>( "Warm voorgerecht",     true ),
            new KeyValuePair<string, bool>( "Hoofdgerecht",         false ),
            new KeyValuePair<string, bool>( "Nagerecht",            true ),
        };

        [BindProperty]
        public Dictionary<string, bool> CourseSelection { get; set; }

        private async Task<IActionResult> GetCoupleAsync() {

            Couple = await Database.GetCoupleAsAdminAsync( CoupleID, AdminCode );
            if ( Couple == null ) {

                return NotFound();
            }

            if ( !Couple.IsAdmin ) {

                return BadRequest();
            }

            if ( Couple.Dinner.SubscriptionStop > DateTime.Now ) {

                return Redirect( ModelPath.Get<Management.EditDinnerModel>() );
            }

            if ( Couple.Dinner.Date < DateTime.Now ) {

                return BadRequest();
            }

            return null;
        }

        private void ShuffleArray<T>( ref T[] array ) {

            Random random = new Random();
            for ( int i = 0; i < array.Length; i++ ) {

                int newIndex = random.Next( 0, array.Length );

                T temp              = array[ i ];
                array[ i ]          = array[ newIndex ];
                array[ newIndex ]   = temp;
            }
        }


        public async Task<IActionResult> OnGetAsync() {

            var getResult = await GetCoupleAsync();
            if ( getResult != null ) {

                return getResult;
            }

            CourseSelection = MenuItems.ToDictionary( x => x.Key, x => x.Value );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {

            if ( !ModelState.IsValid ) {
                return Page();
            }

            var getResult = await GetCoupleAsync();
            if ( getResult != null ) {

                return getResult;
            }

            int courseCount = 0;
            foreach ( var entry in CourseSelection ) {

                if ( !entry.Value ) {
                    continue;
                }

                courseCount++;
            }

            if ( courseCount < 2 ) {
                ModelState.AddModelError( "CourseSelection", "Kies minimaal 2 gangen!" );
                return Page();
            }

            int totalCoupleCount    = Couple.Dinner.Couples.Count;
            int couplesPerGroup     = courseCount;

            int parallelMealCount  = totalCoupleCount / couplesPerGroup;

            // Rounding errors:
            totalCoupleCount = parallelMealCount * couplesPerGroup;

            Couple[] allCouples     = ( await Database.GetCouplesAsync( Couple.Dinner.ID ) ).ToArray();
            // ShuffleArray( ref allCouples );

            Schema schema = Schema.GenerateSchema( allCouples, courseCount );

            return Page();
        }
    }
}