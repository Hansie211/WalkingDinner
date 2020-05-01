using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalkingDinner.Extensions {

    public static class ModelPath {

        private const string PAGE_ROOT_NAMESPACE = "WalkingDinner.Pages";
        private static readonly int PAGE_ROOT_NAMESPACE_LEN = PAGE_ROOT_NAMESPACE.Length + 1;

        public static string Get<T>() where T : PageModel {

            Type pageType   = typeof(T);

            string fullName = pageType.FullName;
            int fullNameLen = fullName.Length;

            // Subtract 'Model' from type name
            fullNameLen -= 5;

            string absoluteName = '/' + fullName.Substring( PAGE_ROOT_NAMESPACE_LEN, fullNameLen - PAGE_ROOT_NAMESPACE_LEN  );

            return absoluteName.Replace( '.', '/' );
        }

        public static string Get<T>( params object[] parameters ) where T : PageModel {

            StringBuilder result = new StringBuilder( Get<T>() + '/' );

            if ( parameters != null ) {

                foreach ( object p in parameters ) {

                    string s = p?.ToString();
                    result.Append( s + '/' );
                }
            }

            return result.ToString();
        }

        public static string GetAbsolutePath<T>( HostString RequestHost, params object[] parameters ) where T : PageModel {

            return $"https://{ RequestHost }{ Get<T>( parameters )}";

            //StringBuilder result = new StringBuilder( $"https://{ RequestHost }{ Get<T>() }/" );

            //if ( parameters != null ) {
            //    foreach ( object p in parameters ) {

            //        string s = p?.ToString();

            //        result.Append( s + '/' );
            //    }
            //}

            //return result.ToString();
        }
    }
}
