using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WalkingDinner.Mollie.Models {

    public static class Currency {

        public static readonly string EUR = "EUR";
    }

    public class Amount {

        public string Currency { get; set; }
        public string Value { get; set; }

        public Amount() { }

        public Amount( string currency, double value ) {
            Currency    = currency;
            Value       = Math.Round( value, 2 ).ToString( "#.00" );
        }

        public override string ToString() {

            return $"{ Value } { Currency }";
        }
    }
}
