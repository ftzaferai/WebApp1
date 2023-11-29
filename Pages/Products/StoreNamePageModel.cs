using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PapaLuiPizzaria.Pages.Products
{
    public class StoreNamePageModel : PageModel
    {
        public SelectList StoreNameSL { get; set; }

        public void PopulateStoresDropDownList(SiteContext _context,
            object selectedStore = null)
        {
            var storesQuery = from d in _context.Stores
                                   orderby d.Name // Sort by name.
                                   select d;

            StoreNameSL = new SelectList(storesQuery.AsNoTracking(),
                nameof(Store.StoreID),
                nameof(Store.Name),
                selectedStore);
        }
    }
}