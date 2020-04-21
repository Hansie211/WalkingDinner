using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WalkingDinner.Models;

namespace WalkingDinner.Database {

    public static class DinnerModelHelper {

        public static RNGCryptoServiceProvider RNG = new RNGCryptoServiceProvider();

        public static string GenerateCode( this Dinner dinner ) {

            byte[] data = new byte[32];
            RNG.GetBytes( data );

            return Convert.ToBase64String( data ).Substring(0,40);
        }
    }
}
