using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WalkingDinner.Models;

namespace WalkingDinner.Database {

    public class DatabaseManager {

        public readonly DatabaseContext Database;
        private static readonly RNGCryptoServiceProvider RNG = new RNGCryptoServiceProvider();

        private static string EncodeBytes( byte[] data ) {

            return Convert.ToBase64String( data ).Replace( '+', '-' ).Replace( '/', '.' ).TrimEnd( '=' );
        }

        private static string GenerateAccessCode() {

            byte[] data = new byte[32];
            RNG.GetBytes( data );

            return EncodeBytes( data ).Substring( 0, 40 );
        }

        public DatabaseManager( DatabaseContext context ) {

            Database = context;
        }

        public async Task<Dinner> CreateDinnerAsync( Dinner dinner ) {

            dinner.AdminEmailAddress = dinner.AdminEmailAddress?.ToLower();
            dinner.AdminCode    = GenerateAccessCode();
            dinner.AccessCode   = GenerateAccessCode();

            Database.Add( dinner );
            await Database.SaveChangesAsync();

            return dinner;
        }

        public async Task<Dinner> GetDinnerAsync( int dinnerID ) {

            Dinner dinner = await Database.Dinners.Include( o => o.Couples ).ThenInclude( o => o.PersonMain )
                                                  .Include( o => o.Couples ).ThenInclude( o => o.PersonGuest )
                                                  .Include( o => o.Couples ).ThenInclude( o => o.Address )
                                                  .Include( o => o.Admin )
                                                  .SingleOrDefaultAsync( o => o.ID == dinnerID );
            if ( dinner == null ) {

                return null;
            }

            return dinner;
        }

        public async Task<Dinner> GetDinnerAsAdmin( int dinnerID, string adminCode ) {

            Dinner dinner = await GetDinnerAsync( dinnerID );
            if ( dinner == null ) {
                return null;
            }

            if ( dinner.AdminCode != adminCode ) {
                return null;
            }

            return dinner;
        }

        public async Task<Dinner> GetDinnerAsAccess( int dinnerID, string accessCode ) {

            Dinner dinner = await GetDinnerAsync( dinnerID );
            if ( dinner == null ) {
                return null;
            }

            if ( dinner.AccessCode != accessCode ) {
                return null;
            }

            return dinner;
        }

        public async Task<Dinner> RemoveDinnerAsync( int dinnerID ) {

            Dinner dinner = await Database.Dinners.FindAsync( dinnerID );
            if ( dinner == null ) {
                return null;
            }

            Database.Remove( dinner );
            await Database.SaveChangesAsync();

            return dinner;
        }

        public async Task<Dinner> UpdateDinnerAsync( Dinner dinnerData ) {

            Dinner dinner = await Database.Dinners.FindAsync( dinnerData.ID );
            if ( dinner == null ) {
                return null;
            }

            dinner.Title        = dinnerData.Title;
            dinner.Description  = dinnerData.Description;
            dinner.Location     = dinnerData.Location;
            dinner.Date         = dinnerData.Date;
            dinner.SubscriptionStop = dinnerData.SubscriptionStop;
            dinner.Price        = dinnerData.Price;
            dinner.IBAN         = dinnerData.IBAN;

            dinner.AdminEmailAddress    = dinnerData.AdminEmailAddress?.ToLower();
            dinner.Admin                = dinnerData.Admin;

            Database.Update( dinner );
            await Database.SaveChangesAsync();

            return dinner;
        }

        public async Task<Couple> CreateCoupleAsync( int dinnerID, Couple couple ) {

            Dinner dinner = await Database.Dinners.FindAsync( dinnerID );
            if ( dinner == null ) {
                return null;
            }

            couple.EmailAddress = couple.EmailAddress?.ToLower();
            couple.AdminCode    = GenerateAccessCode();

            dinner.Couples.Add( couple );
            await Database.SaveChangesAsync();

            return couple;
        }


        public async Task<Couple> UpdateCoupleAsync( Couple coupleData ) {

            Couple couple = await Database.Couples.FindAsync( coupleData.ID );
            if ( couple == null ) {
                return null;
            }

            couple.PhoneNumber  = coupleData.PhoneNumber;
            couple.PersonMain   = coupleData.PersonMain;
            couple.PersonGuest  = coupleData.PersonGuest;
            couple.Address      = coupleData.Address;

            Database.Update( couple );
            await Database.SaveChangesAsync();

            return couple;
        }

        public async Task<Couple> RemoveCoupleAsync( int coupleID ) {

            Couple couple = await Database.Couples.FindAsync( coupleID );
            if ( couple == null ) {
                return null;
            }

            Database.Remove( couple );
            await Database.SaveChangesAsync();

            return couple;
        }

    }
}
