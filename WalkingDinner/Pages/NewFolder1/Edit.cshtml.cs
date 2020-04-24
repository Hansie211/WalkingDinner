using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WalkingDinner.Database;
using WalkingDinner.Models;

namespace WalkingDinner.Pages.NewFolder1
{
    public class EditModel : PageModel
    {
        private readonly WalkingDinner.Database.DatabaseContext _context;

        public EditModel(WalkingDinner.Database.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["AdminID"] = new SelectList(_context.Persons, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Dinner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DinnerExists(Dinner.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DinnerExists(int id)
        {
            return _context.Dinners.Any(e => e.ID == id);
        }
    }
}
