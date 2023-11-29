using PapaLuiPizzaria.Models.SiteViewModels;
using PapaLuiPizzaria.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PapaLuiPizzaria.Models;

namespace PapaLuiPizzaria.Pages
{
    public class AboutModel : PageModel
    {
        private readonly SiteContext _context;

        public AboutModel(SiteContext context)
        {
            _context = context;
        }

        public IList<OrderDateGroup> Customers { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<OrderDateGroup> data =
                from customer in _context.Customers
                group customer by customer.OrderDate into dateGroup
                select new OrderDateGroup()
                {
                    OrderDate = dateGroup.Key,
                    CustomerCount = dateGroup.Count()
                };

            Customers = await data.AsNoTracking().ToListAsync();
        }
    }
}