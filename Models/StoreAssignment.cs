using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapaLuiPizzaria.Models
{
    public class StoreAssignment
    {
        [Key]
        public int DeliveryID { get; set; }
        [StringLength(50)]
        [Display(Name = "Store Location")]
        public string Location { get; set; }

        public Delivery Delivery { get; set; }
    }
}