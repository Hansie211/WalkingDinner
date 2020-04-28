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

        private async Task<Dinner> GetDinnerAsync( int Id ) {

            Dinner dinner = await Database.Dinners.Include( o => o.Couples ).ThenInclude( o => o.PersonMain )
                                                  .Include( o => o.Couples ).ThenInclude( o => o.PersonGuest )
                                                  .Include( o => o.Couples ).ThenInclude( o => o.Address )
                                                  .Include( o => o.Admin )
                                                  .SingleOrDefaultAsync( o => o.ID == Id );
            if ( dinner == null ) {

                return null;
            }

            return dinner;
        }

        public async Task<Dinner> GetDinnerAsAdmin( int Id, string adminCode ) {

            Dinner dinner = await GetDinnerAsync( Id );
            if ( dinner == null ) {
                return null;
            }

            if ( dinner.AdminCode != adminCode ) {
                return null;
            }

            return dinner;
        }

        public async Task<Dinner> GetDinnerAsAccess( int Id, string accessCode ) {

            Dinner dinner = await GetDinnerAsync( Id );
            if ( dinner == null ) {
                return null;
            }

            if ( dinner.AccessCode != accessCode ) {
                return null;
            }

            return dinner;
        }

        public async Task<Dinner> RemoveDinnerAsync( int Id ) {

            Dinner dinner = await Database.Dinners.FindAsync( Id );
            if ( dinner == null ) {
                return null;
            }

            Database.Remove( dinner );
            await Database.SaveChangesAsync();

            return dinner;
        }

        public async Task<Dinner> UpdateDinnerAsync( int Id, Dinner dinnerData ) {

            Dinner dinner = await Database.Dinners.FindAsync( Id );
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

        private async Task<Couple> GetCoupleAsync( int Id ) {

            Couple couple = await Database.Couples.Include( o => o.Dinner )
                                                  .Include( o => o.PersonMain )
                                                  .Include( o => o.PersonGuest )
                                                  .Include( o => o.Address )
                                                  .SingleOrDefaultAsync( o => o.ID == Id );
            if ( couple == null ) {
                return null;
            }

            return couple;
        }

        public async Task<Couple> GetCoupleAsAdminAsync( int Id, string AdminCode ) {

            Couple couple = await GetCoupleAsync( Id );
            if ( couple == null ) {

                return null;
            }

            if ( couple.AdminCode != AdminCode ) {

                return null;
            }

            return couple;
        }

        public async Task<Couple> GetCoupleAcceptedAsAdmin( int Id, string AdminCode ) {

            Couple couple = await GetCoupleAsAdminAsync( Id, AdminCode );
            if ( couple == null ) {
                return null;
            }

            if ( !couple.Accepted ) {
                return null;
            }

            return couple;
        }

        public async Task<Couple> UpdateCoupleAsync( int Id, Couple coupleData ) {

            Couple couple = await GetCoupleAsync( Id );
            if ( couple == null ) {
                return null;
            }

            couple.PhoneNumber  = coupleData.PhoneNumber;
            couple.PersonMain.FirstName     = coupleData.PersonMain.FirstName;
            couple.PersonMain.Preposition   = coupleData.PersonMain.Preposition;
            couple.PersonMain.LastName      = coupleData.PersonMain.LastName;

            if ( coupleData.PersonGuest == null ) {

                couple.PersonGuest = null;

            } else if ( couple.PersonGuest == null ) {

                couple.PersonGuest = coupleData.PersonGuest;
            } else {

                couple.PersonGuest.FirstName     = coupleData.PersonGuest.FirstName;
                couple.PersonGuest.Preposition   = coupleData.PersonGuest.Preposition;
                couple.PersonGuest.LastName      = coupleData.PersonGuest.LastName;
            }

            // couple.Address      = coupleData.Address;

            Database.Update( couple );
            await Database.SaveChangesAsync();

            return couple;
        }

        public async Task<Couple> RemoveCoupleAsync( int Id ) {

            Couple couple = await Database.Couples.FindAsync( Id );
            if ( couple == null ) {
                return null;
            }

            Database.Remove( couple );
            await Database.SaveChangesAsync();

            return couple;
        }

    }
}
