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
    public class DetailsModel : PageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public DetailsModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

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
    }
}
