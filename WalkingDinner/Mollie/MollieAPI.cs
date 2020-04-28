using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WalkingDinner.Mollie.Models;

namespace WalkingDinner.Mollie {

    // API:
    // https://docs.mollie.com/payments/overview

    public static partial class MollieAPI {

        private static readonly HttpClient client;
        public static readonly string HostURL = $"https://api.mollie.com/v2/";

        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings(){
            ContractResolver = new DefaultContractResolver(){
                NamingStrategy = new CamelCaseNamingStrategy(),
            },
        };

        static MollieAPI() {

            client    = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "bearer", Key );

            client.BaseAddress = new Uri( HostURL );
        }

        private static StringContent JSONContent( object content ) {

            string data = JsonConvert.SerializeObject( content, jsonSerializerSettings );
            return new StringContent( data, Encoding.UTF8, "application/json" );
        }

        public static async Task<T> PostAsync<T>( string path, object Object ) where T : class {

            try {
                StringContent content = JSONContent( Object );
                HttpResponseMessage response = await client.PostAsync( path, content );

                bool success    = response.IsSuccessStatusCode;
                string body     = await response.Content.ReadAsStringAsync();

                if ( !success ) {
                    return null;
                }

                return JsonConvert.DeserializeObject<T>( body, jsonSerializerSettings );

            } catch ( Exception exp ) {
                Console.WriteLine( exp.Message );
                return null;
            }
        }

        // private static 

        public static async Task<PaymentResponse> TestRequest() {

            Payment payment = new Payment(){
                Amount = new Amount( Currency.EUR, 100.00 ),
                Description = "Een test betaling",
                RedirectUrl = "https://www.google.com",
                // Testmode = true,
            };

            return await PaymentRequest( payment );
        }

        public static async Task<PaymentResponse> PaymentRequest( Payment payment ) {

            PaymentResponse result = await PostAsync<PaymentResponse>( "payments", payment );

            if ( result == null ) {
                return null;
            }

            return result;
        }

        public static async Task<string> GetPaymentStatus( string paymentId ) {

            dynamic result = await PostAsync<dynamic>( $"payments/{ paymentId }", new {} );
            if ( result == null ) {
                return null;
            }

            return result.status;
        }
    }
}
