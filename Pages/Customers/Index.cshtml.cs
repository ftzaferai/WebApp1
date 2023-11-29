using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;
using Microsoft.Extensions.Configuration;

namespace PapaLuiPizzaria.Pages.Customers
{
public class IndexModel : PageModel
{
    private readonly SiteContext _context;
     private readonly IConfiguration Configuration;

    public IndexModel(SiteContext context, IConfiguration configuration)
    {
         _context = context;
         Configuration = configuration;
    }

    public string NameSort { get; set; }
    public string DateSort { get; set; }
    public string CurrentFilter { get; set; }
    public string CurrentSort { get; set; }

    public PaginatedList<Customer> Customers { get; set; }

    public async Task OnGetAsync(string sortOrder,
    string currentFilter, string searchString, int? pageIndex)
       {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

        CurrentFilter = searchString;

        IQueryable<Customer> customersIQ = from s in _context.Customers
                                        select s;
         if (!String.IsNullOrEmpty(searchString))
        {
            customersIQ = customersIQ.Where(s => s.LastName.Contains(searchString)
                                   || s.FirstMidName.Contains(searchString));
        }                               

        switch (sortOrder)
        {
            case "name_desc":
                customersIQ = customersIQ.OrderByDescending(s => s.LastName);
                break;
            case "Date":
                customersIQ = customersIQ.OrderBy(s => s.OrderDate);
                break;
            case "date_desc":
                customersIQ = customersIQ.OrderByDescending(s => s.OrderDate);
                break;
            default:
                customersIQ = customersIQ.OrderBy(s => s.LastName);
                break;
        }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Customers = await PaginatedList<Customer>.CreateAsync(
                customersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
    }
}
}
