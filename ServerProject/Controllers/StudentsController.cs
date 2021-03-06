﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServerProject.Models;
using ServerProject.Services;

namespace ServerProject.Controllers
{
    using Microsoft.AspNetCore.Http;

    public class StudentsController : Controller
    {
        private readonly ServerProjectContext _context;

        public StudentsController(ServerProjectContext context)
        {
            _context = context;
        }
        public bool checkSession()
        {
            var ck = false;
            string currentLogin = HttpContext.Session.GetString("currentLogin");

            if (currentLogin == null)
            {
                ck = true;
            }

            return (ck);
        }
        // GET: Students
        public async Task<IActionResult> Index()
        {
            if (this.checkSession())
            {
                Response.StatusCode = 403;
               
                return Redirect("/Home/Login");
            }
            var serverProjectContext = _context.Students.Include(s => s.Accounts);
            return View(await serverProjectContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (this.checkSession())
            {
                Response.StatusCode = 403;
               
                return Redirect("/Home/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.Accounts)
                .FirstOrDefaultAsync(m => m.RollNumber == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            if (this.checkSession())
            {
                Response.StatusCode = 403;
                
                return Redirect("/Home/Login");
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId")] Students students)
        {
            if (ModelState.IsValid)
            {
                var data = _context.RollNumberStudents.OrderByDescending(r => r.Alphabet).FirstOrDefault();
                if (data != null)
                {
                    if (data.Number < 1000)
                    {
                        data.Number++;
                        data.UpdatedAt = DateTime.Now;
                        students.RollNumber = data.Alphabet + data.Number.ToString().PadLeft(4, '0');
                        _context.Update(data);
                    }
                    else
                    {
                        var alphabet = data.Alphabet;
                        var roll = new RollNumberStudents
                        {
                            Alphabet = alphabet,
                            Number = 1
                        };
                        roll.Alphabet++;
                        students.RollNumber = roll.Alphabet + roll.Number.ToString().PadLeft(4, '0');
                        _context.Add(roll);
                    }
                }
                else
                {
                    var roll = new RollNumberStudents
                    {
                        Alphabet = 'A',
                        Number = 1
                    };
                    students.RollNumber = roll.Alphabet + roll.Number.ToString().PadLeft(4, '0');
                    _context.Add(roll);
                }
                _context.Add(students);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", students.AccountId);
            return View(students);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (this.checkSession())
            {
                Response.StatusCode = 403;
                
                return Redirect("/Home/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", students.AccountId);
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RollNumber,AccountId,CreatedAt,UpdatedAt")] Students students)
        {
            if (id != students.RollNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(students);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.RollNumber))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", students.AccountId);
            return View(students);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (this.checkSession())
            {
                Response.StatusCode = 403;
               
                return Redirect("/Home/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.Accounts)
                .FirstOrDefaultAsync(m => m.RollNumber == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var students = await _context.Students.FindAsync(id);
            _context.Students.Remove(students);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(string id)
        {
            return _context.Students.Any(e => e.RollNumber == id);
        }
    }
}
