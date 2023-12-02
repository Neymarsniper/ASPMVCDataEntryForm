using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemberDataEntryForm.Models;
using System.Net;

namespace PPCLUB.Controllers
{
    public class UserProposedDataController : Controller
    {
        private readonly MemberDataContext _context;

        public UserProposedDataController(MemberDataContext context)
        {
            _context = context;
        }

        // GET: UserProposedData
        public async Task<IActionResult> Index()
        {
              return _context.UserProposedDirectoryData != null ? 
                          View(await _context.UserProposedDirectoryData.ToListAsync()) :
                          Problem("Entity set 'MemberDataContext.UserProposedDirectoryData'  is null.");
        }

        // GET: UserProposedData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserProposedDirectoryData == null)
            {
                return NotFound();
            }

            var userProposedData = await _context.UserProposedDirectoryData.Include(m => m.UserStatusCodes).FirstOrDefaultAsync(m => m.Id == id);
            if (userProposedData == null)
            {
                return NotFound();
            }

            return View(userProposedData);
        }

        // GET: UserProposedData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserProposedData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,EmailConfirmed,Password,PasswordConfirmed,Address,City,MobileNo,UserRoleId")] UserProposedData userProposedData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userProposedData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userProposedData);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        // GET: UserProposedData/Edit/5
        public async Task<IActionResult> Edit(int? id, int? AdminUserId)
        {
            //var userdata = _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == AdminUserId);
            
            if (id == null || _context.UserProposedDirectoryData == null)
            {
                return NotFound();
            }

            var userProposedData = await _context.UserProposedDirectoryData.Include(m => m.UserStatusCodes).Include(m => m.UserData).FirstOrDefaultAsync(m => m.UserId == id);
           
            if (userProposedData == null)
            {

                return RedirectToAction("Index","User", new { id = AdminUserId });
            }
            
            return View(userProposedData);
        }

        // POST: UserProposedData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int AdminUserId, [Bind("FirstName,LastName,Email,EmailConfirmed,Password,PasswordConfirmed,Address,City,MobileNo,UserRoleId,UserId")] UserProposedData userProposedData)
        {
            if (id != userProposedData.UserId)
            {
                return NotFound();
            }

            var userdata = await _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == id);

            userdata.FirstName = userProposedData.FirstName;
            userdata.LastName = userProposedData.LastName;
            userdata.Email = userProposedData.Email;
            //userdata.EmailConfirmed = userProposedData.EmailConfirmed;
            userdata.Password = userProposedData.Password;
            userdata.PasswordConfirmed = userProposedData.PasswordConfirmed;
            userdata.Address = userProposedData.Address;
            userdata.City = userProposedData.City;
            userdata.MobileNo = userProposedData.MobileNo;
            userdata.UserRoleId = userProposedData.UserRoleId;
            userdata.DataStatusId = 2;
            userProposedData.DataStatusId = 2;

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.UserDirectoryData.Update(userdata);
                    //var allrecords = _context.UserProposedDirectoryData.ToList();
                    //_context.UserProposedDirectoryData.RemoveRange(allrecords);
                    await _context.SaveChangesAsync();
            }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProposedDataExists(userProposedData.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            return RedirectToAction("Index", "User", new { id = AdminUserId });
            //return RedirectToAction("Logout", "User");
            //}
            //return View(userProposedData);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserProposedDirectoryData == null)
            {
                return Problem("Entity set 'MemberDataContext.UserProposedDirectoryData'  is null.");
            }
            var userProposedData = await _context.UserProposedDirectoryData.FindAsync(id);
            if (userProposedData != null)
            {
                _context.UserProposedDirectoryData.Remove(userProposedData);
            }
            
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Logout", "User");
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





        // GET: UserProposedData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserProposedDirectoryData == null)
            {
                return NotFound();
            }

            var userProposedData = await _context.UserProposedDirectoryData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProposedData == null)
            {
                return NotFound();
            }

            return View(userProposedData);
        }

        // POST: UserProposedData/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]



        private bool UserProposedDataExists(int id)
        {
          return (_context.UserProposedDirectoryData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
