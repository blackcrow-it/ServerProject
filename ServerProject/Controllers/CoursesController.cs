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

    using ReflectionIT.Mvc.Paging;

    public class CoursesController : Controller
    {
        private readonly ServerProjectContext _context;

        public CoursesController(ServerProjectContext context)
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
        // GET: Courses
        public async Task<IActionResult> Index(int page = 1)
        {
            if (this.checkSession())
            {
                Response.StatusCode = 403;
               
                return Redirect("/Home/Login");
            }
            var query = _context.Courses.AsNoTracking().AsQueryable().OrderBy(s => s.Id);
            var model = await PagingList.CreateAsync(query, 5, page);
            return View(model);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
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

            var courses = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courses == null)
            {
                return NotFound();
            }
           
            return View(courses);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            if (this.checkSession())
            {
                Response.StatusCode = 403;
               
                return Redirect("/Home/Login");
            }
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Avarta")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courses);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            var courses = await _context.Courses.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Avarta")] Courses courses)
        {
            if (id != courses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursesExists(courses.Id))
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
            return View(courses);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            var courses = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courses = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(courses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursesExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
