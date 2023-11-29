using System;
using System.ComponentModel.DataAnnotations;

namespace PapaLuiPizzaria.Models.SiteViewModels
{
    public class OrderDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }

        public int CustomerCount { get; set; }
    }
}