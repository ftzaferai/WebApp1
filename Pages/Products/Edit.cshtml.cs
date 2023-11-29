using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;

namespace PapaLuiPizzaria.Pages.Products
{
    public class EditModel : StoreNamePageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public EditModel(PapaLuiPizzaria.Data.SiteContext context)
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
                .Include(c => c.Store).FirstOrDefaultAsync(m => m.ProductID == id);

            if (Product == null)
            {
                return NotFound();
            }
            // Select current StoreID.
            PopulateStoresDropDownList(_context, Product.StoreID);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToUpdate = await _context.Products.FindAsync(id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Product>(
                 productToUpdate,
                 "product",   // Prefix for form value.
                   c => c.Credits, c => c.StoreID, c => c.Title))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select StoreID if TryUpdateModelAsync fails.
            PopulateStoresDropDownList(_context, productToUpdate.StoreID);
            return Page();
        }  
    }
}
