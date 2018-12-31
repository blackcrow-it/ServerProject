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
    public class GradeCoursesController : Controller
    {
        private readonly ServerProjectContext _context;

        public GradeCoursesController(ServerProjectContext context)
        {
            _context = context;
        }

        // GET: GradeCourses
        public async Task<IActionResult> Index()
        {
            var serverProjectContext = _context.GradeCourse.Include(g => g.Courses).Include(g => g.Grades);
            return View(await serverProjectContext.ToListAsync());
        }

        // GET: GradeCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeCourse = await _context.GradeCourse
                .Include(g => g.Courses)
                .Include(g => g.Grades)
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (gradeCourse == null)
            {
                return NotFound();
            }

            return View(gradeCourse);
        }

        // GET: GradeCourses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Id");
            return View();
        }

        // POST: GradeCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeId,CourseId,StartTime,EndTime,IsActive,CreatedAt,UpdatedAt")] GradeCourse gradeCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gradeCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", gradeCourse.CourseId);
            ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Id", gradeCourse.GradeId);
            return View(gradeCourse);
        }

        // GET: GradeCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeCourse = await _context.GradeCourse.FindAsync(id);
            if (gradeCourse == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", gradeCourse.CourseId);
            ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Id", gradeCourse.GradeId);
            return View(gradeCourse);
        }

        // POST: GradeCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeId,CourseId,StartTime,EndTime,IsActive,CreatedAt,UpdatedAt")] GradeCourse gradeCourse)
        {
            if (id != gradeCourse.GradeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gradeCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeCourseExists(gradeCourse.GradeId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", gradeCourse.CourseId);
            ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Id", gradeCourse.GradeId);
            return View(gradeCourse);
        }

        // GET: GradeCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeCourse = await _context.GradeCourse
                .Include(g => g.Courses)
                .Include(g => g.Grades)
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (gradeCourse == null)
            {
                return NotFound();
            }

            return View(gradeCourse);
        }

        // POST: GradeCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gradeCourse = await _context.GradeCourse.FindAsync(id);
            _context.GradeCourse.Remove(gradeCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeCourseExists(int id)
        {
            return _context.GradeCourse.Any(e => e.GradeId == id);
        }
    }
}
