using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;

namespace PapaLuiPizzaria.Pages.Deliveries
{
    public class DeleteModel : PageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public DeleteModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Delivery Delivery { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Deliveries == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries.FirstOrDefaultAsync(m => m.ID == id);

            if (delivery == null)
            {
                return NotFound();
            }
            else 
            {
                Delivery = delivery;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Delivery delivery = await _context.Deliveries
                .Include(i => i.    Products)
                .SingleAsync(i => i.ID == id);

            if (delivery == null)
            {
                return RedirectToPage("./Index");
            }

            var stores = await _context.Stores
                .Where(d => d.DeliveryID == id)
                .ToListAsync();
            stores.ForEach(d => d.DeliveryID = null);

            _context.Deliveries.Remove(delivery);

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
