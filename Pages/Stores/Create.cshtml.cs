using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;

namespace PapaLuiPizzaria.Pages.Stores
{
    public class CreateModel : PageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public CreateModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["DeliveryID"] = new SelectList(_context.Deliveries, "ID", "FirstMidName");
            return Page();
        }

        [BindProperty]
        public Store Store { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Stores.Add(Store);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
