using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WalkingDinner.Pages {

    public class CoupleModel : PageModel {

        [BindProperty( SupportsGet = true )]
        public string CoupleID { get; set; }

        [BindProperty( SupportsGet = true )]
        public string AccessCode { get; set; }


        public void OnGet() {

        }
    }
}