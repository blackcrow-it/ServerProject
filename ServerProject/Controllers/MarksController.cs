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
    using Microsoft.AspNetCore.Http;

    public class MarksController : Controller
    {
        private readonly ServerProjectContext _context;

        public MarksController(ServerProjectContext context)
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
        // GET: Marks
        public async Task<IActionResult> Index()
        {
            if (this.checkSession())
            {
                Response.StatusCode = 403;
                
                return Redirect("/Home/Login");
            }
            var serverProjectContext = _context.Marks.Include(m => m.Courses).Include(m => m.Students);
           
            return View(await serverProjectContext.ToListAsync());
        }

        // GET: Marks/Details/5
        public async Task<IActionResult> Details(long? id)
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
            if (this.checkSession())
            {
                Response.StatusCode = 403;
                
                return Redirect("/Home/Login");
            }

        
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["RollNumber"] = new SelectList(_context.Students, "RollNumber", "RollNumber");
            return View();
        }

        // POST: Marks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Value,RollNumber,CourseId")] Marks marks)
        {
            var checkmark = _context.Marks.Where(a => a.RollNumber == marks.RollNumber).Where(s => s.Type == marks.Type)
                .Where(d => d.CourseId == marks.CourseId).FirstOrDefault();
            if (checkmark != null)
            {
                TempData["fail"] = "Học sinh đã có điểm này";
                return RedirectToAction(nameof(Create));
            }
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
            if (this.checkSession())
            {
                Response.StatusCode = 403;
               
                return Redirect("/Home/Login");
            }
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
        public async Task<IActionResult> Edit(long id, [Bind("Id,Value,CourseId")] Marks marks)
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
            if (this.checkSession())
            {
                Response.StatusCode = 403;
                
                return Redirect("/Home/Login");
            }
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
