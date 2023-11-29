using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;
using PapaLuiPizzaria.Models.SiteViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace PapaLuiPizzaria.Pages.Deliveries
{
    public class DeliveryProductsPageModel : PageModel
    {
        public List<AssignedProductData> AssignedProductDataList;

        public void PopulateAssignedProductData(SiteContext context,
                                               Delivery delivery)
        {
            var allProducts = context.Products;
            var deliveryProducts = new HashSet<int>(
                delivery.Products.Select(c => c.ProductID));
            AssignedProductDataList = new List<AssignedProductData>();
            foreach (var product in allProducts)
            {
                AssignedProductDataList.Add(new AssignedProductData
                {
                    ProductID = product.ProductID,
                    Title = product.Title,
                    Assigned = deliveryProducts.Contains(product.ProductID)
                });
            }
        }
    }
}