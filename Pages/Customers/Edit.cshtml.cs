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

namespace PapaLuiPizzaria.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public EditModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

 public async Task<IActionResult> OnGetAsync(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    Customer = await _context.Customers.FindAsync(id);

    if (Customer == null)
    {
        return NotFound();
    }
    return Page();
}

public async Task<IActionResult> OnPostAsync(int id)
{
    var customerToUpdate = await _context.Customers.FindAsync(id);

    if (customerToUpdate == null)
    {
        return NotFound();
    }

    if (await TryUpdateModelAsync<Customer>(
        customerToUpdate,
        "customer",
        s => s.FirstMidName, s => s.LastName, s => s.OrderDate))
    {
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }

    return Page();
    }
  }
}
