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
    public class StudentGradesController : Controller
    {
        private readonly ServerProjectContext _context;

        public StudentGradesController(ServerProjectContext context)
        {
            _context = context;
        }

        // GET: StudentGrades
        public async Task<IActionResult> Index()
        {
            var serverProjectContext = _context.StudentGrade.Include(s => s.Grades).Include(s => s.Students);
            return View(await serverProjectContext.ToListAsync());
        }

        // GET: StudentGrades/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.StudentGrade
                .Include(s => s.Grades)
                .Include(s => s.Students)
                .FirstOrDefaultAsync(m => m.RollNumber == id);
            if (studentGrade == null)
            {
                return NotFound();
            }

            return View(studentGrade);
        }

        // GET: StudentGrades/Create
        public IActionResult Create()
        {
            ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Name");
            ViewData["RollNumber"] = new SelectList(_context.Students, "RollNumber", "RollNumber");
            return View();
        }

        // POST: StudentGrades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RollNumber,GradeId,JoinAt,LeftAt,IsActive,CreatedAt,UpdatedAt")] StudentGrade studentGrade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentGrade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Name", studentGrade.GradeId);
            ViewData["RollNumber"] = new SelectList(_context.Students, "RollNumber", "RollNumber", studentGrade.RollNumber);
            return View(studentGrade);
        }

        // GET: StudentGrades/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.StudentGrade.FindAsync(id);
            if (studentGrade == null)
            {
                return NotFound();
            }
            ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Name", studentGrade.GradeId);
            ViewData["RollNumber"] = new SelectList(_context.Students, "RollNumber", "RollNumber", studentGrade.RollNumber);
            return View(studentGrade);
        }

        // POST: StudentGrades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RollNumber,GradeId,JoinAt,LeftAt,IsActive,CreatedAt,UpdatedAt")] StudentGrade studentGrade)
        {
            if (id != studentGrade.RollNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentGrade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentGradeExists(studentGrade.RollNumber))
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
            ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Name", studentGrade.GradeId);
            ViewData["RollNumber"] = new SelectList(_context.Students, "RollNumber", "RollNumber", studentGrade.RollNumber);
            return View(studentGrade);
        }

        // GET: StudentGrades/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentGrade = await _context.StudentGrade
                .Include(s => s.Grades)
                .Include(s => s.Students)
                .FirstOrDefaultAsync(m => m.RollNumber == id);
            if (studentGrade == null)
            {
                return NotFound();
            }

            return View(studentGrade);
        }

        // POST: StudentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var studentGrade = await _context.StudentGrade.FindAsync(id);
            _context.StudentGrade.Remove(studentGrade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentGradeExists(string id)
        {
            return _context.StudentGrade.Any(e => e.RollNumber == id);
        }
    }
}
