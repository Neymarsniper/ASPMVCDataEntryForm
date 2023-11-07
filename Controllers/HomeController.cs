using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemberDataEntryForm.Models;
using Microsoft.AspNetCore.Authorization;

namespace MemberDataEntryForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly MemberDataContext _context;

        public HomeController(MemberDataContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            return _context.MemberDirectoryData != null ? View(await _context.MemberDirectoryData.ToListAsync()) : Problem("Entity set 'MemberDirectoryContext.MemberDirectoryData'  is null.");
        }


        ////GET
        ////[Authorize(Roles = "Admin")]
        ////[Authorize(Policy = "UserAccess")]
        //public IActionResult Login()
        //{
        //    if (HttpContext.Session.GetString("UserSession") != null)
        //    {
        //        return RedirectToAction("Details");
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Login(UserData user)
        //{
        //    var myuser = _context.UserDirectoryData.Where(item => item.Email == user.Email && item.Password == user.Password).FirstOrDefault();
        //    //It checks if a user with matching credentials exists in the database.
        //    if (myuser != null)
        //    {
        //        HttpContext.Session.SetString("UserSession", myuser.Email);  // In this case, the user's email is stored in a session variable named "UserSession."
        //                                                                     // This session variable is used to keep track of whether a user is logged in. Storing the email is a common practice to identify the user in future requests.
        //        return RedirectToAction("Details", new { id = myuser.UserId });
        //    }
        //    else
        //    {
        //        ViewBag.message = "Login Failed...";
        //    }
        //    return View();
        //}

        //public IActionResult Logout()
        //{
        //    if (HttpContext.Session.GetString("UserSession").ToString() != null)
        //    {
        //        HttpContext.Session.Remove("UserSession");
        //        return RedirectToAction("Login");
        //    }
        //    return View();
        //}


        // GET: Home/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //if (_context.MemberDirectoryData == null/* && HttpContext.Session.GetString("UserSession") == null*/)
            //{
            //    return RedirectToAction("Login");
            //}

            var memberDirectoryDatum = await _context.MemberDirectoryData.FirstOrDefaultAsync(m => m.Id == id);
            if (memberDirectoryDatum == null)
            {
                return NotFound();
            }

            return View(memberDirectoryDatum);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MemNo,Dob,ResAddress,ResPhone,OfficeNo,Profession,OfficeAddress,MobileNo,AlternateMobileNo,Email,DateofMarriage,NameofSpouse,SpouseDob,ChildName")] MemberData memberDirectoryDatum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberDirectoryDatum);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create","MemberBusiness");
            }
            return View(memberDirectoryDatum);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0  || _context.MemberDirectoryData == null)
            {
                return NotFound();
            }

            var memberDirectoryDatum = await _context.MemberDirectoryData.FindAsync(id);
            if (memberDirectoryDatum == null)
            {
                return NotFound();
            }
            return View(memberDirectoryDatum);
        }

        // POST: Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MemNo,Dob,ResAddress,ResPhone,OfficeNo,Profession,OfficeAddress,MobileNo,AlternateMobileNo,Email,DateofMarriage,NameofSpouse,SpouseDob,ChildName")] MemberData memberDirectoryDatum)
        {
            if (id != memberDirectoryDatum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberDirectoryDatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberDirectoryDatumExists(memberDirectoryDatum.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new { id = memberDirectoryDatum.Id });
            }
            return View(memberDirectoryDatum);
        }




        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0 || _context.MemberDirectoryData == null)
            {
                return NotFound();
            }

            var memberDirectoryDatum = await _context.MemberDirectoryData.FirstOrDefaultAsync(m => m.Id == id);
            if (memberDirectoryDatum == null)
            {
                return NotFound();
            }

            return View(memberDirectoryDatum);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberDirectoryData == null)
            {
                return Problem("Entity set 'MemberDirectoryContext.MemberDirectoryData'  is null.");
            }
            var memberDirectoryDatum = await _context.MemberDirectoryData.FirstOrDefaultAsync(m => m.Id == id);
            if (memberDirectoryDatum != null)
            {
                _context.MemberDirectoryData.Remove(memberDirectoryDatum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberDirectoryDatumExists(int id)
        {
            return (_context.MemberDirectoryData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
