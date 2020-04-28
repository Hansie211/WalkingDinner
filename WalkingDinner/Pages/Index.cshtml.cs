using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WalkingDinner.Database;
using WalkingDinner.Models;

namespace WalkingDinner.Pages {

    public class IndexModel : DataBoundModel {

        public IndexModel( DatabaseContext context ) : base( context ) {
        }

        public IActionResult OnGet() {

            return Page();

            //Person personA = new Person(){
            //    FirstName = "Persoon",
            //    LastName = "A",
            //};

            //Dinner dinner = await Database.CreateDinnerAsync( new Dinner(){

            //    Title               = "A diner",
            //    Description         = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus lacinia tortor id lacus malesuada molestie. Nulla fermentum vel massa eleifend porta. Praesent fermentum fringilla ligula, eu egestas est condimentum vitae. Aliquam auctor est at dolor molestie, vitae sollicitudin turpis blandit. Donec lorem ante, molestie quis orci ut, porttitor tempus tellus. Nam facilisis eget leo at porttitor. Morbi interdum velit ligula, a posuere turpis rhoncus et. Ut sit amet dictum turpis, vitae lacinia ipsum.",
            //    Location            = "Amsterdam, Netherlands",
            //    Date                = DateTime.Now.AddDays( 14 ),
            //    SubscriptionStop    = DateTime.Now.AddDays( 7 ),
            //    IBAN                = "NL000000559",
            //    Price               = 1.0f,

            //    Admin               = personA,
            //    AdminEmailAddress   = "admin@example.com",
            //});

            //// Dinner dinner = await Database.GetDinnerAsync(2);

            //Person personB = new Person(){
            //    FirstName = "Persoon",
            //    LastName = "B",
            //};

            //Person personC = new Person(){
            //    FirstName = "Persoon",
            //    LastName = "C",
            //};

            //Couple couple = new Couple(){
            //    Address = new Address(){
            //        Number = 63,
            //        NumberPostfix = "A",
            //        Place = "Amsterdam",
            //        Street = "AStreet",
            //        ZipCode = "111AA",
            //    },

            //    EmailAddress = "couple@example.com",
            //    Accepted = false,
            //    PersonMain = personB,
            //    PersonGuest = personC,
            //    PhoneNumber = "00316369639",
            //};

            //await Database.CreateCoupleAsync( dinner.ID, couple );


            //Database.Persons.Add(
            //    new Models.Person() {
            //        FirstName = "Henk",
            //        Preposition = "Van",
            //        LastName = "Achter",
            //    }
            //);

            //Database.SaveChanges();
        }
    }
}
