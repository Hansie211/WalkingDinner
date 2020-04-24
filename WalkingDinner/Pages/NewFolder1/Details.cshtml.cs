using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WalkingDinner.Database;
using WalkingDinner.Models;

namespace WalkingDinner.Pages.NewFolder1
{
    public class DetailsModel : PageModel
    {
        private readonly WalkingDinner.Database.DatabaseContext _context;

        public DetailsModel(WalkingDinner.Database.DatabaseContext context)
        {
            _context = context;
        }

        public Dinner Dinner { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dinner = await _context.Dinners
                .Include(d => d.Admin).FirstOrDefaultAsync(m => m.ID == id);

            if (Dinner == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
