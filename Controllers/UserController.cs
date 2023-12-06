using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemberDataEntryForm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;

namespace MemberDataEntryForm.Controllers
{
    public class UserController : Controller
    {
        private readonly MemberDataContext _context;

        public UserController(MemberDataContext context)
        {
            _context = context;
        }

        // GET: User
        //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Index(int id)
        {
            var myuser = await _context.UserDirectoryData.Include(m => m.UserType).SingleOrDefaultAsync(u => u.UserId == id);


            if(myuser.UserRoleId == 1)
            {
                var AdminUserId = myuser.UserId;
                ViewBag.AdminUserId = AdminUserId;
                ViewBag.msg = "Success";
            }

            if (myuser.UserRoleId == 2)
            {
                var FrontDeskUserId = myuser.UserId;
                ViewBag.FrontDeskUserId = FrontDeskUserId;
                
            }

            if (myuser.UserRoleId == 1 || myuser.UserRoleId == 2)
            {
                ViewBag.message = "Access Granted!";
                return _context.UserDirectoryData != null ? View(await _context.UserDirectoryData.ToListAsync()) : Problem("Entity set 'MemberDataContext.UserDirectoryData'  is null.");
            }
            else
            {
                return RedirectToAction("Details", new { id = myuser.UserId });
            }
        }


        //GET
        //[Authorize(Policy = "UserAccess")]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserData user)
        {
            var myuser = _context.UserDirectoryData.Where(item => item.Email == user.Email && item.Password == user.Password).FirstOrDefault();
            //It checks if a user with matching credentials exists in the database.
            if (myuser != null)
            {
                HttpContext.Session.SetString("UserSession", myuser.Email);  // In this case, the user's email is stored in a session variable named "UserSession."
                                                                             // This session variable is used to keep track of whether a user is logged in. Storing the email is a common practice to identify the user in future requests.
                return RedirectToAction("Details", new { id = myuser.UserId });
            }
            else
            {
                ViewBag.message = "Login Failed...";
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession").ToString() != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login");
            }
            return View();
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int id, int DataStatusId)
        {
            //ViewBag.message = "Access Denied!!";
            if (id == null || _context.UserDirectoryData == null && HttpContext.Session.GetString("UserSession") == null)
            {
                return NotFound();
            }

            var AuthId = id;

            if (DataStatusId != 0)
            {
                AuthId = DataStatusId;
            }

            var userData = await _context.UserDirectoryData.Include(m => m.UserType).FirstOrDefaultAsync(m => m.UserId == AuthId);

            if (userData.UserRoleId == 1 || userData.UserRoleId == 2)
            {
                ViewBag.AdminAuth = "Success";
                return RedirectToAction("Index", new { id = AuthId });
            }
            else 
            {
                //return RedirectToAction("Details", new { id = AuthId });
                return View(userData);
            }

        }

        // GET: User/Create
        public IActionResult Create(int AdminUserId)
        {
            ViewBag.DataStatusId = AdminUserId;
            ViewData["userType"] = new SelectList(_context.GetUserTypes, "RoleId", "RoleName");
            //ViewData["statusCodes"] = new SelectList(_context.UserStatusDirectoryData, "StatusCode", "StatusMessage");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,Email,EmailConfirmed,Password,PasswordConfirmed,Address,City,MobileNo,UserRoleId,DataStatusId")] UserData userData)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(userData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = userData.DataStatusId });
            //}
            //return View(userData);
        }


        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int id, int FrontDeskUserId, int AdminUserId)
        {
            if (id == null || _context.UserDirectoryData == null)
            {
                return NotFound();
            }

            if (AdminUserId != 0)
            {
                ViewBag.DataStatusId = AdminUserId;
            }
            else
            {
                ViewBag.DataStatusId = FrontDeskUserId;
            }
            ViewData["userType"] = new SelectList(_context.GetUserTypes, "RoleId", "RoleName");

            var userData = await _context.UserDirectoryData.FindAsync(id);

            if (userData == null)
            {
                return NotFound();
            }
            return View(userData);
        }




        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FirstName,LastName,Email,EmailConfirmed,Password,PasswordConfirmed,Address,City,MobileNo,UserRoleId,DataStatusId")] UserData userData)
        {
            if (id != userData.UserId)
            {
                return NotFound();
            }
            var userdata = await _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == userData.DataStatusId);

            if (userdata.UserRoleId == 2)
            {
                var allrecords = _context.UserProposedDirectoryData.ToList();
                _context.UserProposedDirectoryData.RemoveRange(allrecords);

                UserProposedData userProposedData = new UserProposedData
                {
                    UserId = userData.UserId,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    Email = userData.Email,
                    EmailConfirmed = userData.EmailConfirmed,
                    Password = userData.Password,
                    PasswordConfirmed = userData.PasswordConfirmed,
                    Address = userData.Address,
                    City = userData.City,
                    MobileNo = userData.MobileNo,
                    UserRoleId = userData.UserRoleId,
                    DataStatusId = 1
                };

                try
                {
                    _context.Add(userProposedData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDataExists(userData.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = userData.DataStatusId });
            }
            if (userdata.UserRoleId == 1)
            {
                try
                {
                    _context.Update(userData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDataExists(userData.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            return RedirectToAction("Index", new { id = userData.DataStatusId });
            }

            return RedirectToAction("Logout");
        }





        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id, int AdminUserId)
        {
            if (id == null || _context.UserDirectoryData == null)
            {
                return NotFound();
            }

            //if (AdminUserId != 0)
            //{
            //    ViewBag.DataStatusId = AdminUserId;
            //}

            var userData = await _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == id);
            
            userData.DataStatusId = AdminUserId;

            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserDirectoryData == null)
            {
                return Problem("Entity set 'MemberDataContext.UserDirectoryData'  is null.");
            }
            var userData = await _context.UserDirectoryData.FindAsync(id);
            var AdminUserId = userData.DataStatusId;
            if (userData != null)
            {
                _context.UserDirectoryData.Remove(userData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new {id = AdminUserId });
        }

        private bool UserDataExists(int id)
        {
          return (_context.UserDirectoryData?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
