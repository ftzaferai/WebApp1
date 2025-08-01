using System.ComponentModel.DataAnnotations;

namespace PapaLuiPizzaria.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }

        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}