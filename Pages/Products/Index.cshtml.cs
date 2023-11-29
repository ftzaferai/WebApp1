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
    public class IndexModel : PageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public IndexModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get;set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .Include(c => c.Store)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
