using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MenuDishApp.Data;
using MenuDishApp.Models.MenuDish;

namespace MenuDishApp.Controllers
{
    public class MenuDishesController : Controller
    {
        private readonly MenuDishContext _context;

        public MenuDishesController(MenuDishContext context)
        {
            _context = context;
        }

        // GET: MenuDishes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MenuDish.ToListAsync());
        }

        // GET: MenuDishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuDish = await _context.MenuDish
                .FirstOrDefaultAsync(m => m.ID == id);
            if (menuDish == null)
            {
                return NotFound();
            }

            return View(menuDish);
        }

        // GET: MenuDishes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuDishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Number,Name,Description,Price,Family")] MenuDish menuDish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuDish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuDish);
        }

        // GET: MenuDishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuDish = await _context.MenuDish.FindAsync(id);
            if (menuDish == null)
            {
                return NotFound();
            }
            return View(menuDish);
        }

        // POST: MenuDishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Number,Name,Description,Price,Family")] MenuDish menuDish)
        {
            if (id != menuDish.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuDish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuDishExists(menuDish.ID))
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
            return View(menuDish);
        }

        // GET: MenuDishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuDish = await _context.MenuDish
                .FirstOrDefaultAsync(m => m.ID == id);
            if (menuDish == null)
            {
                return NotFound();
            }

            return View(menuDish);
        }

        // POST: MenuDishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuDish = await _context.MenuDish.FindAsync(id);
            _context.MenuDish.Remove(menuDish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuDishExists(int id)
        {
            return _context.MenuDish.Any(e => e.ID == id);
        }
    }
}
