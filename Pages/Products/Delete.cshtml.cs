using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;

namespace PapaLuiPizzaria.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public DeleteModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

         Product = await _context.Products
             .AsNoTracking()
             .Include(c => c.Store)
             .FirstOrDefaultAsync(m => m.ProductID == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                Product = product;
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
