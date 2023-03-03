using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UhanieWebApp.Data;

namespace UhanieWebApp.Controllers
{
    public class BouquetsController : Controller
    {
        private readonly UhanieDbContext _context;

        public BouquetsController(UhanieDbContext context)
        {
            _context = context;
        }

        // GET: Bouquets
        public async Task<IActionResult> Index()
        {
              return View(await _context.Bouquets.ToListAsync());
        }

        // GET: Bouquets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bouquets == null)
            {
                return NotFound();
            }

            var bouquet = await _context.Bouquets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bouquet == null)
            {
                return NotFound();
            }

            return View(bouquet);
        }

        // GET: Bouquets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bouquets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Type,Description,ImageURL,Price,RegisterOn")] Bouquet bouquet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bouquet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bouquet);
        }

        // GET: Bouquets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bouquets == null)
            {
                return NotFound();
            }

            var bouquet = await _context.Bouquets.FindAsync(id);
            if (bouquet == null)
            {
                return NotFound();
            }
            return View(bouquet);
        }

        // POST: Bouquets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Description,ImageURL,Price,RegisterOn")] Bouquet bouquet)
        {
            if (id != bouquet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Bouquets.Update(bouquet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BouquetExists(bouquet.Id))
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
            return View(bouquet);
        }

        // GET: Bouquets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bouquets == null)
            {
                return NotFound();
            }

            var bouquet = await _context.Bouquets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bouquet == null)
            {
                return NotFound();
            }

            return View(bouquet);
        }

        // POST: Bouquets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bouquets == null)
            {
                return Problem("Entity set 'UhanieDbContext.Bouquets'  is null.");
            }
            var bouquet = await _context.Bouquets.FindAsync(id);
            if (bouquet != null)
            {
                _context.Bouquets.Remove(bouquet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BouquetExists(int id)
        {
          return _context.Bouquets.Any(e => e.Id == id);
        }
    }
}
