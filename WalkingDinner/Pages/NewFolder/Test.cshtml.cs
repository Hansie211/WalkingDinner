using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WalkingDinner.Database;
using WalkingDinner.Models;

namespace WalkingDinner.Pages.NewFolder
{
    public class TestModel : PageModel
    {
        private readonly WalkingDinner.Database.DatabaseContext _context;

        public TestModel(WalkingDinner.Database.DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AdminID"] = new SelectList(_context.Persons, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Dinner Dinner { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Dinners.Add(Dinner);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
