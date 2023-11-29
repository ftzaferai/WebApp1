using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;

namespace PapaLuiPizzaria.Pages.Products
{
    public class CreateModel : StoreNamePageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public CreateModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateStoresDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyProduct = new Product();

            if (await TryUpdateModelAsync<Product>(
                 emptyProduct,
                 "product",   // Prefix for form value.
                 s => s.ProductID, s => s.StoreID, s => s.Title, s => s.Credits))
            {
                _context.Products.Add(emptyProduct);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select StoreID if TryUpdateModelAsync fails.
            PopulateStoresDropDownList(_context, emptyProduct.StoreID);
            return Page();
        }
    }
}
