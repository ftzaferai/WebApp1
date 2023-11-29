using PapaLuiPizzaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PapaLuiPizzaria.Data;

namespace PapaLuiPizzaria.Pages.Deliveries
{
    public class EditModel : DeliveryProductsPageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public EditModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Delivery Delivery { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Delivery = await _context.Deliveries
                .Include(i => i.StoreAssignment)
                .Include(i => i.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Delivery == null)
            {
                return NotFound();
            }
            PopulateAssignedProductData(_context, Delivery);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedProducts)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryToUpdate = await _context.Deliveries
                .Include(i => i.StoreAssignment)
                .Include(i => i.Products)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (deliveryToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Delivery>(
                deliveryToUpdate,
                "Delivery",
                i => i.FirstMidName, i => i.LastName,
                i => i.HireDate, i => i.StoreAssignment))
            {
                if (String.IsNullOrWhiteSpace(
                    deliveryToUpdate.StoreAssignment?.Location))
                {
                    deliveryToUpdate.StoreAssignment = null;
                }
                UpdateDeliveryProducts(selectedProducts, deliveryToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateDeliveryProducts(selectedProducts, deliveryToUpdate);
            PopulateAssignedCourseData(_context, deliveryToUpdate);
            return Page();
        }

        private void PopulateAssignedCourseData(SiteContext context, Delivery deliveryToUpdate)
        {
            throw new NotImplementedException();
        }

        public void UpdateDeliveryProducts(string[] selectedProducts,
                                            Delivery deliveryToUpdate)
        {
            if (selectedProducts == null)
            {
                deliveryToUpdate.Products = new List<Product>();
                return;
            }

            var selectedProductsHS = new HashSet<string>(selectedProducts);
            var deliveryProducts = new HashSet<int>
                (deliveryToUpdate.Products.Select(c => c.ProductID));
            foreach (var product in _context.Products)
            {
                if (selectedProductsHS.Contains(product.ProductID.ToString()))
                {
                    if (!deliveryProducts.Contains(product.ProductID))
                    {
                        deliveryToUpdate.Products.Add(product);
                    }
                }
                else
                {
                    if (deliveryProducts.Contains(product.ProductID))
                    {
                        var productToRemove = deliveryToUpdate.Products.Single(
                                                        c => c.ProductID == product.ProductID);
                        deliveryToUpdate.Products.Remove(productToRemove);
                    }
                }
            }
        }
    }
}