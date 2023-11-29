using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PapaLuiPizzaria.Models.SiteViewModels
{
    public class DeliveryIndexData
    {
        public IEnumerable<Delivery> Deliveries { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        
    }
}