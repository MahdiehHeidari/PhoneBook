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
        public IActionResult Create(int? id)
        {

            //ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id");
            if(id != null)
            {
                ViewData["PersonId"] = id;
            }
            
           // ViewData["PersonId"] = phone.PersonId;
            return View();
        }

        // POST: Phone/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,Type,PersonId")] Phone phone)
        {
            if (ModelState.IsValid || true)
            {
                if(phone.PhoneNumber == null)
                {
                    return RedirectToAction(nameof(PhonesWithPersonId), new { id = phone.PersonId });

                }
                _context.Add(phone);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(PhonesWithPersonId), new { id = phone.PersonId });

            }
            ViewData["PersonId"] = phone.PersonId;
            // ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", phone.PersonId);
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
            ViewData["PersonId"] = phone.PersonId;
            // ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", phone.PersonId);
            return View(phone);
        }

        // POST: Phone/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PhoneNumber,Type,PersonId")] Phone phone)
        {
            if (phone.PersonId != null)
            {
                var person = _context.Persons.Find(phone.PersonId);
                var phones = person.Phones;
            }
            if (id == null || _context.Phones == null)
            {
                return NotFound();
            }
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
                return RedirectToAction(nameof(PhonesWithPersonId), new { id = phone.PersonId });
                // return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = id;
            // ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", phone.PersonId);
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
            var x =DeleteConfirmed((int)id);
            // return View(phone);
            return RedirectToAction(nameof(PhonesWithPersonId), new { id = phone.PersonId });

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
            return RedirectToAction(nameof(PhonesWithPersonId), new { id = phone.PersonId });

          //  return RedirectToAction(nameof(Index));
        }

        private bool PhoneExists(int id)
        {
          return (_context.Phones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // POST: Phone/PhonesWithPersonId
        [HttpGet]
        [ActionName("PhonesWithPersonId")]
        public async Task<IActionResult> PhonesWithPersonId(int? id)
        {
            
                var phoneBookContext = _context.Phones.Include(p => p.Person);
            
            if (id == null)
            {
                return NotFound();
            }
            var person = _context.Persons.Find(id); // Replace 'personId' with the actual ID
            if (person == null)
            {
                return NotFound();
            }
            // Next, explicitly load the Phones navigation property
            _context.Entry(person).Collection(p => p.Phones).Load();

            var phones = person.Phones;


            if( phones.Count == 0)
            {
                return RedirectToAction(nameof(Create), new { id = id });

            }
            if (ModelState.IsValid || true)
            {
                ViewData["PersonId"] = id;

                return View(phones);
            }
            
            return View(phones);
        
    }

    }
}
