﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServerProject.Models;

namespace ServerProject.Controllers
{
    public class MarksController : Controller
    {
        private readonly ServerProjectContext _context;

        public MarksController(ServerProjectContext context)
        {
            _context = context;
        }

        // GET: Marks
        public async Task<IActionResult> Index()
        {
            var serverProjectContext = _context.Marks.Include(m => m.Courses).Include(m => m.Students);
            return View(await serverProjectContext.ToListAsync());
        }

        // GET: Marks/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marks = await _context.Marks
                .Include(m => m.Courses)
                .Include(m => m.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marks == null)
            {
                return NotFound();
            }

            return View(marks);
        }

        // GET: Marks/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["RollNumber"] = new SelectList(_context.Students, "RollNumber", "RollNumber");
            return View();
        }

        // POST: Marks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Value,RollNumber,TypeMark,CourseId")] Marks marks)
        {
            if (ModelState.IsValid)
            {
                if (marks.Value >5)
                {
                    marks.Status = MarkStatus.PASS;
                }
                else
                {
                    marks.Status = MarkStatus.FAIL;
                }
                    _context.Add(marks);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", marks.CourseId);
            ViewData["RollNumber"] = new SelectList(_context.Students, "RollNumber", "RollNumber", marks.RollNumber);
            return View(marks);
        }
        // GET: Marks/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marks = await _context.Marks.FindAsync(id);
            if (marks == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", marks.CourseId);
            ViewData["RollNumber"] = new SelectList(_context.Students, "RollNumber", "RollNumber", marks.RollNumber);
            return View(marks);
        }

        // POST: Marks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Type,Value,RollNumber,CourseId")] Marks marks)
        {
            if (id != marks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarksExists(marks.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", marks.CourseId);
            ViewData["RollNumber"] = new SelectList(_context.Students, "RollNumber", "RollNumber", marks.RollNumber);
            return View(marks);
        }

        // GET: Marks/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marks = await _context.Marks
                .Include(m => m.Courses)
                .Include(m => m.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marks == null)
            {
                return NotFound();
            }

            return View(marks);
        }

        // POST: Marks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var marks = await _context.Marks.FindAsync(id);
            _context.Marks.Remove(marks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarksExists(long id)
        {
            return _context.Marks.Any(e => e.Id == id);
        }
    }
}