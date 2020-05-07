using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            Couple = await GetAuthorizedCouple();
            if ( Couple == null ) {

                return NotFound();
            }

            if ( Couple.Dinner.SubscriptionStop > DateTime.Now ) {

                return Redirect( ModelPath.Get<Management.EditDinnerModel>() );
            }

            if ( Couple.Dinner.Date < DateTime.Now ) {

                return Redirect( ModelPath.Get<Setup.CreateDinnerModel>() );
            }

            await Database.GetDinnerAsync( Couple.Dinner.ID );

            return null;
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

            int parallelMealCount   = totalCoupleCount / couplesPerGroup;

            // Rounding errors:
            totalCoupleCount = parallelMealCount * couplesPerGroup;

            Couple[] allCouples = ( await Database.GetCouplesAsync( Couple.Dinner.ID ) ).ToArray();
            Schema schema       = Schema.GenerateSchema( allCouples, courseCount );

            if ( !Schema.ValidSchema( schema, allCouples, totalCoupleCount ) ) {

                // ERROR

                ViewData[ "Schema-error" ] =  "Huidige indeling is niet mogelijk.";
                return Page();
            }

            // Send emails
            for ( int i = 0; i < schema.Courses.Length; i++ ) {

                foreach ( Meal meal in schema.Courses[ i ].Meals ) {

                    Couple chef = meal.Couples[ i ];

                    StringBuilder emailBody = new StringBuilder();

                    emailBody.Append( $"Hoi { chef.PersonMain.FirstName }" );
                    if ( chef.PersonGuest != null ) {

                        emailBody.Append( $" en { chef.PersonGuest.FirstName }" );
                    }
                    emailBody.AppendLine( "," );

                    emailBody.AppendLine( $"Jij/jullie gaan koken in ronde { i + 1 }! Jullie maken een '{ CourseSelection.Keys.ElementAt( i ) }'." );
                    emailBody.AppendLine( "De volgende dieetwensen zijn opgegeven:" );

                    bool hasGuidelines = false;
                    foreach ( Couple couple in meal.Couples ) {

                        if ( couple == chef ) {
                            continue;
                        }

                        string guidelines = couple.DietaryGuidelines?.Trim();
                        if ( !string.IsNullOrEmpty( guidelines ) ) {

                            emailBody.AppendLine( guidelines );
                            hasGuidelines = true;
                        }
                    }

                    if ( !hasGuidelines ) {
                        emailBody.AppendLine( "Geen." );
                    }

                    emailBody.AppendLine( "Veel plezier!" );

                    EmailServer.SendEmail( chef.EmailAddress, "Walking dinner!", emailBody.ToString() );
                }
            }


            PDF.Letter.Generate( allCouples[ 0 ], allCouples[ 1 ] );


            ViewData[ "message" ] = "Schema is opgeslagen.";
            return Page();
        }
    }
}