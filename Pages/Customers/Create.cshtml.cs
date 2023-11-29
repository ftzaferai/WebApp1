using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;

namespace PapaLuiPizzaria.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public CreateModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
{
    var emptyCustomer = new Customer();

    if (await TryUpdateModelAsync<Customer>(
        emptyCustomer,
        "customer",   // Prefix for form value.
        s => s.FirstMidName, s => s.LastName, s => s.OrderDate))
    {
        _context.Customers.Add(emptyCustomer);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }

    return Page();
}
    }
}
