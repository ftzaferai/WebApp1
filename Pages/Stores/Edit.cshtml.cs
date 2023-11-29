using PapaLuiPizzaria.Data;
using PapaLuiPizzaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;                // For GUID in SQLite version
using System.Linq;
using System.Threading.Tasks;

namespace PapaLuiPizzaria.Pages.Stores
{
    public class EditModel : PageModel
    {
        private readonly PapaLuiPizzaria.Data.SiteContext _context;

        public EditModel(PapaLuiPizzaria.Data.SiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Store Store { get; set; }
        // Replace ViewData["StoreID"] 
        public SelectList DeliveryNameSL { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Store = await _context.Stores
                .Include(d => d.Administrator)  // eager loading
                .AsNoTracking()                 // tracking not required
                .FirstOrDefaultAsync(m => m.StoreID == id);

            if (Store == null)
            {
                return NotFound();
            }

            // Use strongly typed data rather than ViewData.
            DeliveryNameSL = new SelectList(_context.Deliveries,
                "ID", "FirstMidName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch current store from DB.
            // ConcurrencyToken may have changed.
            var storeToUpdate = await _context.Stores
                .Include(i => i.Administrator)
                .FirstOrDefaultAsync(m => m.StoreID == id);

            if (storeToUpdate == null)
            {
                return HandleDeletedStore();
            }

            storeToUpdate.ConcurrencyToken = Guid.NewGuid();

            // Set ConcurrencyToken to value read in OnGetAsync
            _context.Entry(storeToUpdate).Property(d => d.ConcurrencyToken)
                                   .OriginalValue = Store.ConcurrencyToken;

            if (await TryUpdateModelAsync<Store>(
                storeToUpdate,
                "Store",
                s => s.Name, s => s.StartDate, s => s.Budget, s => s.DeliveryID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Store)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save. " +
                            "The store was deleted by another user.");
                        return Page();
                    }

                    var dbValues = (Store)databaseEntry.ToObject();
                    await SetDbErrorMessage(dbValues, clientValues, _context);

                    // Save the current ConcurrencyToken so next postback
                    // matches unless an new concurrency issue happens.
                    Store.ConcurrencyToken = dbValues.ConcurrencyToken;
                    // Clear the model error for the next postback.
                    ModelState.Remove($"{nameof(Store)}.{nameof(Store.ConcurrencyToken)}");
                }
            }

            DeliveryNameSL = new SelectList(_context.Deliveries,
                "ID", "FullName", storeToUpdate.DeliveryID);

            return Page();
        }

        private IActionResult HandleDeletedStore()
        {
            // ModelState contains the posted data because of the deletion error
            // and overides the Store instance values when displaying Page().
            ModelState.AddModelError(string.Empty,
                "Unable to save. The store was deleted by another user.");
            DeliveryNameSL = new SelectList(_context.Deliveries, "ID", "FullName", Store.DeliveryID);
            return Page();
        }

        private async Task SetDbErrorMessage(Store dbValues,
                Store clientValues, SiteContext context)
        {

            if (dbValues.Name != clientValues.Name)
            {
                ModelState.AddModelError("Store.Name",
                    $"Current value: {dbValues.Name}");
            }
            if (dbValues.Budget != clientValues.Budget)
            {
                ModelState.AddModelError("Store.Budget",
                    $"Current value: {dbValues.Budget:c}");
            }
            if (dbValues.StartDate != clientValues.StartDate)
            {
                ModelState.AddModelError("Store.StartDate",
                    $"Current value: {dbValues.StartDate:d}");
            }
            if (dbValues.DeliveryID != clientValues.DeliveryID)
            {
                Delivery dbDelivery = await _context.Deliveries
                   .FindAsync(dbValues.DeliveryID);
                ModelState.AddModelError("Store.DeliveryID",
                    $"Current value: {dbDelivery?.FullName}");
            }

            ModelState.AddModelError(string.Empty,
                "The record you attempted to edit "
              + "was modified by another user after you. The "
              + "edit operation was canceled and the current values in the database "
              + "have been displayed. If you still want to edit this record, click "
              + "the Save button again.");
        }
    }
}