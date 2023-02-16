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
    public class OrderFlowersController : Controller
    {
        private readonly UhanieDbContext _context;

        public OrderFlowersController(UhanieDbContext context)
        {
            _context = context;
        }

        // GET: OrderFlowers
        public async Task<IActionResult> Index()
        {
            var uhanieDbContext = _context.OrderFlowers.Include(o => o.Customer).Include(o => o.Flower);
            return View(await uhanieDbContext.ToListAsync());
        }

        // GET: OrderFlowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderFlowers == null)
            {
                return NotFound();
            }

            var orderFlower = await _context.OrderFlowers
                .Include(o => o.Customer)
                .Include(o => o.Flower)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderFlower == null)
            {
                return NotFound();
            }

            return View(orderFlower);
        }

        // GET: OrderFlowers/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["FlowerId"] = new SelectList(_context.Flowers, "Id", "Id");
            return View();
        }

        // POST: OrderFlowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FlowerId,CustomerId,Quantity,RegisterOn")] OrderFlower orderFlower)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderFlower);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id", orderFlower.CustomerId);
            ViewData["FlowerId"] = new SelectList(_context.Flowers, "Id", "Id", orderFlower.FlowerId);
            return View(orderFlower);
        }

        // GET: OrderFlowers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderFlowers == null)
            {
                return NotFound();
            }

            var orderFlower = await _context.OrderFlowers.FindAsync(id);
            if (orderFlower == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id", orderFlower.CustomerId);
            ViewData["FlowerId"] = new SelectList(_context.Flowers, "Id", "Id", orderFlower.FlowerId);
            return View(orderFlower);
        }

        // POST: OrderFlowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlowerId,CustomerId,Quantity,RegisterOn")] OrderFlower orderFlower)
        {
            if (id != orderFlower.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderFlower);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderFlowerExists(orderFlower.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id", orderFlower.CustomerId);
            ViewData["FlowerId"] = new SelectList(_context.Flowers, "Id", "Id", orderFlower.FlowerId);
            return View(orderFlower);
        }

        // GET: OrderFlowers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderFlowers == null)
            {
                return NotFound();
            }

            var orderFlower = await _context.OrderFlowers
                .Include(o => o.Customer)
                .Include(o => o.Flower)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderFlower == null)
            {
                return NotFound();
            }

            return View(orderFlower);
        }

        // POST: OrderFlowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderFlowers == null)
            {
                return Problem("Entity set 'UhanieDbContext.OrderFlowers'  is null.");
            }
            var orderFlower = await _context.OrderFlowers.FindAsync(id);
            if (orderFlower != null)
            {
                _context.OrderFlowers.Remove(orderFlower);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderFlowerExists(int id)
        {
          return _context.OrderFlowers.Any(e => e.Id == id);
        }
    }
}
