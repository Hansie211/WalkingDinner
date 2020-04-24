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
    public class IndexModel : PageModel
    {
        private readonly WalkingDinner.Database.DatabaseContext _context;

        public IndexModel(WalkingDinner.Database.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Dinner> Dinner { get;set; }

        public async Task OnGetAsync()
        {
            Dinner = await _context.Dinners
                .Include(d => d.Admin).ToListAsync();
        }
    }
}
