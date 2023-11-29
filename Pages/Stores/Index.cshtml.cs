using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;

namespace PapaLuiPizzaria.Pages.Stores
{
    public class IndexModel : PageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public IndexModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

        public IList<Store> Store { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Stores != null)
            {
                Store = await _context.Stores
                .Include(s => s.Administrator).ToListAsync();
            }
        }
    }
}
