using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UhanieWebApp.Data;

namespace UhanieWebApp.Controllers
{
    public class OrderBouquetsController : Controller
    {
        private readonly UhanieDbContext _context;
        private readonly UserManager<Customer> _userManager;
        //private readonly SignInManager<User> _sigInManager;
        //private readonly RoleManager<IdentityRole> _roleManager;


        public OrderBouquetsController(UhanieDbContext context,
                                UserManager<Customer> userManager
                                /*RoleManager<IdentityRole> roleManager*/)
        {
            _context = context;
            _userManager = userManager;
            //_roleManager = roleManager
        }

        // GET: OrderBouquets
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var uhanieDbContext = _context.OrderBouquets.Include(o => o.Bouquets).Include(o => o.Customer);
                return View(await uhanieDbContext.ToListAsync());
            }
            else
            {
                var uhanieDbContext = _context.OrderBouquets.Include(o => o.Bouquets).Include(o => o.Customer)
                    .Where(o => o.CustomerId == _userManager.GetUserId(User));
                return View(await uhanieDbContext.ToListAsync());
            }
        }

        // GET: OrderBouquets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderBouquets == null)
            {
                return NotFound();
            }

            var orderBouquet = await _context.OrderBouquets
                .Include(o => o.Bouquets)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderBouquet == null)
            {
                return NotFound();
            }

            return View(orderBouquet);
        }

        // GET: OrderBouquets/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["BouquetId"] = new SelectList(_context.Bouquets, "Id", "Name");
            //ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: OrderBouquets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BouquetId,Quantity")] OrderBouquet orderBouquet)
        {
            if (ModelState.IsValid)
            {
                orderBouquet.RegisterOn = DateTime.Now;
                orderBouquet.CustomerId = _userManager.GetUserId(User);
                _context.OrderBouquets.Add(orderBouquet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BouquetId"] = new SelectList(_context.Bouquets, "Id", "Name", orderBouquet.BouquetId);
            //ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id", orderBouquet.CustomerId);
            return View(orderBouquet);
        }

        // GET: OrderBouquets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderBouquets == null)
            {
                return NotFound();
            }

            var orderBouquet = await _context.OrderBouquets.FindAsync(id);
            if (orderBouquet == null)
            {
                return NotFound();
            }
            ViewData["BouquetId"] = new SelectList(_context.Bouquets, "Id", "Name", orderBouquet.BouquetId);
            //ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id", orderBouquet.CustomerId);
            return View(orderBouquet);
        }

        // POST: OrderBouquets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BouquetId,Quantity")] OrderBouquet orderBouquet)
        {
            if (id != orderBouquet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    orderBouquet.RegisterOn = DateTime.Now;
                    orderBouquet.CustomerId = _userManager.GetUserId(User);
                    _context.OrderBouquets.Update(orderBouquet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderBouquetExists(orderBouquet.Id))
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
            ViewData["BouquetId"] = new SelectList(_context.Bouquets, "Id", "Name", orderBouquet.BouquetId);
            //ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id", orderBouquet.CustomerId);
            return View(orderBouquet);
        }

        // GET: OrderBouquets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderBouquets == null)
            {
                return NotFound();
            }

            var orderBouquet = await _context.OrderBouquets
                .Include(o => o.Bouquets)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderBouquet == null)
            {
                return NotFound();
            }

            return View(orderBouquet);
        }

        // POST: OrderBouquets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderBouquets == null)
            {
                return Problem("Entity set 'UhanieDbContext.OrderBouquets'  is null.");
            }
            var orderBouquet = await _context.OrderBouquets.FindAsync(id);
            if (orderBouquet != null)
            {
                _context.OrderBouquets.Remove(orderBouquet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderBouquetExists(int id)
        {
          return _context.OrderBouquets.Any(e => e.Id == id);
        }
    }
}
