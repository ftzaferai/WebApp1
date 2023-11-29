using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;

namespace PapaLuiPizzaria.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public DetailsModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

      public Customer Customer { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            Customer = await _context.Customers
                .Include(s => s.Orders)
                .ThenInclude(e => e.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
                
            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
