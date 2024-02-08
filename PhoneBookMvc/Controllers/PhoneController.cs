using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhoneBookMvc.Models;

namespace PhoneBookMvc
{
    public class PhoneController : Controller
    {
        private readonly PhoneBookContext _context;

        public PhoneController(PhoneBookContext context)
        {
            _context = context;
        }

        // GET: Phone
        public async Task<IActionResult> Index()
        {
            var phoneBookContext = _context.Phones.Include(p => p.Person);
            return View(await phoneBookContext.ToListAsync());
        }

        // GET: Phone/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Phones == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // GET: Phone/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id");
            return View();
        }

        // POST: Phone/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PhoneNumber,Type,PersonId")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", phone.PersonId);
            return View(phone);
        }

        // GET: Phone/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Phones == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", phone.PersonId);
            return View(phone);
        }

        // POST: Phone/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PhoneNumber,Type,PersonId")] Phone phone)
        {
            if (id != phone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid || true)
            {
                try
                {
                    _context.Update(phone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneExists(phone.Id))
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
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", phone.PersonId);
            return View(phone);
        }

        // GET: Phone/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Phones == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // POST: Phone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Phones == null)
            {
                return Problem("Entity set 'PhoneBookContext.Phones'  is null.");
            }
            var phone = await _context.Phones.FindAsync(id);
            if (phone != null)
            {
                _context.Phones.Remove(phone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneExists(int id)
        {
          return (_context.Phones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
