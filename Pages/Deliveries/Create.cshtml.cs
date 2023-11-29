using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PapaLuiPizzaria.Pages.Deliveries
{
    public class CreateModel : DeliveryProductsPageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;
        private readonly ILogger<DeliveryProductsPageModel> _logger;

        public CreateModel(SiteContext context,
                          ILogger<DeliveryProductsPageModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            var delivery = new Delivery();
            delivery.Products = new List<Product>();

            // Provides an empty collection for the foreach loop
            // foreach (var product in Model.AssignedProductDataList)
            // in the Create Razor page.
            PopulateAssignedProductData(_context, delivery);
            return Page();
        }

        [BindProperty]
        public Delivery Delivery { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedProducts)
        {
            var newDelivery = new Delivery();

            if (selectedProducts.Length > 0)
            {
                newDelivery.Products = new List<Product>();
                // Load collection with one DB call.
                _context.Products.Load();
            }

            // Add selected Products products to the new Delivery.
            foreach (var product in selectedProducts)
            {
                var foundProduct = await _context.Products.FindAsync(int.Parse(product));
                if (foundProduct != null)
                {
                    newDelivery.Products.Add(foundProduct);
                }
                else
                {
                    _logger.LogWarning("Product {product} not found", product);
                }
            }

            try
            {
                if (await TryUpdateModelAsync<Delivery>(
                                newDelivery,
                                "Delivery",
                                i => i.FirstMidName, i => i.LastName,
                                i => i.HireDate, i => i.StoreAssignment))
                {
                    _context.Deliveries.Add(newDelivery);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            PopulateAssignedProductData(_context, newDelivery);
            return Page();
        }
    }
}