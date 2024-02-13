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
    public class personController : Controller
    {
        private readonly PhoneBookContext _context;

        public personController(PhoneBookContext context)
        {
            _context = context;
        }

        // GET: person
        public async Task<IActionResult> Index()
        {

            var personWithPhones = _context.Persons
    .Include(p => p.Phones).ToList();

            return _context.Persons
    .Include(p => p.Phones).ToList() != null ?
                          View(await _context.Persons.ToListAsync()) :
                          Problem("Entity set 'PhoneBookContext.Persons'  is null."); ;
        }

        // GET: person/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: person/Create
        public IActionResult Create()
        {
          
            var persons = new Person();
            persons.Phones = new List<Phone>();

            return View(persons);
          
        }
        public ActionResult AddChild()
        {
            return PartialView("ChildRow", new Phone());
        }





        [HttpPost]
        public ActionResult Create(Person viewModel)
        {
            var per = new Person();
            if (ModelState.IsValid || true)
            {
               
              
                _context.Persons.Add(viewModel);
                _context.SaveChanges();
               
            }

            return View(viewModel);
        }
        // POST: person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Created([Bind("Id,FirstName,LastName,Phones")] Person person)
        {

            var proj = new Person();
           
           
            if (ModelState.IsValid || true)
            {
             //   person.Phones = person.Phones.Select(phone => new Phone { PhoneNumber = phone.PhoneNumber }).ToList();

                
                if (ModelState.IsValid || true)
                {
                    proj.LastName = person.LastName;

                    proj.FirstName = person.FirstName;

                    proj.Phones = new List<Phone>();

                    foreach (var t in proj.Phones)
                    {
                        proj.Phones.Add(new Phone()
                        {
                            PhoneNumber = t.PhoneNumber,
                            Type = t.Type
                        });
                    }

                  
                }
                _context.Add(proj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }
          
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }
           person =_context.Persons
          .Include(p => p.Phones).First(t => t.Id == person.Id);
            return View(person);
        }

        // POST: person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Phones")] Person person)
        {
          
            if (ModelState.IsValid || true)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Persons == null)
            {
                return Problem("Entity set 'PhoneBookContext.Persons'  is null.");
            }
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
          return (_context.Persons?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpPost]
        public void SaveData(Person model)
        {
            
         
            var x = 3;
            var y = 2;
            // Access form data via model.JobId
            // Process the data as needed
            // Return appropriate result (e.g., RedirectToAction or JsonResult)
        }
    }
}
