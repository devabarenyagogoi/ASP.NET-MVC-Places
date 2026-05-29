// C# classes like String, DateTime, etc.
using System;
// List<int>, Dictionary<string, int>, etc.
using System.Collections.Generic;
// LINQ methods like .Where(), .Select(), Any(), FirstOrDefault(), etc.
using System.Linq;
// Allows use of asynchronous programming: Task, async, await, etc.
using System.Threading.Tasks;
// ASP.NET Core MVC attributes and classes: Controller, IActionResult, View(), RedirectToAction(), etc.
using Microsoft.AspNetCore.Mvc;
// Used for dropdowns like new SelectList(), etc.
using Microsoft.AspNetCore.Mvc.Rendering;
// Entity Framework Core methods: ToListAsync(), FirstOrDefaultAsync(), FindAsync(), etc.
using Microsoft.EntityFrameworkCore;
// Application-specific namespaces for data context and models
using FirstDotNETApp.Data;
// Application-specific namespace for the Country model
using FirstDotNETApp.Models;
// Groups all controller classes together
namespace FirstDotNETApp.Controllers
{
    // Creates controller named "CountriesController" that inherits from the base "Controller" class
    public class CountriesController : Controller
    {
        // Private variable, type: AppDbContext, name: _context
        private readonly AppDbContext _context;

        // Dependency Injection
        // Constructor runs when controller object is created
        public CountriesController(AppDbContext context)
        {
            // Stores the injected database objec into _context
            _context = context;
        }

        // GET: Countries
        // Action method. URL: /Countries. Returns: IActionResult: A response sent back to the browser
        // Async because database operation is asynchronous. Allows use of await
        public async Task<IActionResult> Index()
        {
            // _context.Countries: Refers to Country table
            // ToListAsync(): Fetches all rows
            // await: waits for database response
            // View: Sends data to Views/Countries/Index.cshtml 
            return View(await _context.Countries.ToListAsync());
        }

        // GET: Countries/Details/5
        // int? id: Integer is nullable, because id might not be provided in URL
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // SELECT TOP 1* FROM Countries WHERE CountryId = id
            // CountryId is primary key, source: AppDbContext.cs
            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryId == id);

            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        // Shows empty form
        // URL: /Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // [HttpPost]: Only handles POST requests
        // [ValidateAntiForgeryToken]: Security feature against CSRF attacks. Validates hidden token generated in form.
        [HttpPost]
        [ValidateAntiForgeryToken]

        // Create: Method name. Matches form action in Create.cshtml
        // Country country: Creates country object {Country country = new Country();}
        // Bind: Model binding restriction. Fill only "CountryId,CountryName" properties. Protects against overposting
        public async Task<IActionResult> Create([Bind("CountryId,CountryName")] Country country)
        {
            // Checks validation rules. Eg: [Required], [StringLength], etc.
            if (ModelState.IsValid)
            {
                // Marks entity for insertion
                // Executes: INSERT INTO Countries(...) VALUES(...)
                // Redirects to /Countries
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetches record by primary key
            // Select * FROM Countries WHERE CountryId = id
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]

        // int id: Comes from URL
        // Country country: Comes from form fields
        // 2 separate inputs. If id from URL doesn't match country.CountryId from form, return 404. Extra protection
        public async Task<IActionResult> Edit(int id, [Bind("CountryId,CountryName")] Country country)
        {
            if (id != country.CountryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // UPDATE Countries SET CountryName = ... WHERE CountryId = ...
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }

                // DbUpdateConcurrencyException: Thrown when 2 users try to edit the same record at the same time. Last save wins. The other user gets this exception
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        // [HttpPost, ActionName("Delete")]: Actual delete operation. Method: DeleteConfirmed. URL: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }

            // DELETE FROM Countries WHERE CountryId = 5
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method
        // SELECT CASE
        // WHEN EXISTS
        // ( SELECT * FROM Countries WHERE CountryId = id )
        // THEN 1
        // ELSE 0
        // END
        // Useful when 2 or more users are editing the same record at the same time
        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryId == id);
        }
    }
}
