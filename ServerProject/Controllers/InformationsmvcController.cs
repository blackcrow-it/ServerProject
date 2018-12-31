using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServerProject.Models;

namespace ServerProject.Controllers
{
    public class InformationsmvcController : Controller
    {
        private readonly ServerProjectContext _context;

        public InformationsmvcController(ServerProjectContext context)
        {
            _context = context;
        }

        // GET: Informationsmvc
        public async Task<IActionResult> Index()
        {
            var serverProjectContext = _context.Informations.Include(i => i.Accounts);
            return View(await serverProjectContext.ToListAsync());
        }

        // GET: Informationsmvc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informations = await _context.Informations
                .Include(i => i.Accounts)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (informations == null)
            {
                return NotFound();
            }

            return View(informations);
        }

        // GET: Informationsmvc/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id");
            return View();
        }

        // POST: Informationsmvc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,FirstName,MiddleName,LastName,Gender,Birthday,Email,Address,Phone,Avatar,CreatedAt,UpdatedAt")] Informations informations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", informations.AccountId);
            return View(informations);
        }

        // GET: Informationsmvc/Edit/5
        public async Task<IActionResult> EditInfor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informations = await _context.Informations.FindAsync(id);
            if (informations == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", informations.AccountId);
            return View(informations);
        }

        // POST: Informationsmvc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInfor(int id, [Bind("AccountId,FirstName,MiddleName,LastName,Gender,Birthday,Email,Address,Phone,Avatar")] Informations informations)
        {
            if (id != informations.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationsExists(informations.AccountId))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", informations.AccountId);
            return View(informations);
        }

        // GET: Informationsmvc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informations = await _context.Informations
                .Include(i => i.Accounts)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (informations == null)
            {
                return NotFound();
            }

            return View(informations);
        }

        // POST: Informationsmvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var informations = await _context.Informations.FindAsync(id);
            _context.Informations.Remove(informations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformationsExists(int id)
        {
            return _context.Informations.Any(e => e.AccountId == id);
        }
    }
}
