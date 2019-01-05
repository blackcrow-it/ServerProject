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
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;

    using ServerProject.Security;

    public class AccountsController : Controller
    {
        private readonly ServerProjectContext _context;

        public AccountsController(ServerProjectContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Accounts.Include(m=>m.Informations).Include(s=>s.Students).Where(t=>t.Role != 1).ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Marks> Mk = _context.Marks.Include(m=>m.Courses).ToList();
            ViewBag.Mk = Mk;
            List<StudentGrade> stusList = _context.StudentGrade.Include(g=>g.Grades).ToList();
            ViewBag.Stus = stusList;
            var accounts = await _context.Accounts.Include(m=>m.Informations).Include(s=>s.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,ConfirmPassword,Informations,Students,Role")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                var exisEmail = this._context.Accounts.SingleOrDefault(a => a.UserName == accounts.UserName);
                var exisEmail2 = this._context.Informations.SingleOrDefault(a => a.Email == accounts.Informations.Email);
                if (exisEmail2 != null)
                {
                    TempData["fail2"] = "Email đã được sử dụng";
                    return RedirectToAction(nameof(Create));
                }
                if (exisEmail != null)
                {
                    TempData["fail"] = "Username đã được sử dụng";
                    return RedirectToAction(nameof(Create));
                }
                var students = new Students();
                accounts.Salt = Handlepassword.GetInstance().GenerateSalt();
                accounts.Password = Handlepassword.GetInstance()
                .EncryptPassword(accounts.Password, accounts.Salt);
                _context.Add(accounts);
                await _context.SaveChangesAsync();
                int Idacount = accounts.Id;
                students.AccountId = Idacount;
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

            return View(accounts);
        }

        //// GET: Accounts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var accounts = await _context.Accounts.FindAsync(id);
        //    if (accounts == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(accounts);
        //}

        //// POST: Accounts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Role,CreatedAt,UpdatedAt")] Accounts accounts)
        //{
        //    if (id != accounts.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(accounts);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AccountsExists(accounts.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(accounts);
        //}

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accounts = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(accounts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountsExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMark(long id, [Bind("Id,Value,CourseId,RollNumber")] Marks marks)
        {
            if (id != marks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               marks.CalculateMarkStatus();

                _context.Update(marks);
                    await _context.SaveChangesAsync();
              return RedirectToAction(nameof(Index));
            }
            
            return View(marks);
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
                var exisEmail2 = this._context.Informations.SingleOrDefault(a => a.Email == informations.Email);
                if (exisEmail2 != null)
                {
                    TempData["fail2"] = "Email đã được sử dụng";
                    return RedirectToAction(nameof(EditInfor));
                }
                try
                {
                    _context.Update(informations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                  
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", informations.AccountId);
            return View(informations);
        }
    }
}
