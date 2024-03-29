﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UhanieWebApp.Data;

namespace UhanieWebApp.Controllers
{
    public class FlowersController : Controller
    {
        private readonly UhanieDbContext _context;

        public FlowersController(UhanieDbContext context)
        {
            _context = context;
        }

        // GET: Flowers
        public async Task<IActionResult> Index(string searchString)
        {
            var flowers = from s in _context.Flowers
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                flowers = flowers.Where(s => s.BulgarianName.Contains(searchString));
            }
            return View(flowers.ToList()); //return View(await _context.Flowers.ToListAsync());
        }

        // GET: Flowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Flowers == null)
            {
                return NotFound();
            }

            var flower = await _context.Flowers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flower == null)
            {
                return NotFound();
            }

            return View(flower);
        }

        // GET: Flowers/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BulgarianName,LatinName,Size,Description,ImageURL,Price")] Flower flower)
        {
            if (ModelState.IsValid)
            {
                flower.RegisterOn = DateTime.Now;
                _context.Flowers.Add(flower);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flower);
        }

        // GET: Flowers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Flowers == null)
            {
                return NotFound();
            }

            var flower = await _context.Flowers.FindAsync(id);
            if (flower == null)
            {
                return NotFound();
            }
            return View(flower);
        }

        // POST: Flowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BulgarianName,LatinName,Size,Description,ImageURL,Price")] Flower flower)
        {
            if (id != flower.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    flower.RegisterOn = DateTime.Now;
                    _context.Flowers.Update(flower);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlowerExists(flower.Id))
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
            return View(flower);
        }

        // GET: Flowers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Flowers == null)
            {
                return NotFound();
            }

            var flower = await _context.Flowers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flower == null)
            {
                return NotFound();
            }

            return View(flower);
        }

        // POST: Flowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Flowers == null)
            {
                return Problem("Entity set 'UhanieDbContext.Flowers'  is null.");
            }
            var flower = await _context.Flowers.FindAsync(id);
            if (flower != null)
            {
                _context.Flowers.Remove(flower);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlowerExists(int id)
        {
          return _context.Flowers.Any(e => e.Id == id);
        }
    }
}
