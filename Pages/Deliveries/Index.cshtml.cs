using PapaLuiPizzaria.Models;
using PapaLuiPizzaria.Models.SiteViewModels;  // Add VM
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PapaLuiPizzaria.Pages.Deliveries
{
    public class IndexModel : PageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public IndexModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

        public DeliveryIndexData DeliveryData { get; set; }
        public int DeliveryID { get; set; }
        public int ProductID { get; set; }

        public async Task OnGetAsync(int? id, int? productID)
        {
            DeliveryData = new DeliveryIndexData();
            DeliveryData.Deliveries = await _context.Deliveries
                .Include(i => i.StoreAssignment)                 
                .Include(i => i.Products)
                    .ThenInclude(c => c.Store)
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null)
            {
                DeliveryID = id.Value;
                Delivery delivery = DeliveryData.Deliveries
                    .Where(i => i.ID == id.Value).Single();
                DeliveryData.Products = delivery.Products;
            }

            if (productID != null)
            {
                ProductID = productID.Value;
                IEnumerable<Order> Orders = await _context.Orders
                    .Where(x => x.ProductID == ProductID)                    
                    .Include(i=>i.Customer)
                    .ToListAsync();                 
                DeliveryData.Orders = Orders;
            }
        }
    }
}