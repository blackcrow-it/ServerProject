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
    public class GradesController : Controller
    {
        private readonly ServerProjectContext _context;

        public GradesController(ServerProjectContext context)
        {
            _context = context;
        }

        // GET: Grades
        public async Task<IActionResult> Index()
        {
            return View(await _context.Grades.ToListAsync());
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int? id ,long changeId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grades = await _context.Grades.Include(k=>k.StudentGrades).ThenInclude(h=>h.Students).FirstOrDefaultAsync(m => m.Id == id);

            List<GradeCourse> fundList = _context.GradeCourse.Where(i => i.GradeId == id).Include(m=>m.Courses).ToList();
            ViewBag.Funds = fundList;

            List<StudentGrade> stusList = _context.StudentGrade.Where(s=>s.GradeId == id).Include(st=>st.Students).ThenInclude(a=>a.Accounts).ToList();
            ViewBag.Stus = stusList;
            
            List<Marks> Mk = _context.Marks.Where(m=>m.CourseId == changeId).Include(c=>c.Courses).ToList();
            ViewBag.Mk = Mk;

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
           

            if (grades == null)
            {
                return NotFound();
            }

            return View(grades);
        }
        [HttpGet]
        public async Task<IActionResult> StudentDetail(int? id)
        {
            if (id == null)
            {
                return Json("123");
            }

            var informations = await _context.Informations
                                   .Include(i => i.Accounts)
                                   .FirstOrDefaultAsync(m => m.AccountId == id);
            if (informations == null)
            {
                return NotFound();
            }

            return Json(informations);
        }

        // GET: Grades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartTime,EndTime,IsActive,CreatedAt,UpdatedAt")] Grades grades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grades);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grades);
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grades = await _context.Grades.FindAsync(id);
            if (grades == null)
            {
                return NotFound();
            }
            return View(grades);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartTime,EndTime,IsActive,CreatedAt,UpdatedAt")] Grades grades)
        {
            if (id != grades.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grades);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradesExists(grades.Id))
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
            return View(grades);
        }

        // GET: Grades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grades = await _context.Grades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grades == null)
            {
                return NotFound();
            }

            return View(grades);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grades = await _context.Grades.FindAsync(id);
            _context.Grades.Remove(grades);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradesExists(int id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
        public IActionResult AddStudent(int? id)
        {
            List<Students> fundList = _context.Students.Include(m => m.Accounts).ThenInclude(i => i.Informations).ToList();
            ViewBag.Funds = fundList;
           
            ViewData["ID"] = id;
            return View();
        }

        // POST: StudentGrades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent([Bind("RollNumber,GradeId")] StudentGrade studentGrade)
        {
            var st = _context.StudentGrade.Where(r => r.RollNumber == studentGrade.RollNumber)
                .Where(s => s.GradeId == studentGrade.GradeId).FirstOrDefault();
            if (st != null)
            {
                TempData["fail"] = "Lớp đã có học sinh này !!!";
                return RedirectToAction(nameof(AddStudent));
            }
            if (ModelState.IsValid)
            {
                _context.Add(studentGrade);
                await _context.SaveChangesAsync();
                TempData["succ"] = "Thành công";
                return RedirectToAction(nameof(AddStudent));
            }
            ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Id", studentGrade.GradeId);
            ViewData["RollNumber"] = new SelectList(_context.Students, "RollNumber", "RollNumber", studentGrade.RollNumber);
            return View(studentGrade);
        }
        public IActionResult CreateGC(int? id)
        {
            ViewData["ID"] = id;
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Name");
            return View();
        }

        // POST: GradeCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGC([Bind("GradeId,CourseId,StartTime,EndTime")] GradeCourse gradeCourse)
        {
            var Cour = _context.GradeCourse.Where(s => s.CourseId == gradeCourse.CourseId).Where(m=>m.GradeId == gradeCourse.GradeId).FirstOrDefault();
            if (Cour != null)
            {
                TempData["fail"] = "Lớp đã có điểm này !!!";
                return RedirectToAction(nameof(CreateGC));
            }
            if (ModelState.IsValid)
            {
                _context.Add(gradeCourse);
                await _context.SaveChangesAsync();
                TempData["succ"] = "Thành công";
                return RedirectToAction(nameof(CreateGC));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", gradeCourse.CourseId);
            ViewData["GradeId"] = new SelectList(_context.Grades, "Id", "Id", gradeCourse.GradeId);
            return View(gradeCourse);
        }
        [HttpPost]
        public IActionResult DelSt(string RollNumber, int GradeId)
        {
            var st = _context.StudentGrade.Where(r => r.RollNumber == RollNumber)
                .Where(s => s.GradeId == GradeId).FirstOrDefault();

            _context.StudentGrade.Remove(st);
            _context.SaveChanges();
            return this.Ok(RollNumber);
        }
        [HttpPost]
        public IActionResult DelC(int CourseId, int GradeId)
        {
            var st = _context.GradeCourse.Where(r => r.CourseId == CourseId)
                .Where(s => s.GradeId == GradeId).FirstOrDefault();

            _context.GradeCourse.Remove(st);
            _context.SaveChanges();
            return this.Ok();
        }
        public async Task<IActionResult> DetailsInfor(int? id)
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
                    
                        return NotFound();
                    
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", informations.AccountId);
            return View(informations);
        }
      
        public async Task<IActionResult> CreateListMark([Bind("Id,Type,Value,RollNumber,TypeMark,CourseId")] Marks marks)
        {
            if (ModelState.IsValid)
            {
                if (marks.Value > 5)
                {
                    marks.Status = MarkStatus.PASS;
                }
                else
                {
                    marks.Status = MarkStatus.FAIL;
                }
                
                _context.Add(marks);
                await _context.SaveChangesAsync();
               
                return Json("hello");
            }
            
            return this.Ok();
        }
    }
}